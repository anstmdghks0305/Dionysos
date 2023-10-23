using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterData
{
    public Data Hp { private set; get; }
    public Data Fever{private set;get;}
    public int Speed{ set; get; }
    public int Damage{ set; get; }
    public int AttackSpeed{set; get;}
    public Animator animator{ set; get; }
    public bool isFlip { get; set; }
    public bool Died;
    public State state{ set; get; }
    public IState IState { get; set; }
    public bool Attacking { get; set; }
    public Rhythm PlayerRhythm;
    public EventController eventcontroller;
    public EffectManager Effect;
    bool init = false;
    public GameObject fireball;
    public Animator anim;
    public bool Init = false;
    public Vector3 target;
    ISkill SkillInterface;
    public Slash SlashSkill;
    public Dash DashSkill;
    public float horizontal;
    public float vertical;
    public bool dash = false;
    public bool attack;
    float attackT;
    Vector3 scale, newScale;
    Transform attackScale;
    public int enemyCount;
    public bool powerUp;
    public float attackSpeed;
    public Weapon weapon;
    public int defaultDamage = 30;
    public Rhythm PlayerRhythm;
    private bool hurt;
    public bool slash;
    [SerializeField] private float maxHurtTime;
    private void Awake()
    {
        //weapon = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetChild(1).gameObject;
    }
    private void SkillManage()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //if(om[itget
    }
    

    public void Start()
    {
        Hp = new Data(100);
        Speed = 3;
        state = State.Idle;
        attackScale = transform.GetChild(0).GetChild(3);
        scale = transform.GetChild(0).GetChild(3).localScale;
        newScale = new Vector3(scale.x + 1, scale.y + 1, scale.z + 1);

    }

    public void Attack()
    {
        attack = true;

        //퍼펙트 == true => powerUp = true;
    }
    public void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (!dash && state != State.Attack)
        {
            transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * Speed;
        }
        else
        {
            
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
        Died = true;
    }
    private void Update()
    {
        SkillManage();
        
        PlayerAnim();
        Move();

        if(hurt)
        {
            HurtTime();
        }
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            GameObject ball = Instantiate(fireball);

            ball.transform.position = 
                new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);

            ball.AddComponent<FireBall>();

            if(isFlip)
                ball.GetComponent<FireBall>().dir = Vector3.right;
            else
                ball.GetComponent<FireBall>().dir = Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            attack = true;
            PlayerRhythm.InputAction("Attack");
            //if 퍼펙트 == true => powerUp = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            slash = true;
            SkillInterface = SlashSkill;
            SkillInterface.CanUse = true;
            if (SkillInterface.CanUse)
                SkillInterface.Work(this);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
            SkillInterface = DashSkill;
            SkillInterface.CanUse = true;
            if (SkillInterface.CanUse)
                SkillInterface.Work(this);

            if(powerUp)
            {
                Damage = 50;
            }
            else if(powerUp)
            {
                Damage = defaultDamage;
            }
        }
        if(isFlip)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);

        if (horizontal < 0)
            isFlip = false;
        else if (horizontal > 0)
            isFlip = true;

        if (attack)
        {
            if (powerUp)
            {
                if (!init)
                {
                    Effect.AttackEffect("Perfect");
                    init = true;
                }
                attackScale.localScale = newScale;
                weapon.Damage = 40;
            }
            else
            {
                if (!init)
                {
                    Effect.AttackEffect("Bad");
                    init = true;
                }
                attackScale.localScale = scale;
                weapon.Damage = defaultDamage;
            }
            anim.SetTrigger("RunToIdle");
            attackT += Time.deltaTime;

            if (attackT < attackSpeed)
            {
                state = State.Attack;
                GetComponent<ICharacterData>().Attacking = true;
            }
            else if (attackT >= attackSpeed)
            {
                init = false;
                anim.SetTrigger("AttackToIdle");
                powerUp = false;
                state = State.Idle;
                attack = false;
                attackT = 0;
                init = false;
                attackScale.localScale = scale;
                GetComponent<ICharacterData>().Attacking = false;
            }
        }
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
        if(state == State.Move) //뛰기
        {
            anim.SetTrigger("IdleToRun");
        }
        else if(state == State.Attack) //공격하기
        {
            anim.SetTrigger("IdleToAttack");
        }
        else if(state == State.Idle) //쉬기
        {
            anim.SetTrigger("RunToIdle");
            anim.SetTrigger("AttackToIdle");
            anim.ResetTrigger("IdleToRun");
        }
    }
    float hurtTime = 0;
    private void HurtTime()
    {
        hurtTime += Time.deltaTime;

        if(hurtTime >= maxHurtTime)
        {
            hurt = false;
            hurtTime = 0;
        }
    }

    public void Damaged(int Damage)
    {
        if(!hurt)
        {
            Hp -= Damage;
            hurt = true;
            eventcontroller.DoEvent(new EventData("Hp", Hp));
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (dash)
        {
            if(powerUp)
            {
                if (collision.CompareTag("enemy"))
                {
                    collision.GetComponent<Enemy>().Stun();
                    collision.GetComponent<ICharacterData>().Damaged(Damage);
                }
                else if (collision.CompareTag("?"))
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}