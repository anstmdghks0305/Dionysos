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
            Debug.Log("투사체" + _Projectile_SerialNum + "죽어있음");
        }
        if (!projectiles.ContainsKey(_Projectile_SerialNum))
        {
            projectiles.Add(_Projectile_SerialNum, new Queue<Projectile>());
            Debug.Log("투사체" + _Projectile_SerialNum + "살아있음");
        }
        if (Usedprojectiles[_Projectile_SerialNum].Count != 0)
        {
            Projectile temp = Usedprojectiles[_Projectile_SerialNum].Dequeue();
            temp.transform.SetParent(projectileParent);
            temp.ReUse(Pos);
            temp.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(temp);
        }
        else
        {
            Projectile obj = GameObject.Instantiate(ProjectileInputer.ProjectileList[_Projectile_SerialNum], Pos.position + Vector3.up * 0.5f, Quaternion.Euler(90, 0, 0)).transform.GetComponent<Projectile>();
            obj.transform.SetParent(projectileParent);
            obj.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(obj);
        }
    }

    public void UsedProjectilePooling(Projectile projectile)
    {
        projectile = projectiles[projectile.SerialNum].Dequeue();
        Usedprojectiles[projectile.SerialNum].Enqueue(projectile);
        projectile.transform.SetParent(usedprojectileParent);
        projectile.gameObject.SetActive(false);
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
