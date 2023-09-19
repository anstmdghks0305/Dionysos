using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IEnemy
{
    public EnemyType Type { get; set; }
    public Data Hp { private set; get; }
    public int Speed { set; get; }
    public int Damage { set; get; }
    public int AttackSpeed { set; get; }
    public Animator animator { set; get; }
    public bool Died;
    public State state { set; get; }
    public EventController eventcontroller;
    private NavMeshAgent navMeshAgent;
    AttackState attackState = new AttackState();
    public void Start()
    {
        Hp = new Data(100);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackState.AttackRange;
    }
    void Update()
    {
        if (GameManager.Instance.GameStop == true)
            return;
    }

    public void Attack()
    {

    }
    public void Move()
    {
        
    }


    public void Die()
    {
        Died = true;
    }


    private void OnTriggerEnter(Collider collision)
    {
        Hp += (-5);
        eventcontroller.DoEvent(new EventData("Hp", Hp));
    }

    public void SetTheTarget(Transform transform)
    {

    }

}
