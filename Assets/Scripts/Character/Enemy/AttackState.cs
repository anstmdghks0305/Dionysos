using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AttackState : IState
{
    protected float AttackType;
    public int AttackRange;
    public float AttackCoolTime;
    public bool CanAttack;
    CancellationTokenSource token;


    public AttackState(int _AttackRange, float _AttackCoolTime)
    {
        AttackType = 0;
        AttackRange = _AttackRange;
        AttackCoolTime = _AttackCoolTime;
        CanAttack = true;

        token = new CancellationTokenSource();

    }

    ~AttackState()
    {
        token.Cancel();
    }

    public virtual void Work(IEnemy characterData, Transform target)
    {
        if (CanAttack == true)
        {
            characterData.navMeshAgent.isStopped = true;
            characterData.animator.SetFloat("NormalState", AttackType);
            characterData.animator.SetTrigger("Attack");
            CanAttack = false;
            characterData.navMeshAgent.avoidancePriority = 1;
            StartAttacking(characterData).Forget();
            if (!characterData.Attacking)
            {
                AttackingDecision(characterData).Forget();
            }
        }
    }
    async UniTask StartAttacking(IEnemy characterData)
    {
        await UniTask.Delay((int)(AttackCoolTime * 1000), cancellationToken:token.Token);
        CanAttack = true;
    }

    async UniTask AttackingDecision(IEnemy characterData)
    {
        
        await UniTask.Delay(5);
        characterData.Attacking = true;
        await UniTask.Delay((int)(characterData.animator.GetCurrentAnimatorStateInfo(0).length * 1000), cancellationToken: token.Token);
        characterData.Attacking = false;
    }

}
