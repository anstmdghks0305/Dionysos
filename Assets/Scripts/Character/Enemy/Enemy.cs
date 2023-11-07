using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using System.Collections;

public class Enemy : MonoBehaviour, IEnemy
{
    public int Score;
    public bool isBoss = false;
    public EnemyType Type { get; set; }
    public int SerialNum;
    public Data Hp { private set; get; }
    public int Speed { set; get; }
    public int Damage { set; get; }
    public Animator animator { set; get; }
    public bool Died;
    public State state { set; get; }
    public EventController eventcontroller;
    public NavMeshAgent navMeshAgent { get; set; }
    public IState IState { get; set; }
    public bool isFlip { get; set; }
    public bool Attacking { get; set; }
    public int AttackRange{get; set;}
    public int AttackCoolTime;
    public int Projectile_SerialNum;
    private bool Hurt;
    AttackState attackState;
    RunState runState;
    
    public Enemy Copy(Enemy value)
    {
        Type = value.Type;
        Hp = new Data(value.Hp.Max);
        Speed = value.Speed;
        Damage = value.Damage;
        AttackRange = value.AttackRange;
        return this;
    }

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        eventcontroller = this.transform.GetComponentInChildren<EventController>();
        navMeshAgent.updateRotation = false;
        animator = this.transform.GetChild(0).transform.GetComponent<Animator>();
        Copy(EnemyDataInputer.FindEnemy(this));
        eventcontroller.DoEvent(new EventData("Hp", Hp));
        switch (this.Type)
        {
            case EnemyType.Near:
                Debug.Log("attack");
                attackState = new AttackState(AttackRange, AttackCoolTime);
                break;
            case EnemyType.Far:
                Debug.Log("farattack");
                attackState = new FarAttackState(AttackRange, AttackCoolTime, Projectile_SerialNum);
                break;
            default:
                Debug.Log("잘못어감");
                break;
        }
        runState = new RunState(Speed);
        IState = runState;
    }

    public void Revive(Vector3 Pos)
    {
        this.transform.position = Pos;
        this.gameObject.SetActive(true);
        animator.SetBool("EditChk", true);
        Hp = Hp.Reset();
        Died = false;
        eventcontroller.DoEvent(new EventData("Hp", Hp));
        state = State.Idle;
        animator.SetBool("EditChk", false) ;
    }
    void Update()
    {
        PoolingSkin();
        if (poolingBool)
        {
            poolingTime += Time.deltaTime;
        }
        if (GameManager.Instance.GameStop == true || state == State.Die)
            return;
    }

    public void Stun()
    {
        new Stun().Work(this);
    }

    public void Attack()
    {
        IState = attackState;
    }
    public void Move()
    {
        IState = runState;
    }

    public void StateChange(Player player)
    {
        if (navMeshAgent != null)
        {
            if (GameManager.Instance.GameStop || player.Died||player.slash)
            {
                Idle();
                return;
            }
            if(!attackState.CanAttack)
            {
                Idle();
            }
            if (state == State.Die)
            {
                Die();
                return;
            }
            Filp(player.gameObject.transform);
            if (Vector3.Distance(player.transform.position, this.transform.position) < attackState.AttackRange)
            {
                Attack();
            }
            else
            {
                Move();
            }
            IState.Work(this, player.transform);
        }
    }


    public void Die()
    {
        if (!Died)
        {
            poolingTime = 0;
            Died = true;
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Die");
            poolingBool = true;
        }
    }
    [SerializeField] private float poolingTime;
    [SerializeField] private bool poolingBool;
    void PoolingSkin()
    {
        if(poolingTime >= 0.5f)
        {
            poolingBool = false;
            this.gameObject.SetActive(false);
            GameManager.Instance.CurrentStage.CurrentScore += Score;
            EnemyController.Instance.EnemyDiePooling(this);
            if (isBoss)
            {
                GameManager.Instance.EndStage(true);
            }
            poolingTime = 0;
        }
    }

    public Transform where()
    {
        return this.gameObject.transform;
    }

    public void Idle()
    {
        animator.SetBool("Run", true);
        animator.SetFloat("RunState", 0);
        navMeshAgent.isStopped = true;
        IState = null;
    }
    public void Filp(Transform player)
    {
        isFlip = this.gameObject.transform.position.x - player.transform.position.x > 0 ? true : false;
        if (isFlip)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(!isBoss)
            eventcontroller.Filp(isFlip);
    }

    public void Damaged(int Damage)
    {
        if (!Hurt)
        {
            Hurt = true;
            HurtDelay().Forget();
            Hp -= Damage;
            //GameManager.Instance.CurrentStage.CurrentScore += 10;
            eventcontroller.DoEvent(new EventData("Hp", Hp));
            if (Hp.ShowCurrentHp() <= 0)
                state = State.Die;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Type">EnemyType(근거리/원거리)</param>
    /// <param name="_SerialNum">SerialNum 이랑 프리팹이름과 동일해야함</param>
    /// <param name="_Hp">Hp 최대값</param>
    /// <param name="_Speed">스피트값</param>
    /// <param name="_Damage"></param>
    /// <param name="_AttackRange"></param>
    /// <param name="_AttackCoolTime"></param>
    public void Initailize(EnemyType _Type, int _SerialNum, int _Hp, int _Speed, int _Damage, int _AttackRange, int _AttackCoolTime, int _Projectile_SerialNum, int _Score)
    {
        this.gameObject.tag = "enemy";
        this.Type = _Type;
        Hp = new Data(_Hp);
        SerialNum = _SerialNum;
        Damage = _Damage;
        animator = this.transform.GetChild(0).transform.GetComponent<Animator>();
        Died = false;
        eventcontroller = this.transform.GetComponentInChildren<EventController>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        IState = null;
        Attacking = false;
        AttackRange = _AttackRange;
        AttackCoolTime = _AttackCoolTime;
        Projectile_SerialNum = _Projectile_SerialNum;
        Speed = _Speed;
        navMeshAgent.stoppingDistance = _AttackRange - 2;
        navMeshAgent.speed = _Speed;
        Score = _Score;
    }

    async UniTask HurtDelay()
    {
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        Hurt = false;
    }
}