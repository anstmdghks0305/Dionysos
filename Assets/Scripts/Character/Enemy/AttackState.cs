using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public int AttackRange;
    public float AttackCoolTime;
    private bool CanAttack;


    public AttackState(int _AttackRange,float _AttackCoolTime)
    {
        AttackRange = _AttackRange;
        AttackCoolTime = _AttackCoolTime;
        CanAttack = true;
    }

    public virtual void Work(IEnemy characterData,Transform target)
    {
        characterData.navMeshAgent.isStopped = true;
        if (CanAttack==true)
        {
            characterData.animator.SetTrigger("Attack");
            CanAttack = false;
            
            StartAttacking().Forget();
            AttackingDecision(characterData).Forget();
        }
    }

    async UniTask StartAttacking()
    {
        await UniTask.Delay((int)(AttackCoolTime * 1000));
        CanAttack = true;
    }

    async UniTask AttackingDecision(IEnemy characterData)
    {
        await UniTask.Delay(5);
        characterData.Attacking = true;
        await UniTask.Delay((int)(characterData.animator.GetCurrentAnimatorStateInfo(0).length*1000));
        characterData.Attacking = false;

    }

}
