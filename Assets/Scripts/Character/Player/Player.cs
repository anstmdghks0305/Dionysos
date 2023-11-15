using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Data Hp { private set; get; }
    public Data Fever { private set; get; }
    public int Speed;
    public int Damage;
    public float AttackSpeed;
    public float DashDistance;
    public Animator animator { set; get; }
    public bool isFlip { get; set; }
    public bool Died;
    public State state { set; get; }
    public IState IState { get; set; }
    public bool Attacking;
    public Rhythm PlayerRhythm;
    public EventController eventcontroller;
    public EffectManager Effect;
    public Slash SlashSkill;
    public Dash DashSkill;
    public GameObject fireball;
    public Animator anim;
    [SerializeField] int defaultSpeed = 3;
    [SerializeField] int feverSpeed = 10;
    [SerializeField] int dashDefaultDamage = 30;
    [SerializeField] int dashPowerUpDamage = 70;
    [SerializeField] float dashDistance = 3;
    public float defaultAttackSpeed = 0.4f;
    [SerializeField] int defaultWeaponDamage = 30;
    [SerializeField] int powerUpWeaponDamage = 70;
    public int slashDefaultDamage = 50;
    public int slashPowerUpDamage = 100;
    public int slashMaxCount = 5;
    [SerializeField] int defaultFireBallDamage = 30;
    [SerializeField] int powerUpFireBallDamage = 70;
    [SerializeField] float maxHurtTime = 0.5f;
    [SerializeField] float maxFeverTime = 10;
    public Vector3 target;
    public ISkill SkillInterface;
    public float horizontal;
    public float vertical;
    public bool dash = false;
    public bool attack;
    float attackT;
    public Vector3 scale, newScale;
    public bool attackPowerUp;
    public bool fireBallPowerUP;
    public PlayerWeapon weapon;
    private bool hurt;
    float hurtTime = 0;
    public bool slash;
    bool dashInit;
    public bool dashPowerUp;
    public bool fever;
    public BarUI feverUI;
    float feverT;
    float feverT2;
    bool feverInit;
    bool isFireball;
    Rigidbody r;
    public float fireBallTime;
    public float fireBallCoolTime;

    private void Awake()
    {

    }
    private void SkillManage()
    {

    }


    public void Start()
    {
        PlayerData data = GameManager.Instance.playerData;
        Hp = new Data(data.hp);
        eventcontroller.DoEvent(new EventData("Hp", Hp));
        Fever = new Data(data.fever);
        Fever -= data.fever;
        Speed = data.speed;
        state = State.Idle;
        scale = transform.GetChild(0).GetChild(3).localScale;
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
        r = GetComponent<Rigidbody>();
        fireBallCoolTime = data.fireballCoolTime;
        fireBallTime = fireBallCoolTime;
        DashSkill._coolTime = data.dashCoolTime;
        SlashSkill._coolTime = data.slashCoolTime;
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
        {
            state = State.Move;

            
        }
        else
        {
            state = State.Idle;
        }

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
        r.velocity = Vector3.zero;

        SkillManage();
        PlayerAnim();
        HurtTime();

        if(fireBallTime <= fireBallCoolTime)
        {
            fireBallTime += Time.deltaTime;
        }

        if (Hp.ShowCurrentHp() > 0 && 
            //!hurt && 
            !GameManager.Instance.GameStop)
        {
            InputKey();
        }
        else if(Hp.ShowCurrentHp() <= 0)
        {
            state = State.Die;
        }

        Flip();
        IfAttack();
        IfDash();
        IfFever();
    }
    bool init2 = false;
    void InputKey()
    {
        if (!attack && !slash && !dash)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.X))
            {
                attack = true;
                isFireball = false;

                if (fever)
                    attackPowerUp = true;
                else
                {
                    PlayerRhythm.InputAction("Attack");

                }
                if (attackPowerUp)
                {
                    weapon.Damage = powerUpWeaponDamage; 
                    Effect.AttackEffect("Perfect");
                    weapon.transform.localScale = newScale;
                    Manager.SoundManager.Instance.PlaySFXSound("슉", 1);
                }
                else
                {
                    weapon.Damage = defaultWeaponDamage;
                    Effect.AttackEffect("Bad");
                    weapon.transform.localScale = scale;
                    Manager.SoundManager.Instance.PlaySFXSound("검격2", 1);
                }

                //if 퍼펙트 == true => attackPowerUp = true;

            }
            if (Input.GetKeyDown(KeyCode.C) && fireBallTime >= fireBallCoolTime)
            {
                fireBallTime = 0;
                Manager.SoundManager.Instance.PlaySFXSound("화염구", 1);
                attack = true;
                isFireball = true;

                if (fever)
                    fireBallPowerUP = true;
                else
                    PlayerRhythm.InputAction("FireBall");

                GameObject ball = Instantiate(fireball);

                ball.AddComponent<FireBall>();
                if (fireBallPowerUP)
                {
                    ball.transform.localScale = new Vector3(transform.localScale.x + 3, transform.localScale.y + 3, transform.localScale.z + 3);
                    ball.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                    ball.GetComponent<FireBall>().damage = powerUpFireBallDamage;
                }
                else
                {
                    ball.transform.position =
                        new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
                    ball.GetComponent<FireBall>().damage = defaultFireBallDamage;
                }


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
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(Fever.ShowCurrentHp() >= 100)
                    fever = true;
            }
        }

    }
    bool init;
    void IfAttack()
    {
        if (attack)
        {
            if(!init)
            {

                anim.SetTrigger("RunToIdle");
                init = true;
            }
            attackT += Time.deltaTime;

            if (attackT < AttackSpeed)
            {
                state = State.Attack;
                Attacking = true;
                
            }
            else if (attackT >= AttackSpeed)
            {

                init = false;
                anim.SetTrigger("AttackToIdle");
                attackPowerUp = false;
                state = State.Idle;
                attack = false;
                attackT = 0;
                Attacking = false;
            }
        }
    }
    void IfDash()
    {
        if (dash)
        {
            if(!dashInit)
            {
                //effect
                dashInit = true;
            }

            Collider[] Colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].name == "ExplosionArrow(Clone)" && dashPowerUp)
                {
                    ProjectileController.Instance.UsedProjectilePooling(Colliders[i].GetComponent<Projectile>());
                    Colliders[i].gameObject.SetActive(false);
                }
                else if (Colliders[i].CompareTag("enemy"))
                {
                    if (dashPowerUp)
                        Colliders[i].GetComponent<Enemy>().Damaged(dashPowerUpDamage);
                    else
                        Colliders[i].GetComponent<Enemy>().Damaged(dashDefaultDamage);
                }
                else if (Colliders[i].CompareTag("Respawn"))
                {
                    dash = false;
                    break;
                }
            }
        }
        else
        {
            dashInit = false;
            dashPowerUp = false;
        }
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
                Speed = defaultSpeed;
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
            //state = State.Stun;

            if (hurtTime >= maxHurtTime)
            {
                hurt = false;
                hurtTime = 0;
                //anim.SetTrigger("DamageToIdle");
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
        if (Hp.ShowCurrentHp() < 100)
        {
            Hp += value;
            eventcontroller.DoEvent(new EventData("Hp", Hp));
            Debug.Log(Hp.ShowCurrentHp());
        }
    }
}