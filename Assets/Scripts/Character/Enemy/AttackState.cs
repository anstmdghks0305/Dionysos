using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public int AttackRange;
    public float AttackCoolTime;
    public float AnimationTime;
    private bool CanAttack;



    public AttackState()
    {
        AttackRange = 3;
        AttackCoolTime = 1;
        AnimationTime = 0.1f;
        CanAttack = true;
    }

    public virtual void Work(IEnemy characterData,Transform target)
    {
        characterData.navMeshAgent.isStopped = true;
        if (CanAttack==true)
        {
            Debug.Log("Attack");
            CanAttack = false;
            characterData.animator.SetTrigger("Attack");
            StartAttacking().Forget();
            //EndAnimation(characterData).Forget();
        }
    }

    async UniTask StartAttacking()
    {
        await UniTask.Delay((int)(AttackCoolTime * 1000));
        CanAttack = true;
    }

    async UniTaskVoid EndAnimation(ICharacterData characterData)
    {
        await UniTask.Delay((int)(AnimationTime * 1000));
        characterData.animator.SetBool("Attack", false);
        characterData.state = State.Idle;
    }

}
