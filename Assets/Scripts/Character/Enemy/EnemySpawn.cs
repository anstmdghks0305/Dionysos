using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemySpawn : MonoBehaviour
{
    public List<int> enemies;
    public int CoolTime = 2000;
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
        foreach (int enemy in enemies)
        {
            if (GameManager.Instance.GameStop == true)
                await UniTask.WaitUntil(() => !GameManager.Instance.GameStop);
            await UniTask.Delay(CoolTime, cancellationToken: this.GetCancellationTokenOnDestroy());
            if (GameManager.Instance.GameStop == true)
                await UniTask.WaitUntil(() => !GameManager.Instance.GameStop);
            EnemyController.Instance.EnemyPooling(this.transform, enemy);
        }
    }
}
