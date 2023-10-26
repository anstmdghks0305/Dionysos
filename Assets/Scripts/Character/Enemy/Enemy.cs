using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Enemy : MonoBehaviour, IEnemy
{
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
    public int HP;
    private bool Hurt;
    AttackState attackState;
    RunState runState;

    public Enemy Copy(Enemy value)
    {
        Hp = new Data(value.Hp.Max);
        Type = value.Type;
        SerialNum = value.SerialNum;
        Speed = value.Speed;
        Damage = value.Damage;
        AttackCoolTime = value.AttackCoolTime;
        Projectile_SerialNum = value.Projectile_SerialNum;
        AttackRange = value.AttackRange;
        return this;
    }

    private void Start()
    {
        Copy(EnemyDataInputer.FindEnemy(this));

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        EnemyDataInputer.CopyComponent<NavMeshAgent>(navMeshAgent, this.gameObject);
        animator = this.transform.GetChild(0).transform.GetComponent<Animator>();
        EnemyDataInputer.CopyComponent<Animator>(animator, this.gameObject);
        switch (this.Type)
        {
            case EnemyType.Near:
                attackState = new AttackState(AttackRange, AttackCoolTime);
                break;
            case EnemyType.Far:
                attackState = new FarAttackState(AttackRange, AttackCoolTime, Projectile_SerialNum);
                break;
        }
        runState = new RunState(Speed);
        IState = runState;

    }

    public void Revive(Transform Pos)
    {
        this.transform.position = Pos.position;
        this.gameObject.SetActive(true);
        animator.SetBool("Die", false);
        Hp = Hp.Reset();
        Died = false;
        eventcontroller.DoEvent(new EventData("Hp", Hp));
        state = State.Idle;

    }
    void Update()
    {
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
            Died = true;
            navMeshAgent.isStopped = true;
            animator.SetBool("Die", true);
            Invoke("PoolingSkin", 0.5f);
        }
    }

    public void PoolingSkin()
    {
        EnemyController.Instance.EnemyDiePooling(this);
        this.gameObject.SetActive(false);
    }

    public Transform where()
    {
        return this.gameObject.transform;
    }

    public void Idle()
    {
        navMeshAgent.isStopped = true;
        AnimatorControllerParameter[] paramarray = animator.parameters;
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
        eventcontroller.Filp(isFlip);
    }

    public void Damaged(int Damage)
    {
        if (!Hurt)
        {
            Hurt = true;
            HurtDelay().Forget();
            Hp -= Damage;
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
    public void Initailize(EnemyType _Type, int _SerialNum, int _Hp, int _Speed, int _Damage, int _AttackRange, int _AttackCoolTime, int _Projectile_SerialNum)
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
    }

    async UniTask HurtDelay()
    {
        await UniTask.Delay(300);
        Hurt = false;
    }
}