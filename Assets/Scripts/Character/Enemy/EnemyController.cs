using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : Singleton<EnemyController>
{
    Camera Cam;
    List<IEnemy> CurrentEnemyPool = new List<IEnemy>();
    List<IEnemy> DieEnemyPool = new List<IEnemy>();
    async UniTask StartAttacking()
    {
        while (true)
        {
            foreach (IEnemy enemy in EnemyPool);
            {
                enemy.Attack();
            }
            await UniTask.Delay(500);
        }
    }

    void EnemyPooling()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
