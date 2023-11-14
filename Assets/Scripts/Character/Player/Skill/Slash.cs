using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, ISkill
{
    public float _coolTime;

    public bool CanUse { get; set; }
    public float coolTime { get; set; }
    public float maxTime { get; set; }
    public bool powerUp { get; set; }

    void Start()
    {
        coolTime = _coolTime;
    }
    void Update()
    {
        
        if(!CanUse)
        {
            if (coolTime <= _coolTime)
            {
                coolTime += Time.deltaTime;
            }
        }
    }

    public void Work(Player player)
    {
        if (CanUse)
        {
            player.AttackSpeed = 0.24f;

            if (powerUp)
            {
                player.weapon.Damage = player.slashPowerUpDamage;
                player.weapon.transform.localScale = player.newScale;
            }

            else
            {
                player.weapon.Damage = player.slashDefaultDamage;
                player.weapon.transform.localScale = player.scale;
            }

            StartCoroutine(SlashLogic(player));
        }
    }
    IEnumerator SlashLogic(Player player)
    {
        int index = 0;
        int index2 = 0;
        List<Enemy> pool = EnemyController.Instance.AliveEnemyPool;
        List<Enemy> e = new List<Enemy>();
        Enemy boss = null;

        if (pool.Count > 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                Vector3 enemyPoints = GameManager.Instance.MainCam.WorldToViewportPoint(pool[i].transform.position);

                if (enemyPoints.x > 0 && enemyPoints.x < 1 && enemyPoints.y > 0 && enemyPoints.y < 1)
                {
                    if(pool[i].isBoss)
                        boss = pool[i];
                    else
                        e.Add(pool[i]);
                }
            }
            if(boss != null)
            {
                e.Add(boss);
            }
        }

        if (pool.Count == 0 || e.Count == 0)
        {
            coolTime = 0;
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
                transform.position = new Vector3(e[index].transform.position.x - 0.3f, transform.position.y, e[index].transform.position.z);
                player.isFlip = true;
            }
            else
            {
                transform.position = new Vector3(e[index].transform.position.x + 0.3f, transform.position.y, e[index].transform.position.z);
                player.isFlip = false;
            }
            player.Attack();
            player.Effect.LightningEffect();
            player.Effect.NightEffect(true);
            if(powerUp)
            {
                Manager.SoundManager.Instance.PlaySFXSound("슉", 1);
                player.Effect.AttackEffect("Perfect");
            }
            else
            {
                Manager.SoundManager.Instance.PlaySFXSound("검격2", 1);
                player.Effect.AttackEffect("Bad");
            }

            if (index < e.Count - 1)
            {
                index++;
                index2++;
            }
            else if(index == e.Count - 1 )
            {
                if(e[index] != boss || index2 == player.slashMaxCount - 1)
                {
                    coolTime = 0;
                    player.slash = false;
                    player.AttackSpeed = player.defaultAttackSpeed;
                    powerUp = false;
                    CanUse = false;
                    player.Effect.NightEffect(false);
                    yield break;
                }
                else
                {
                    index2++;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    //private void OnEnable()
    //{
    //    maxTime = 0;
    //}

}
