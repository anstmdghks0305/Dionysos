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

    public EventController eventcontroller;
    public EffectManager Effect;

    public GameObject fireball;
    public Animator anim;
    public bool Init = false;
    public Vector3 target;
    ISkill SkillInterface;
    public Slash SlashSkill;
    public Dash DashSkill;
    float horizontal;
    float vertical;
    Collider attackCollider;
    public bool dash = false;
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
        }
    }

    public void Start()
    {
        Hp = new Data(100);
        attackCollider = transform.GetChild(0).GetComponent<Collider>();
    }

    public void Attack()
    {
        anim.SetTrigger("IdleToAttack");
        anim.SetTrigger("AttackToIdle");

        StartCoroutine(ColEnable(attackCollider));
    }
    public void Move()
    {

    }

    IEnumerator ColEnable(Collider col)
    {
        float t = 0;

        while(true)
        {
            t += Time.deltaTime;
            col.enabled = true;

            if(t >= 0.25f)
            {
                col.enabled = false;
                yield break;
            }
            yield return null;
        }
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
        transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * 10;

        if(Input.GetKeyDown(KeyCode.Q)) //���̾
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
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);

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
}