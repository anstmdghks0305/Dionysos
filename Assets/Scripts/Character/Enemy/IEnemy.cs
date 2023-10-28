using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy : ICharacterData
{
    public EnemyType Type
    {
        get; set;
    }
    public void StateChange(Player player);
    public NavMeshAgent navMeshAgent { get; set; }
    public int AttackRange { get; set; }
}
