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

    public void ProjectilePooling(Transform Pos, Projectile projectile)
    {
        if (Usedprojectiles[projectile.SerialNum].Count != 0)
        {
            Projectile temp = Usedprojectiles[projectile.SerialNum].Dequeue();
            temp.transform.SetParent(projectileParent);
            projectiles[projectile.SerialNum].Enqueue(temp);
        }
        else
        {
            GameObject obj = GameObject.Instantiate(projectile.gameObject, Pos.position, Quaternion.Euler(EnemyController.Instance.player.gameObject.transform.position - Pos.position), projectileParent);
            obj.transform.SetParent(projectileParent);
            projectiles[projectile.SerialNum].Enqueue(obj.GetComponent<Projectile>());
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
