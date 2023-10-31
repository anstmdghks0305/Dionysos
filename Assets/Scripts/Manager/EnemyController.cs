using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : Singleton<EnemyController>
{
    public List<Enemy> AliveEnemyPool = new List<Enemy>();
    Transform EnemyPool;
    public List<Enemy> DieEnemyPool = new List<Enemy>();
    Transform DiedEnemy;
    public Player player;

    private void Awake()
    {
        EnemyPool = this.transform.GetChild(0);
        DiedEnemy = this.transform.GetChild(1);
    }



    async UniTask StartAttacking()
    {
        while (true)
        {
            foreach (IEnemy enemy in AliveEnemyPool)
            {
                enemy.StateChange(player);
            }
            await UniTask.Delay(300);
        }
    }

    public GameObject FristPooling(Enemy enemy)
    {
        GameObject obj = Instantiate(enemy.gameObject);
        obj.name = enemy.name;
        obj.transform.SetParent(EnemyPool);
        AliveEnemyPool.Add(obj.GetComponent<Enemy>());
        return obj;
    }

    public void EnemyDiePooling(Enemy enemy)
    {
        AliveEnemyPool.Remove(enemy);
        DieEnemyPool.Add(enemy);
        enemy.gameObject.transform.SetParent(DiedEnemy);
    }

    public GameObject EnemyPooling(Vector3 Pos, int enemy)
    {
        Enemy temp = null;
        foreach (Enemy died in DieEnemyPool)
        {
            if (died.SerialNum == enemy)
            {
                died.gameObject.transform.SetParent(EnemyPool);
                died.Revive(Pos);
                DieEnemyPool.Remove(died);
                AliveEnemyPool.Add(died);
                temp = died;
                return died.gameObject;
            }
        }
        if (temp == null)
        {
            GameObject obj = null;
            foreach (GameObject target in EnemyDataInputer.EnemyList)
            {
                if (target.GetComponent<Enemy>().SerialNum == enemy)
                {
                    obj=Instantiate(target, Pos, Quaternion.Euler(player.gameObject.transform.position - Pos));
                    obj.name = target.name;
                    obj.transform.SetParent(EnemyPool);
                    AliveEnemyPool.Add(obj.GetComponent<Enemy>());
                    return obj;
                } 
            }
        }
        Debug.Log("생성 실패");
        return null;
    }




    // Start is called before the first frame update
    void Start()
    {
        StartAttacking().Forget();
        AliveEnemyPool.AddRange(GameObject.FindObjectsOfType<Enemy>());
        foreach (Enemy enemy in AliveEnemyPool)
        {
            enemy.gameObject.transform.SetParent(EnemyPool);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }


}
