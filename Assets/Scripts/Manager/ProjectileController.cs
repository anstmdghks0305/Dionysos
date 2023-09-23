using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Singleton<ProjectileController>
{

    List<Projectile> projectilePool = new List<Projectile>();
    Transform projectileParent;
    List<Projectile> UsedprojectilePool = new List<Projectile>();
    Transform usedprojectileParent;

    public void ProjectilePooling(Transform Pos, Projectile projectile)
    {
        Projectile temp = null;
        foreach (Projectile used in UsedprojectilePool)
        {
            if (used.SerialNum == projectile.SerialNum)
            {
                projectile.gameObject.transform.SetParent(projectileParent);
                temp = used;
                break;
            }
        }
        if (temp != null)
        {
            UsedprojectilePool.Remove(temp);
            projectilePool.Add(temp);
        }
        else
        {
            GameObject obj = GameObject.Instantiate(projectile.gameObject, Pos.position, Quaternion.Euler(EnemyController.Instance.player.gameObject.transform.position - Pos.position), projectileParent);
            obj.transform.SetParent(projectileParent);
            projectilePool.Add(obj.GetComponent<Projectile>());
        }
    }

    public void UsedProjectilePooling(Projectile projectile)
    {
        projectilePool.Remove(projectile);
        UsedprojectilePool.Add(projectile);
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
