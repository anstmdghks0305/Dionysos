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
    GameObject[] e;
    bool slash = false;
    public bool dash = false;
    public float slashTime = 0;
    public bool Init = false;
    int index = 0;
    List<GameObject> enemies;
    public Transform testPos;
    public Vector3 target;
    ISkill SkillInterface;
    public Slash SlashSkill;
    Dash DashSkill = new Dash();
    float horizontal;
    float vartical;
    private void SkillManage()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //if(om[itget
    }
    private void OnTriggerEnter(Collider collision)
    {
       
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SkillInterface = 벽렴일섬;
        //    if (SkillInterface.CanUse)
        //        SkillInterface.Work();
        //    SkillInterface.Work();
        //}
        //Hp += (-5);
        //eventcontroller.DoEvent(new EventData("Hp", Hp));

        if (dash)
        {

        }
    }

    public void Start()
    {
        Hp = new Data(100);
    }


    public void Attack()
    {

        anim.SetTrigger("IdleToAttack");
        anim.SetTrigger("AttackToIdle");
    }




    public void Die()
    {
        Died = true;
    }
    private void Update()
    {

        SkillManage();
        horizontal = Input.GetAxis("Horizontal");
        vartical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, 0, vartical) * Time.deltaTime * 10;

        if(Input.GetKeyDown(KeyCode.Q)) //파이어볼
        {
            GameObject ball = Instantiate(fireball);

            ball.transform.position = transform.position;

            ball.AddComponent<FireBall>();
            ball.GetComponent<FireBall>().dir = (cam.ScreenToWorldPoint(Input.mousePosition) - ball.transform.position).normalized;
        }


        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillInterface = SlashSkill;
            //SlashInit();
            if (SkillInterface.CanUse)
                SkillInterface.Work(this);

        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            SkillInterface = DashSkill;
            if (SkillInterface.CanUse)
                SkillInterface.Work(this);
        }
        if (Input.GetKeyDown(KeyCode.E) && !dash)
        {
            slash = true;
            enemies = new List<GameObject>();

            e = GameObject.FindGameObjectsWithTag("enemy");

            //카메라 안에 있는 적군 가져오기

            //선택정렬
            for (int i = 0; i < enemies.Count - 1; i++)
            {
                for(int j = i + 1; j < enemies.Count; j++)
                {
                    if(Vector3.Distance(enemies[j].transform.position, transform.position) <
                        Vector3.Distance(enemies[i].transform.position, transform.position))
                    {
                        GameObject temp = enemies[j];
                        enemies[j] = enemies[i];
                        enemies[i] = temp;
                    }
                }
            }
        }
        //if (slash)
        //{
        //    Debug.Log("check2");
        //    slashTime += Time.deltaTime;
        //    if (slashTime > 0.25f)
        //    {
        //        if(!Init)
        //        {
        //            if(cam.WorldToViewportPoint(enemies[index].transform.position).x > 0.5f)
        //            {
        //                transform.position = new Vector3(enemies[index].transform.position.x - 1, enemies[index].transform.position.y, enemies[index].transform.position.z);
        //                isFlip = true;
        //            }
        //            else
        //            {
        //                transform.position = new Vector3(enemies[index].transform.position.x + 1, enemies[index].transform.position.y, enemies[index].transform.position.z);
        //                isFlip = false;
        //            }
        //            Attack();
        //        }

        //        if (index < enemies.Count - 1)
        //        {
        //            index++;
        //        }
        //        else
        //        {
        //            Init = false;
        //            slash = false;
        //            slashTime = 0;
        //            index = 0;
        //        }

        //        slashTime = 0;
        //        Init = false;
        //    }
        //}

        //else if (!slash)
        //{
        //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //        isFlip = false;
        //    else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //        isFlip = true;
        //}
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && !slash)
        {
            dash = true;
        }

        if(dash)
        {
            if(!Init)
            {
                if(isFlip)
                {
                    target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                }
                else
                {
                    target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                }
                Init = true;
            }

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 7);

            if(Vector3.Distance(target, transform.position) < 0.1f)
            {
                dash = false;
                Init = false;
            }
        }

        if(isFlip)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void SlashInit()
    {
        slash = true;
        enemies = new List<GameObject>();

        e = GameObject.FindGameObjectsWithTag("enemy");

        //카메라 안에 있는 적군 가져오기
        for (int i = 0; i < e.Length; i++)
        {
            Vector3 enemyPoints = cam.WorldToViewportPoint(e[i].transform.position);

            if (enemyPoints.x > 0 && enemyPoints.x < 1
                && enemyPoints.y > 0 && enemyPoints.y < 1)
            {
                enemies.Add(e[i]);
            }
        }

        //선택정렬
        for (int i = 0; i < enemies.Count - 1; i++)
        {
            for (int j = i + 1; j < enemies.Count; j++)
            {
                if (Vector3.Distance(enemies[j].transform.position, transform.position) <
                    Vector3.Distance(enemies[i].transform.position, transform.position))
                {
                    GameObject temp = enemies[j];
                    enemies[j] = enemies[i];
                    enemies[i] = temp;
                }
            }
        }
    }
}