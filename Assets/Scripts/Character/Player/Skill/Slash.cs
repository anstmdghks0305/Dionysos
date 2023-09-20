using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, ISkill
{
    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }

    int slashT;

    int index = 0;

    public Slash()
    {
        CoolTime = 3;

    }
    public void Work(Player player, List<GameObject> enemies)
    {
        if (CanUse)
        {

            player.slashTime += Time.deltaTime;
            if (player.slashTime > 0.25f)
            {
                if (!player.Init)
                {
                    if (GameManager.Instance.Main.WorldToViewportPoint(enemies[index].transform.position).x > 0.5f) //0.5f는 카메라의 절반이다
                    {
                        transform.position = new Vector3(enemies[index].transform.position.x - 1, enemies[index].transform.position.y, enemies[index].transform.position.z);
                        player.isFlip = true;
                    }
                    else
                    {
                        transform.position = new Vector3(enemies[index].transform.position.x + 1, enemies[index].transform.position.y, enemies[index].transform.position.z);
                        player.isFlip = false;
                    }
                    player.Attack();
                }

                if (index < enemies.Count - 1)
                {
                    index++;
                }
                else
                {
                    player.Init = false;
                    CanUse = false;
                    player.slashTime = 0;
                    index = 0;
                }

                player.slashTime = 0;
                player.Init = false;
            }
        }
        //GameObject[] Enemys;
        //Player.position;

    }

    private void OnEnable()
    {
        RemainTime = 0;
    }
    public void Update()
    {
        RemainTime += Time.deltaTime;
        if (!CanUse && CoolTime < RemainTime)
            CanUse = true;

    }

    public void Work()
    {
        throw new System.NotImplementedException();
    }
}
