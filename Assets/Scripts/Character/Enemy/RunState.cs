using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    float RunSpeed;
    float AnimationTime;

    public void Work(IEnemy characterData, Transform target)
    {
        characterData.state = State.Move;
        characterData.navMeshAgent.SetDestination(target.position);
        characterData.navMeshAgent.isStopped = false;
        characterData.navMeshAgent.speed = RunSpeed;
        characterData.animator.SetBool("Run", true);
        EndAnimation(characterData).Forget();
    }

    async UniTaskVoid EndAnimation(ICharacterData characterData)
    {
        while (true)
        {
            await UniTask.Delay((int)(AnimationTime * 1000));
            characterData.animator.SetBool("Run", false);
            characterData.state = State.Idle;
        }
    }
}
