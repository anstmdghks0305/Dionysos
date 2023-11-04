using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class FarAttackState : AttackState
{
    int Projectile_SerialNum;
    bool AfterAttackBool = false;

    public FarAttackState(int _AttackRange, float _AttackCoolTime,int? _Projectile_SerialNum) : base(_AttackRange, _AttackCoolTime)  
    {
        Projectile_SerialNum = (int)_Projectile_SerialNum;
        AttackType = 0.5f;
    }

    public override void Work(IEnemy enemy, Transform target)
    {
        if(CanAttack ==true)
            ProjectileController.Instance.ProjectilePooling(enemy.where(), Projectile_SerialNum);
        base.Work(enemy, target);
        if (CanAttack == false)
        {
            if (enemy.Attacking == false)
            {
                AfterAttack(enemy,target);
                if (enemy.navMeshAgent.stoppingDistance - enemy.navMeshAgent.remainingDistance< 0.1f)
                    AfterAttackBool = false;
            }
        }

    }

    public void AfterAttack(IEnemy enemy,Transform Target)
    {
        if (AfterAttackBool == true)
            return;
        enemy.navMeshAgent.isStopped = false;
        enemy.navMeshAgent.avoidancePriority = 51;
        Vector3 RandomMove = enemy.where().position +(enemy.where().position-Target.position).normalized;
        enemy.navMeshAgent.SetDestination(RandomMove);
        enemy.navMeshAgent.stoppingDistance = 0;
        AfterAttackBool = true;
        enemy.animator.SetFloat("RunState", 0.5f);
    }
}
