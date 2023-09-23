using System.Collections;
using System.Collections.Generic;
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
    public EventController eventcontroller;


    public GameObject fireball;
    public Camera cam;
    public Animator anim;
    public bool Init = false;
    public Vector3 target;
    ISkill SkillInterface;
    public Slash SlashSkill;
    public Dash DashSkill;
    float horizontal;
    float vertical;
    float colTime = 0;
    private void SkillManage()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //if(om[itget
    }
    private void OnTriggerEnter(Collider collision)
    {

    }

    public void Start()
    {
        Hp = new Data(100);
    }

    public void Attack()
    {
        anim.SetTrigger("IdleToAttack");
        anim.SetTrigger("AttackToIdle");

        //StartCoroutine(ColEnable());
    }
    public void Move()
    {

    }

    IEnumerator ColEnable()
    {
        float t = 0;

        while(t < 3)
        {
            Debug.Log("ㅣ");
            t += Time.deltaTime;
            GetComponent<Collider>().enabled = true;

            if(t >= 3)
            {
                Debug.Log("ㅣㅣ");
                GetComponent<Collider>().enabled = false;
                yield break;

            }
            yield return null;

        }

        yield return null;



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
        transform.position += new Vector3(horizontal, 0, vertical) * Time.deltaTime * 10;

        if(Input.GetKeyDown(KeyCode.Q)) //파이어볼
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

        if (Input.GetMouseButtonDown(0))
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
            SkillInterface = DashSkill;
            SkillInterface.CanUse = true;
            if (SkillInterface.CanUse)
                SkillInterface.Work(this);
        }
        if(isFlip)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            isFlip = false;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            isFlip = true;

    }
}