using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
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

    AttackState attackState;
    RunState runState;

    private void Start()
    {
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
        IState = runState;
    }
    public void Move()
    {
        IState = attackState;
    }

    public void StateChange(Player player)
    {
        if (GameManager.Instance.GameStop|| player.Died)
        {
            Idle();
            return;
        }
        if (state == State.Die)
        {
            Die();
            return;
        }
        navMeshAgent.SetDestination(player.gameObject.transform.position);
        Filp(player.gameObject.transform);
        if (state == State.Idle)
        {
            if (navMeshAgent.remainingDistance > attackState.AttackRange)
            {
                Attack();
                Attacking = true;
            }
            else
            {
                Move();
            }
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
        isFlip = this.gameObject.GetComponent<RectTransform>().position.x - player.GetComponent<RectTransform>().position.x > 0 ? true : false;
        if (isFlip)
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void Damaged(int Damage)
    {
        Debug.Log(Damage);
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
    public void Initailize(EnemyType _Type, int _SerialNum, int _Hp, int _Speed, int _Damage, int _AttackRange, int _AttackCoolTime,int? _Projectile_SerialNum =null)
    {
        this.gameObject.tag = "enemy";
        this.Type = _Type;
        SerialNum = _SerialNum;
        Hp = new Data(_Hp);
        Damage = _Damage;
        animator = this.transform.GetChild(0).transform.GetComponent<Animator>();
        Died = false;
        eventcontroller = this.transform.GetComponentInChildren<EventController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IState = null;
        Attacking = false;
        switch (this.Type)
        {
            case EnemyType.Near:
            attackState = new AttackState(_AttackRange,_AttackCoolTime);
            break;
        case EnemyType.Far:
            attackState = new FarAttackState(_AttackRange, _AttackCoolTime, _Projectile_SerialNum);
            break;
        }
        navMeshAgent.stoppingDistance = attackState.AttackRange;
        runState = new RunState(_Speed);
    }
    }