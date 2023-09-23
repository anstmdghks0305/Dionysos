using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackState : AttackState
{
    GameObject projectile;

    public FarAttackState(GameObject projectile)
    {
        this.projectile = projectile;
    }

    public override void Work(IEnemy enemy, Transform target)
    {
        base.Work(enemy, target);
        ProjectileController.Instance.ProjectilePooling(enemy.where(), projectile.GetComponent<Projectile>());
    }

}
