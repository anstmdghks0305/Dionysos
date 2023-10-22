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
    private void Awake()
    {
        //weapon = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetChild(1).gameObject;
    }
    private void SkillManage()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //if(om[itget
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(dash && collision.CompareTag("enemy"))
        {
            collision.GetComponent<Enemy>().Stun();
            collision.GetComponent<ICharacterData>().Damaged(Damage);
        }
    }

    public void Start()
    {
        Hp = new Data(100);
        Speed = 3;
        state = State.Idle;
        Damage = 30;
    }

    public void Attack()
    {
        attack = true;
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
        Debug.Log(state == State.Attack);
        SkillManage();
        
        PlayerAnim();
        Move();

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
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
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
            anim.SetTrigger("RunToIdle");
            attackT += Time.deltaTime;

            if (attackT < 0.3f)
            {
                state = State.Attack;
                GetComponent<ICharacterData>().Attacking = true;
            }
            else if (attackT >= 0.3f)
            {
                anim.SetTrigger("AttackToIdle");
                state = State.Idle;
                attack = false;
                attackT = 0;
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

    public void Damaged(int Damage)
    {
        Hp -= Damage;
        eventcontroller.DoEvent(new EventData("Hp",Hp));
    }
}