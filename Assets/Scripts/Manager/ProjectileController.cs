using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Singleton<ProjectileController>
{

    Dictionary<int, Queue<Projectile>> projectiles = new Dictionary<int, Queue<Projectile>>();
    Transform projectileParent;
    Dictionary<int, Queue<Projectile>> Usedprojectiles = new Dictionary<int, Queue<Projectile>>();
    Transform usedprojectileParent;
    public Player Target;

    public void ProjectilePooling(Transform Pos, Projectile projectile)
    {
        if (!Usedprojectiles.ContainsKey(projectile.SerialNum))
        {
            Usedprojectiles.Add(projectile.SerialNum, new Queue<Projectile>());
        }
        if (!projectiles.ContainsKey(projectile.SerialNum))
        {
            projectiles.Add(projectile.SerialNum, new Queue<Projectile>());
        }
        if (Usedprojectiles[projectile.SerialNum].Count != 0)
        {
            Projectile temp = Usedprojectiles[projectile.SerialNum].Dequeue();
            temp.transform.SetParent(projectileParent);
            temp.DirectionControll(Target.transform);
            projectiles[projectile.SerialNum].Enqueue(temp);
        }
        else
        {
            Debug.Log("¹ÙºÎ");
            Projectile obj = GameObject.Instantiate(projectile.gameObject, Pos.position, Quaternion.Euler(EnemyController.Instance.player.gameObject.transform.position - Pos.position), projectileParent).transform.GetComponent<Projectile>();
            obj.transform.SetParent(projectileParent);
            obj.DirectionControll(Target.transform);
            projectiles[projectile.SerialNum].Enqueue(obj);
        }
    }

    public void UsedProjectilePooling(Projectile projectile)
    {
        Projectile temp = projectiles[projectile.SerialNum].Dequeue();
        Usedprojectiles[projectile.SerialNum].Enqueue(projectile);
        temp.transform.SetParent(usedprojectileParent);
    }


    // Start is called before the first frame update
    void Start()
    {
        projectileParent = this.transform.GetChild(0).transform;
        usedprojectileParent = this.transform.GetChild(1).transform;
    }

    public void Barrior(List<Projectile> projectiles)
    {
        foreach (Projectile p in projectiles)
        {
            UsedProjectilePooling(p);
        }
    }
}
