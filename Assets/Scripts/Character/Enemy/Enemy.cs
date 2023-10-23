using UnityEngine;
using UnityEngine.AI;

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
    public int AttackRange;
    public int AttackCoolTime;
    public int Projectile_SerialNum;

    AttackState attackState;
    RunState runState;

    public Enemy Copy(Enemy value)
    {

        Type = value.Type;
        IState = value.IState;
        SerialNum = value.SerialNum;
        Speed = value.Speed;
        Damage = value.Damage;
        AttackCoolTime = value.AttackCoolTime;
        Projectile_SerialNum = value.Projectile_SerialNum;
        
        return this;
    }

    private void OnEnable()
    {
        Copy(EnemyDataInputer.FindEnemy(this));
        navMeshAgent = this.GetComponent<NavMeshAgent>();
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
    void Update()
    {
        if (GameManager.Instance.GameStop == true || state == State.Die)
            return;
        Debug.Log(IState.ToString());

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
        if (GameManager.Instance.GameStop || player.Died)
        {
            Idle();
            return;
        }
        if (state == State.Die)
        {
            Die();
            return;
        }
        navMeshAgent.SetDestination(player.where().position);
        Filp(player.gameObject.transform);
        Debug.Log(navMeshAgent.remainingDistance);
        Debug.Log(attackState.AttackRange);
        if (navMeshAgent.remainingDistance < attackState.AttackRange)
        {
            Attack();
            Attacking = true;
        }
        else
        {
            Move();
        }

        IState.Work(this, player.transform);
    }


    public void Die()
    {
        Died = true;
        navMeshAgent.isStopped = true;
        animator.SetBool("Die", true);
        Invoke("PoolingSkin", 0.5f);
    }

    public void PoolingSkin()
    {
        EnemyController.Instance.EnemyDiePooling(this);
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
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void Damaged(int Damage)
    {
        Hp -= Damage;
        eventcontroller.DoEvent(new EventData("Hp", Hp));
        if (Hp.ShowCurrentHp() <= 0)
            state = State.Die;
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
    /// <param name="AttackAnimationTime"></param>
    /// <param name="_projectile"></param>
    public void Initailize(EnemyType _Type, int _SerialNum, int _Hp, int _Speed, int _Damage, int _AttackRange, int _AttackCoolTime, int _Projectile_SerialNum)
    {
        this.gameObject.tag = "enemy";
        this.Type = _Type;
        SerialNum = _SerialNum;
        Hp = new Data(_Hp);
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
        navMeshAgent.stoppingDistance = _AttackRange-2;
        navMeshAgent.speed = _Speed;
    }
}