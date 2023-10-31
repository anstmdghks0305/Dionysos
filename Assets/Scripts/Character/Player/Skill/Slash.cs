using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slash : MonoBehaviour, ISkill
{

    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }

    public int index = 0;
    public EnemyController pool;

    public bool powerUp { get; set; }
    public Slash()
    {
        CoolTime = 3;
    }
    public void Work(Player player)
    {
        if (CanUse)
        {
            player.attackSpeed = 0.1f;
            StartCoroutine(StartCorotin(player));

            Debug.Log(player.powerUp);
        }
        //GameObject[] Enemys;
        //Player.position;
    }


    IEnumerator StartCorotin(Player player)
    {
        List<Enemy> e = new List<Enemy>();

        if (pool.AliveEnemyPool.Count > 0)
        {
            for (int i = 0; i < pool.AliveEnemyPool.Count; i++)
            {
                Vector3 enemyPoints = GameManager.Instance.MainCam.WorldToViewportPoint(pool.AliveEnemyPool[i].transform.position);

                if (enemyPoints.x > 0 && enemyPoints.x < 1
                    && enemyPoints.y > 0 && enemyPoints.y < 1)
                {
                    e.Add(pool.AliveEnemyPool[i]);
                }
            }
        }
        
        if(pool.AliveEnemyPool.Count == 0 || e.Count == 0)
        {
            CanUse = false;
            index = 0;
            player.Effect.NightEffect(false);
            player.Damage = player.defaultDamage;
            yield break;
        }

        
        while (true)
        {
            if (GameManager.Instance.MainCam.WorldToViewportPoint(e[index].transform.position).x > 0.5f) //0.5f�� ī�޶��� �����̴�
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
            
            
            if(index == player.slashMaxCount - 1 || 
                index >= e.Count - 1)
            {
                e.Clear();
                if(player.slash)
                {
                    player.Damage = 90;
                }
                else
                {
                    player.Damage = player.defaultDamage;
                }
                player.slash = false;
                player.attackSpeed = 0.5f;
                powerUp = false;
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
