using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Data Hp { private set; get; } //플레이어 HP
    public Data Fever { private set; get; } //플레이어 FEVER게이지
    public bool isFlip { get; set; } //플레이어의 좌우 방향
    public State state { set; get; } //플레이어의 현재 상태를 나타내는 열거형 변수
    public int Speed; //플레이어의 이동 속도
    public int Damage; //플레이어 기본 공격력
    public float AttackSpeed; //플레이어 공격속도
    public float DashDistance; //대쉬 스킬의 이동 거리
    public bool Died; //플레이어 사망여부
    public Rhythm PlayerRhythm; //리듬 입력을 처리를 위한 인스턴스
    public EventController eventcontroller; //HP나 FEVER게이지를 관리하는 변수
    public EffectManager Effect; //이펙트효과를 위한 인스턴스
    public ISkill SkillInterface;
    public Slash SlashSkill;
    public Dash DashSkill;
    public GameObject fireball; //화염구 게임 오브젝트
    public Animator anim; //플레이어 애니메이터
    [SerializeField] int feverSpeed; //플레이어 FEVER 상태일 때 이동 속도
    [SerializeField] int dashDefaultDamage; //기본 대쉬 공격 데미지
    [SerializeField] int dashPowerUpDamage; //강화된 대쉬 공격 데미지
    [SerializeField] float dashDistance; //대쉬 스킬 이동 거리
    public float defaultAttackSpeed; //플레이어 기본 공격 속도
    [SerializeField] int defaultWeaponDamage; //기본 공격 데미지
    [SerializeField] int powerUpWeaponDamage; //강화 공격 데미지
    public int slashDefaultDamage; //기본 슬래시 스킬 데미지
    public int slashPowerUpDamage; //강화된 슬래시 스킬 데미지
    public int slashMaxCount; //슬래시 스킬의 최대 공격 횟수
    [SerializeField] int defaultFireBallDamage; //기본 화염구 데미지
    [SerializeField] int powerUpFireBallDamage; //강화된 화염구 데미지
    [SerializeField] float maxHurtTime;//플레이어가 피격된 후 아파하는 시간
    [SerializeField] float maxFeverTime; //플레이어가 피버타임을 유지할 수 있는 최대 시간
    public Vector3 target; //대쉬 사용시 플레이어가 향하는 방향
    public float horizontal, vertical; //플레이어의 수평 및 수직 입력 값
    public bool dash = false; //플레이어가 대쉬 중인지 여부를 나타내는 변수
    public bool attack; //플레이어가 공격 중인지 여부를 나타내는 변수
    float attackT; //공격 지속시간을 추적하기 위한 변수
    public PlayerWeapon weapon; //플레이어 무기
    public Vector3 scale, newScale; //무기 공격판정
    public bool attackPowerUp; //기본공격 강화여부
    public bool fireBallPowerUP; //화염구 공격 강화여부
    private bool hurt; //플레이어가 피격 후 아파하는지 여부
    float hurtTime = 0; //플레이어 피격 후 경과시간을 추적하는 변수
    public bool slash; //플레이어가 슬래시스킬 사용 중인지 여부
    public bool dashPowerUp; //대쉬스킬 강화여부
    public bool fever; //플레이어가 피버상태인지 여부
    public BarUI feverUI; //FEVER UI 관리를 위한 인스턴스
    float feverT, feverT2; //fever상태 지속 시간 및 fever게이지 감소 시간을 추적하는 변수
    bool feverInit; //fever상태 초기화를 위한 변수
    public float fireBallTime, fireBallCoolTime; //파이어볼 쿨타임 관리를 위한 변수
    public PlayerData data; //플레이어 데이터를 나타내는 인스턴스
    Rigidbody r;

    private void Awake()
    {
    }

    public void Start()
    {
        //data로부터 가져온 데이터를 기반으로 플레이어의 스탯을 초기화 시켜준다.
        data = GameManager.Instance.playerData;
        Hp = new Data(data.hp); 
        eventcontroller.DoEvent(new EventData("Hp", Hp)); 
        Fever = new Data(data.fever);
        Fever -= data.fever;
        Speed = data.speed;
        defaultWeaponDamage = data.attackDamage;
        DashDistance = data.dashDistance;
        AttackSpeed = data.attackSpeed;
        defaultAttackSpeed = data.attackSpeed;
        powerUpWeaponDamage = data.powerAttackDamage;
        dashDefaultDamage = data.dashDamage;
        dashPowerUpDamage = data.powerDashDamage;
        slashDefaultDamage = data.slashDamage;
        slashPowerUpDamage = data.powerSlashDamage;
        slashMaxCount = data.slashCount;
        defaultFireBallDamage = data.fireballDamage;
        powerUpFireBallDamage = data.powerFireballDamage;
        hurtTime = data.hurtTime;
        maxFeverTime = data.feverTime;
        newScale = new Vector3(scale.x + data.powerAttackRange, scale.y + data.powerAttackRange, scale.z + data.powerAttackRange);
        fireBallCoolTime = data.fireballCoolTime;
        fireBallTime = fireBallCoolTime;
        DashSkill._coolTime = data.dashCoolTime;
        SlashSkill._coolTime = data.slashCoolTime;
        r = GetComponent<Rigidbody>();
        state = State.Idle;
        scale = transform.GetChild(0).GetChild(3).localScale;
    }

    public void Attack()
    {
        attack = true;

        //퍼펙트 == true => attackPowerUp = true;
    }
    public void Move()
    {
       
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (!dash && state != State.Stun && state != State.Attack)
        {
            transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * Speed;
        }
        if ((horizontal != 0) || (vertical != 0))
            state = State.Move;
        else
            state = State.Idle;
    }


    public void Die()
    {
        if(!Died)
        {
            Died = true;
            GameManager.Instance.EndStage(false);
        }
    }
    private void Update()
    {
        r.velocity = Vector3.zero; //플레이어가 외부적인 힘에 움직이는 것을 방지
        PlayerAnim(); //플레이어의 애니메이션을 업데이트하는 함수

        if (Hp.ShowCurrentHp() > 0 && !GameManager.Instance.GameStop) //살아있거나 게임이 정지상태가 아닐경우 키 조작 가능
        {
            InputKey();
        }
        else if (Hp.ShowCurrentHp() <= 0) //만약 플레이어의 체력이 0 이하로 떨어지면, 플레이어 상태를 사망 상태로 설정
        {
            state = State.Die;
        }

        if (fireBallTime <= fireBallCoolTime) //화염구 스킬의 쿨타임 관리
        {
            fireBallTime += Time.deltaTime;
        }


        if (hurt)
        {
            HurtTime(); //플레이어가 피격 당할 시에 호출되는 함수
        }

        Flip(); //플레이어의 모습이 바라보는 방향에 따라 뒤집는 함수
        IfAttack(); //플레이어 공격 처리 함수
        IfDash(); //플레이어 대쉬 처리 함수
        IfFever(); //플레이어 피버상태 처리 함수
    }
    void InputKey() //사용자의 키 입력에 따라 플레이어의 동작을 처리하는 함수
    {
        if (!attack && !slash && !dash) //공격중이 아니거나 스킬 사용중이 아닐 때
        {
            Move(); //플레이어 이동을 처리하는 함수

            if (Input.GetKeyDown(KeyCode.X)) //x키 입력 시 공격 동작 처리
            {
                attack = true; //플레이어의 공격 상태 활성화

                if (fever) //피버상태일 경우 리듬판정에 상관없이 공격을 강화시킴
                    attackPowerUp = true;
                else //아니면 리듬입력을 처리하는 함수를 호출
                    PlayerRhythm.InputAction("Attack");


                //리듬판정을 잘 맞출 경우 attackPowerup이 true가 된다.
                if (attackPowerUp)
                {
                    weapon.Damage = powerUpWeaponDamage; //공격력 증가
                    Effect.AttackEffect("Perfect"); //리듬효과 퍼펙트효과
                    weapon.transform.localScale = newScale; //공격범위 증가
                    Manager.SoundManager.Instance.PlaySFXSound("슉", 1); //공격사운드
                }
                //아니라면
                else
                {
                    weapon.Damage = defaultWeaponDamage; //기본 공격력
                    Effect.AttackEffect("Bad"); //리듬효과 보통효과
                    weapon.transform.localScale = scale; //공격범위 그대로
                    Manager.SoundManager.Instance.PlaySFXSound("검격2", 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.C) && fireBallTime >= fireBallCoolTime)// 화염구 스킬 쿨타임이 다 찼을 때 c키를 누르면 화염구 스킬 동작을 처리
            {
                fireBallTime = 0; //쿨타임을 0으로 초기화
                Manager.SoundManager.Instance.PlaySFXSound("화염구", 1); //화염구사운드
                attack = true; 

                if (fever) //피버상태일 경우 리듬판정에 상관없이 공격을 강화시킴
                    fireBallPowerUP = true;
                else //아니면 리듬입력을 처리하는 함수를 호출
                    PlayerRhythm.InputAction("FireBall");

                //화염구 생성
                GameObject ball = Instantiate(fireball);
                ball.AddComponent<FireBall>();

                if (fireBallPowerUP) //리듬판정에 의해 화염구가 강화상태일 경우
                {
                    ball.transform.localScale = new Vector3(transform.localScale.x + 3, transform.localScale.y + 3, transform.localScale.z + 3); //화염구 크기 증가
                    ball.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                    ball.GetComponent<FireBall>().damage = powerUpFireBallDamage; //데미지 증가
                }
                else
                {
                    ball.transform.position =
                        new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
                    ball.GetComponent<FireBall>().damage = defaultFireBallDamage;
                }

                //flip여부에 따라 화염구가 날아가는 방향을 다르게 설정한다.
                if (isFlip)
                    ball.GetComponent<FireBall>().dir = Vector3.right;
                else
                    ball.GetComponent<FireBall>().dir = Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                slash = true;
                SkillInterface = SlashSkill;

                if (fever || SkillInterface.coolTime >= SlashSkill._coolTime)
                {
                    SkillInterface.CanUse = true;

                    if (fever)
                        SkillInterface.powerUp = true;
                    else
                        PlayerRhythm.InputAction("Slash");

                    if (SkillInterface.CanUse)
                        SkillInterface.Work(this);
                }
                else
                    slash = false;

                //자세한 부분은 Slash.cs 참고
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SkillInterface = DashSkill;

                if (fever || SkillInterface.coolTime >= DashSkill._coolTime)
                {
                    Manager.SoundManager.Instance.PlaySFXSound("대쉬2", 1);
                    dash = true;
                    SkillInterface.CanUse = true;

                    if (fever)
                        SkillInterface.powerUp = true;
                    else
                        PlayerRhythm.InputAction("Dash");

                    if (SkillInterface.CanUse)
                        SkillInterface.Work(this);

                }
                else
                    dash = false;

                //IfDash() 및 Dash.cs 참고
            }
            if (Input.GetKeyDown(KeyCode.F) && Fever.ShowCurrentHp() >= 100) //피버게이지가 100일때 f키를 누르면 피버상태로 전환
            {
                fever = true;
            }
        }

    }
    bool init;
    void IfAttack() //플레이의 공격동작 처리
    {
        if (attack) //플레이어가 공격 중인 상태일 때
        {
            if(!init)
            {
                anim.SetTrigger("RunToIdle");
                init = true;
            }
            attackT += Time.deltaTime; //공격타이머 증가

            if (attackT < AttackSpeed) //공격타이머가 아직 AttackSpeed(공격속도)보다 낮을 경우
            {
                state = State.Attack; //플레이어의 상태를 공격상태로 설정
            }
            else if (attackT >= AttackSpeed) //공격타이머가 attackspeed(공격속도)를 넘어서면
            {
                attack = false; //공격상태 해제
                attackPowerUp = false; //공격 강화상태 해제
                state = State.Idle; //플레이어 상태를 대기상태로 설정
                attackT = 0; //공격타이머 초기화
                init = false;
                anim.SetTrigger("AttackToIdle");
            }
        }
    }
    void IfDash()
    {
        if (dash) //플레이어가 dash상태일때
        {
            //대쉬 중에 플레이어 주위의 충돌체들과 상호작용 하기위해 Physics.OverlapBox사용
            Collider[] Colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

            //충돌체 배열 검사
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].CompareTag("Projectile") && dashPowerUp) //대쉬가 강화된 상태일 때 투사체와 닿으면
                {
                    //투사체파괴 로직
                    ProjectileController.Instance.UsedProjectilePooling(Colliders[i].GetComponent<Projectile>());
                    Colliders[i].gameObject.SetActive(false);
                }
                else if (Colliders[i].CompareTag("enemy")) //적군과 충돌 시
                {
                    //대쉬 강화 여부에 따라 데미지를 다르게 입힌다.
                    if (dashPowerUp)
                        Colliders[i].GetComponent<Enemy>().Damaged(dashPowerUpDamage);
                    else
                        Colliders[i].GetComponent<Enemy>().Damaged(dashDefaultDamage);
                }
                else if (Colliders[i].CompareTag("Respawn")) //벽과 닿으면 대쉬 중단
                {
                    dash = false;
                    break;
                }
            }
        }
        else
            dashPowerUp = false; //대쉬 상태가 아니면 대쉬 강화상태를 해제
    }
    void IfFever()
    {
        if(fever)
        {
            feverT += Time.deltaTime;
            feverT2 += Time.deltaTime;
            float feverT3 = maxFeverTime * 0.01f - 0.005f;
            AttackSpeed = 0.3f;
            
            DashDistance = 5;
            Damage = 50;

            if (feverT >= maxFeverTime)
            {
                feverT = 0;
                fever = false;
                feverInit = false;
            }
            if (feverUI.ReturnFillAmount() != 0 && feverT2 >= feverT3)
            {
                Fever -= 1;
                eventcontroller.DoEvent(new EventData("Fever", Fever));
                feverT2 = 0;
            }
        }
        else if(!fever)
        {
            if(!feverInit)
            {
                eventcontroller.DoEvent(new EventData("Fever", Fever));
                AttackSpeed = defaultAttackSpeed;
                DashDistance = dashDistance;
                Damage = defaultWeaponDamage;
                feverInit = true;
                SlashSkill.coolTime = SlashSkill._coolTime;
                DashSkill.coolTime = DashSkill._coolTime;
            }
        }
    }

    void Flip()
    {
        if (isFlip)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);

        if (horizontal < 0)
            isFlip = false;
        else if (horizontal > 0)
            isFlip = true;
    }
    public void Idle()
    {

    }

    public Transform where()
    {
        return this.transform;
    }
    public void PlayerAnim()
    {
        if (state == State.Move) //뛰기
        {
            GetComponent<Rigidbody>().isKinematic = false;
            anim.SetTrigger("IdleToRun");
        }
        else if (state == State.Attack) //공격
        {
            anim.SetTrigger("IdleToAttack");
        }
        else if (state == State.Idle) //쉬기
        {
            GetComponent<Rigidbody>().isKinematic = true;

            anim.SetTrigger("RunToIdle");
            anim.SetTrigger("AttackToIdle");
            anim.ResetTrigger("IdleToRun");
            anim.ResetTrigger("IdleToDamage");
        }
        else if(state == State.Stun) //피격
        {
            anim.SetTrigger("IdleToDamage");
        }
        else if(state == State.Die) //사망
        {
            Debug.Log("death");
            anim.SetTrigger("RunToIdle");
            if (!dieInit)
            {
                anim.SetTrigger("AttackToIdle");
                dieInit = true;
            }
            anim.SetTrigger("IdleToDeath");
            
        }
    }
    bool dieInit = false;
    private void HurtTime()
    {
        if (hurt)
        {
            hurtTime += Time.deltaTime;

            if (hurtTime >= maxHurtTime)
            {
                hurt = false;
                hurtTime = 0;
                state = State.Idle;
            }
        }
    }

    public void Damaged(int Damage)
    {
        if (!fever && !dashPowerUp && !slash && Hp.ShowCurrentHp() > 0)
        {
            if (!hurt)
            {
                //anim.ResetTrigger("IdleToRun");
                //anim.ResetTrigger("IdleToAttack");
                //anim.SetTrigger("AttackToIdle");
                //anim.SetTrigger("RunToIdle");
                Hp -= Damage;
                eventcontroller.DoEvent(new EventData("Hp", Hp));
                //Fever -= 1;
                //eventcontroller.DoEvent(new EventData("Fever", Fever));

                hurt = true;
            }
        }
        else if(Hp.ShowCurrentHp() <= 0)
        {
            GameManager.Instance.MainCam.transform.SetParent(null);
            gameObject.SetActive(false);
            Die();
        }
    }
    public void PlusFever(int value)
    {
        if(Fever.ShowCurrentHp() < 100)
        {
            Fever += value;
            eventcontroller.DoEvent(new EventData("Fever", Fever));
        }
    }
    public void PlusHP(int value)
    {
        if (Hp.ShowCurrentHp() < data.hp)
        {
            Hp += value;
            eventcontroller.DoEvent(new EventData("Hp", Hp));
            Debug.Log(Hp.ShowCurrentHp());
        }
    }
}