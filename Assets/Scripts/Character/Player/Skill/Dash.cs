using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISkill
{
    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }

    public void Work(Player player, List<GameObject> enemies)
    {

    }
    public void Work(Player player)
    {
        if(CanUse)
        {
            if (!player.Init)
            {
                if (player.isFlip)
                {
                    player.target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                }
                else
                {
                    player.target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                }
                player.Init = true;
            }

            transform.position = Vector3.Lerp(transform.position, player.target, Time.deltaTime * 7);

            if (Vector3.Distance(player.target, transform.position) < 0.1f)
            {
                CanUse = false;
                player.Init = false;
            }
        }
    }
}
