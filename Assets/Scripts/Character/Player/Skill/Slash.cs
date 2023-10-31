using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, ISkill
{

    public bool CanUse { get; set; }
    public float coolTime { get; set; }
    public float maxTime { get; set; }
    public bool powerUp { get; set; }

    void Start()
    {
        coolTime = 0;
    }
    void Update()
    {
        if(!CanUse)
        {
            if(coolTime >= 0)
            {
                coolTime -= Time.deltaTime;
            }
        }
    }

    public void Work(Player player)
    {
        if (CanUse)
        {
            player.AttackSpeed = 0.1f;

            if (powerUp)
            {
                player.weapon.Damage = 100;
                player.attackScale.localScale = player.scale;
            }

            else
            {
                player.weapon.Damage = 50;
                player.attackScale.localScale = player.scale;
            }

            StartCoroutine(SlashLogic(player));
        }
    }
    IEnumerator SlashLogic(Player player)
    {
        int index = 0;
        List<Enemy> pool = EnemyController.Instance.AliveEnemyPool;
        List<Enemy> e = new List<Enemy>();

        if (pool.Count > 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                Vector3 enemyPoints = GameManager.Instance.MainCam.WorldToViewportPoint(pool[i].transform.position);

                if (enemyPoints.x > 0 && enemyPoints.x < 1 && enemyPoints.y > 0 && enemyPoints.y < 1)
                    e.Add(pool[i]);
            }
        }

        if (EnemyController.Instance.AliveEnemyPool.Count == 0 || e.Count == 0)
        {
            coolTime = player.coolTime;
            player.slash = false;
            player.AttackSpeed = player.defaultAttackSpeed;
            powerUp = false;
            CanUse = false;
            player.Effect.NightEffect(false);
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
            if(powerUp)
                player.Effect.AttackEffect("Perfect");
            else
                player.Effect.AttackEffect("Bad");

            if (index == player.slashMaxCount - 1 ||
                index >= e.Count - 1)
            {
                Debug.Log("end");
                coolTime = player.coolTime;
                player.slash = false;
                player.AttackSpeed = player.defaultAttackSpeed;
                powerUp = false;
                CanUse = false;
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
    //private void OnEnable()
    //{
    //    maxTime = 0;
    //}
}
