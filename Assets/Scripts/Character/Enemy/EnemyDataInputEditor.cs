using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;

public static class EnemyDataInputer
{
    public static List<GameObject> EnemyList
    { get; private set; }

    public static Enemy FindEnemy(Enemy enemy)
    {
        foreach (GameObject target in EnemyDataInputer.EnemyList)
        {
            if (enemy.GetComponent<Enemy>().SerialNum == target.GetComponent<Enemy>().SerialNum)
                return target.GetComponent<Enemy>();
        }
        return null;
    }

    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = null;
        System.Reflection.FieldInfo[] fields;
        foreach (GameObject target in EnemyDataInputer.EnemyList)
        {
            if (destination.GetComponent<Enemy>().SerialNum == target.GetComponent<Enemy>().SerialNum)
            {
                if (!destination.TryGetComponent<Component>(out copy))
                    copy = destination.AddComponent(type);
                fields = type.GetFields();
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(copy, field.GetValue(original));
                }
            }
        }
        return copy as T;
    }

    public static void EnemyDataInput(List<int> UseMob)
    {
        List<Dictionary<string, object>> EenemyDataCsv = CSVReader.Read("EnemyData");
        EnemyList = new List<GameObject>();
        List<int> UseProjectile= new List<int>();
        for (int i = 0; i < EenemyDataCsv.Count; i++)
        {
            foreach (GameObject obj in Resources.LoadAll<GameObject>("Enemy"))
            {
                if (EenemyDataCsv[i]["EnemyName"].ToString() == obj.name)
                {
                    int _SerialNum = Convert.ToInt32(EenemyDataCsv[i]["SerialNum"]);
                    if (UseMob.Contains(_SerialNum))
                    {
                        EnemyType _Type = EenemyDataCsv[i]["Type"].ToString() == "Near" ? EnemyType.Near : EnemyType.Far;
                        int _Hp = Convert.ToInt32(EenemyDataCsv[i]["Hp"]);
                        int _Speed = Convert.ToInt32(EenemyDataCsv[i]["Speed"]);
                        int _Damage = Convert.ToInt32(EenemyDataCsv[i]["Damage"]);
                        int _AttackRange = Convert.ToInt32(EenemyDataCsv[i]["AttackRange"]);
                        int _AttackCoolTime = Convert.ToInt32(EenemyDataCsv[i]["AttackCoolTime"]);
                        int _Projectile_SerialNum = Convert.ToInt32(EenemyDataCsv[i]["Projectile_SerialNum"]);
                        int _Score = Convert.ToInt32(EenemyDataCsv[i]["Score"]);
                        if (!UseProjectile.Contains(_Projectile_SerialNum))
                            UseProjectile.Add(_Projectile_SerialNum);
                        obj.GetComponent<Enemy>().Initailize(_Type, _SerialNum, _Hp, _Speed, _Damage, _AttackRange, _AttackCoolTime, _Projectile_SerialNum, _Score);
                        GameObject temp = EnemyController.Instance.FristPooling(obj.GetComponent<Enemy>());
                        temp.GetComponent<Enemy>().Initailize(_Type, _SerialNum, _Hp, _Speed, _Damage, _AttackRange, _AttackCoolTime, _Projectile_SerialNum, _Score);
                        EnemyList.Add(temp);
                        temp.SetActive(false);
                        EnemyController.Instance.EnemyDiePooling(temp.GetComponent<Enemy>());
                        break;
                    }
                }
            }
        }
        ProjectileInputer.ProjectileDataInput(UseProjectile);
    }
}


public static class ProjectileInputer
{
    public static List<GameObject> ProjectileList
    { get; private set; }
    public static Projectile FindProjectile(Projectile projectile)
    {
        foreach (GameObject target in ProjectileInputer.ProjectileList)
        {
            if (projectile.GetComponent<Projectile>().SerialNum == target.GetComponent<Projectile>().SerialNum)
            {
                return target.GetComponent<Projectile>();
            }
        }
        return null;
    }

    public static void ProjectileDataInput(List<int> UseProjectile)
    {
        ProjectileList = new List<GameObject>();
        List<Dictionary<string, object>> ProjectileDataCsv = CSVReader.Read("ProjectileData");
        for (int i = 0; i < ProjectileDataCsv.Count; i++)
        {
            foreach (GameObject obj in Resources.LoadAll<GameObject>("Projectile"))
            {
                if (ProjectileDataCsv[i]["ProjectileName"].ToString() == obj.name)
                {
                    int _SerialNum = Convert.ToInt32(ProjectileDataCsv[i]["SerialNum"]);
                    if (UseProjectile.Contains(_SerialNum))
                    {
                        string type = ProjectileDataCsv[i]["Type"].ToString();
                        float _Speed = (float)Convert.ToDouble(ProjectileDataCsv[i]["Speed"]);
                        int _Damage = Convert.ToInt32(ProjectileDataCsv[i]["Damage"]);
                        if (type == "Thrust")
                        {
                            int _Damage2 = Convert.ToInt32(ProjectileDataCsv[i]["SecondDamage"]);
                            obj.GetComponent<ThrustProjectile>().Initialize(_SerialNum, _Speed, _Damage, _Damage2);
                            Projectile temp = ProjectileController.Instance.FirstPooling(obj.GetComponent<ThrustProjectile>());
                            temp.GetComponent<ThrustProjectile>().Initialize(_SerialNum, _Speed, _Damage, _Damage2);
                            ProjectileList.Add(temp.gameObject);
                            ProjectileController.Instance.UsedProjectilePooling(temp);
                            break;
                        }
                        else if (type == "Explosion")
                        {
                            int _Damage2 = Convert.ToInt32(ProjectileDataCsv[i]["SecondDamage"]);
                            obj.GetComponent<ExplosionProjectile>().Initialize(_SerialNum, _Speed, _Damage, _Damage2);
                            Projectile temp = ProjectileController.Instance.FirstPooling(obj.GetComponent<ExplosionProjectile>());
                            temp.GetComponent<ExplosionProjectile>().Initialize(_SerialNum, _Speed, _Damage, _Damage2);
                            ProjectileList.Add(temp.gameObject);
                            ProjectileController.Instance.UsedProjectilePooling(temp);
                            break;
                        }
                        else
                        {
                            obj.GetComponent<Projectile>().Initialize(_SerialNum, _Speed, _Damage);
                            Projectile temp = ProjectileController.Instance.FirstPooling(obj.GetComponent<Projectile>());
                            temp.GetComponent<Projectile>().Initialize(_SerialNum, _Speed, _Damage);
                            ProjectileList.Add(temp.gameObject);
                            ProjectileController.Instance.UsedProjectilePooling(temp);
                            break;
                        }
                    }

                }
            }
        }
    }
}
