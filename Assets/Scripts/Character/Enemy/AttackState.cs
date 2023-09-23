using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public int AttackRange { get; set; }
    public float AttackCoolTime;
    public float AnimationTime;
    private bool CanAttack;



    public virtual void Work(IEnemy characterData,Transform target)
    {
        characterData.navMeshAgent.isStopped = true;
        if (CanAttack==true)
        {
            CanAttack = false;
            characterData.animator.SetBool("Attack", true);
            StartAttacking().Forget();
            EndAnimation(characterData).Forget();
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
