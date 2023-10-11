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
    bool attack;
    float attackT;
    public bool move;
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
    }

    public void Attack()
    {
        attack = true;
    }
    public void Move()
    {

    }


    public void Die()
    {
        Died = true;
    }
    private void Update()
    {
        SkillManage();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(!dash && !attack)
            transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * 1;
        if((horizontal != 0) || (vertical != 0))
        {
            anim.SetTrigger("IdleToRun");
            anim.ResetTrigger("RunToIdle");
            move = true;
        }
        else
        {
            anim.SetTrigger("RunToIdle");
            anim.ResetTrigger("IdleToRun");
            move = false;
        }

        if (Input.GetKeyDown(KeyCode.Q)) //���̾
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
        if(attack)
        {
            if(!init)
            {
                Debug.Log("?");
                init = true;
            }
            attackT += Time.deltaTime;

            if(attackT < 0.3f)
            {
                anim.ResetTrigger("RunToIdle");
                anim.ResetTrigger("IdleToRun");
                anim.ResetTrigger("AttackToIdle");
                anim.SetTrigger("IdleToAttack");
                GetComponent<ICharacterData>().Attacking = true;
            }
            else if(attackT >= 0.3f)
            {
                anim.ResetTrigger("IdleToAttack");
                anim.SetTrigger("AttackToIdle");
                attack = false;
                attackT = 0;
                init = false;
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

    public void Damaged(int Damage)
    {
        Hp -= Damage;
        eventcontroller.DoEvent(new EventData("Hp",Hp));
    }
}