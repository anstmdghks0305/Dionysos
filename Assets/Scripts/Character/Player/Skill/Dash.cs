using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISkill
{
    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }
    public bool powerUp { get; set; }

    public void Work(Player player)
    {
        if (CanUse)
        {
            StartCoroutine(SlashLogic(player));
        }
    }
    IEnumerator SlashLogic(Player player)
    {
        if ((player.vertical == 0f) && (player.horizontal == 0f))
        {
            if (player.isFlip)
                player.target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            else
                player.target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
        }
        else
        {
            if (player.vertical > 0f && player.horizontal == 0f)//¡è
                player.target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
            else if (player.vertical > 0f && player.horizontal > 0)//¢Ö
                player.target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z + 1.5f);
            else if (player.vertical == 0f && player.horizontal > 0)//¡æ
                player.target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            else if (player.vertical < 0f && player.horizontal > 0)//¢Ù
                player.target = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z - 1.5f);
            else if (player.vertical < 0f && player.horizontal == 0f)//¡é
                player.target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);
            else if (player.vertical < 0f && player.horizontal < 0)//¢×
                player.target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z - 1.5f);
            else if (player.vertical == 0f && player.horizontal < 0)//¡ç
                player.target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
            else if(player.vertical > 0f && player.horizontal < 0) //¢Ø
                player.target = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z + 1.5f);
            
        }

        float elapseTime = 0;
        while (elapseTime < 1)
        {
            elapseTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, player.target, elapseTime / 2);

            if (Vector3.Distance(player.target, transform.position) < 0.01f)
            {
                CanUse = false;
                player.dash = false;
                yield break;
            }
            yield return null;
        }
        yield return null;
    }

}
