using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    float RunSpeed;
    float AnimationTime;

    public RunState(int _RunSpeed)
    {
        RunSpeed = _RunSpeed;
    }

    public void Work(IEnemy characterData, Transform target)
    {
        characterData.state = State.Move;
        characterData.navMeshAgent.SetDestination(target.position);
        characterData.navMeshAgent.stoppingDistance = characterData.AttackRange;
        characterData.navMeshAgent.avoidancePriority = 51;
        characterData.navMeshAgent.isStopped = false;
        characterData.animator.SetBool("Run", true);
    }

}
