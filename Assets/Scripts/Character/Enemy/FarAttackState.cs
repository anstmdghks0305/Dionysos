using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackState : AttackState
{
    int Projectile_SerialNum;

    public FarAttackState(int _AttackRange, float _AttackCoolTime,int? _Projectile_SerialNum) : base(_AttackRange, _AttackCoolTime)  
    {
        Projectile_SerialNum = (int)_Projectile_SerialNum;
    }

    public override void Work(IEnemy enemy, Transform target)
    {
        base.Work(enemy, target);
        Debug.Log("FarState");
        ProjectileController.Instance.ProjectilePooling(enemy.where(), Projectile_SerialNum);
    }

}
