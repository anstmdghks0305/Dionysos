using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun
{
    float StunTime;

    public void Work(IEnemy characterData)
    {
        characterData.state = State.Stun;
        characterData.navMeshAgent.isStopped = true;
        characterData.animator.SetBool("Stun" , true);
        EndAnimation(characterData).Forget();
    }

    async UniTaskVoid EndAnimation(ICharacterData characterData)
    {
        await UniTask.Delay((int)(StunTime * 1000));
        characterData.animator.SetBool("Stun", false);
        characterData.state = State.Idle;
    }
}
