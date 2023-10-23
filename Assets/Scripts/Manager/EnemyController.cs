using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : Singleton<EnemyController>
{
    public List<Enemy> AliveEnemyPool = new List<Enemy>();
    Transform EnemyPool;
    List<Enemy> DieEnemyPool = new List<Enemy>();
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

    public void EnemyDiePooling(Enemy enemy)
    {
        AliveEnemyPool.Remove(enemy);
        DieEnemyPool.Add(enemy);
        enemy.gameObject.transform.SetParent(DiedEnemy, transform);
    }

    public void EnemyPooling(Transform Pos, Enemy enemy)
    {
        Enemy temp = null;
        foreach (Enemy died in DiedEnemy)
        {
            if (died.SerialNum == enemy.SerialNum)
            {
                enemy.gameObject.transform.SetParent(EnemyPool);
                enemy.animator.SetBool("Die", false);
                temp = died;
                break;
            }
        }
        if (temp != null)
        {
            DieEnemyPool.Remove(temp);
            AliveEnemyPool.Add(temp);
        }
        else
        {
            GameObject obj = GameObject.Instantiate(enemy.gameObject, Pos.position, Quaternion.Euler(EnemyController.Instance.player.gameObject.transform.position - Pos.position), EnemyPool);
            obj.transform.SetParent(EnemyPool);
            AliveEnemyPool.Add(obj.GetComponent<Enemy>());
        }
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
