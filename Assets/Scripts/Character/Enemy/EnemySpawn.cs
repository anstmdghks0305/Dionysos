using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<Enemy> enemies;
    private int CoolTime = 2000;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnCoolTime().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async UniTask EnemySpawnCoolTime()
    {
        foreach (Enemy enemy in enemies)
        {
            await UniTask.Delay(CoolTime);
            EnemyController.Instance.EnemyPooling(transform, enemy);
        }
    }
}
