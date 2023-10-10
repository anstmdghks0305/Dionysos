using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, ISkill
{
    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }

    int slashT;
    public int index = 0;

    public List<GameObject> enemies;
    //public List<Enemy> e;
    public EnemyController pool;
    public Slash()
    {
        CoolTime = 3;

    }
    void Update()
    {
        
    }
    public void Work(Player player)
    {
        Debug.Log("check");
        if (CanUse)
        {
            if (!player.Init)
                StartCoroutine(StartCorotin(player));
            player.Init = true;
        }
        //GameObject[] Enemys;
        //Player.position;
    }


    IEnumerator StartCorotin(Player player)
    {
        List<Enemy> e = pool.AliveEnemyPool;
        if(e.Count > 1)
        {
            for (int i = 0; i < e.Count; i++)
            {
                Vector3 enemyPoints = GameManager.Instance.MainCam.WorldToViewportPoint(e[i].transform.position);

                if (!(enemyPoints.x > 0 && enemyPoints.x < 1
                    && enemyPoints.y > 0 && enemyPoints.y < 1))
                {
                    e.RemoveAt(i);
                }
            }
        }
        else
        {
            Debug.Log("엜,");
            player.Init = false;
            CanUse = false;
            index = 0;
            player.Effect.NightEffect(false);
            yield break;
        }
        while (true)
        {
            if (GameManager.Instance.MainCam.WorldToViewportPoint(e[index].transform.position).x > 0.5f) //0.5f는 카메라의 절반이다
            {
                transform.position = new Vector3(e[index].transform.position.x - 1, e[index].transform.position.y, e[index].transform.position.z);
                player.isFlip = true;
            }
            else
            {
                transform.position = new Vector3(e[index].transform.position.x + 1, e[index].transform.position.y, e[index].transform.position.z);
                player.isFlip = false;
            }
            player.Attack();
            player.Effect.LightningEffect();
            player.Effect.NightEffect(true);

            
            if(e.Count < 1 || (index == e.Count - 1))
            {
                if(e.Count == 1)
                {
                    Debug.Log("zz");
                }
                Debug.Log("엜,");
                player.Init = false;
                CanUse = false;
                index = 0;
                player.Effect.NightEffect(false);
                yield break;
            }
            else if (index < e.Count - 1)
            {
                index++;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void OnEnable()
    {
        RemainTime = 0;
    }
}
