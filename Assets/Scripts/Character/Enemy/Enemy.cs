using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy
{
    public EnemyType Type { get; set; }
    public int SerialNum;
    public Data Hp{private set;get;}
    public int Speed { set; get; }
    public int Damage { set; get; }
    public Animator animator { set; get; }
    public bool Died;
    public State state { set; get; }
    public EventController eventcontroller;
    public NavMeshAgent navMeshAgent { get; set; }
    public IState IState { get; set; }
    public bool isFlip { get; set; }

    AttackState attackState;
    RunState runState;
    public GameObject Projectile = null;
    private void OnEnable()
    {
    }

    private void Start()
    {
        Hp = new Data(100);
        this.gameObject.tag = "enemy";
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        switch (Type)
        {
            case EnemyType.Near:
                attackState = new AttackState();
                break;
            case EnemyType.Far:
                attackState = new FarAttackState(Projectile);
                break;
        }
        navMeshAgent.stoppingDistance = attackState.AttackRange;
        runState = new RunState();
    }
    void Update()
    {
        if (GameManager.Instance.GameStop == true || state == State.Die)
            return;
    }

    private void OnDisable()
    {
        
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

    public void StateChange(Transform player)
    {
        if (state == State.Idle)
        {
            if (navMeshAgent.remainingDistance > attackState.AttackRange)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
        IState.Work(this, player);
    }


    public void Die()
    {
        Died = true;
        EnemyController.Instance.EnemyDiePooling(this);
        animator.SetBool("Die", true);
    }

    public Transform where()
    {
        return this.gameObject.transform;
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }
}