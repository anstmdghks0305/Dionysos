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

    public void ProjectilePooling(Transform Pos, int _Projectile_SerialNum)
    {
        if (!Usedprojectiles.ContainsKey(_Projectile_SerialNum))
        {
            Usedprojectiles.Add(_Projectile_SerialNum, new Queue<Projectile>());
        }
        if (!projectiles.ContainsKey(_Projectile_SerialNum))
        {
            projectiles.Add(_Projectile_SerialNum, new Queue<Projectile>());
        }
        if (Usedprojectiles[_Projectile_SerialNum].Count != 0)
        {
            Projectile temp = Usedprojectiles[_Projectile_SerialNum].Dequeue();
            temp.transform.SetParent(projectileParent);
            temp.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(temp);
        }
        else
        {
            Debug.Log("¹ÙºÎ");
            Projectile obj = GameObject.Instantiate(ProjectileInputer.ProjectileList[_Projectile_SerialNum], Pos.position, Quaternion.Euler(EnemyController.Instance.player.gameObject.transform.position - Pos.position), projectileParent).transform.GetComponent<Projectile>();
            obj.transform.SetParent(projectileParent);
            obj.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(obj);
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
