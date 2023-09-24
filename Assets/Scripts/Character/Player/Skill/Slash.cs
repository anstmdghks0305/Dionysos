using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, ISkill
{
    public bool CanUse { get; set; }
    public float CoolTime { get; set; }
    public float RemainTime { get; set; }

    int slashT;
    int count = 5;
    public int index = 0;

    public List<GameObject> enemies;
    public GameObject[] e;
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
        e  =  GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < e.Length; i++)
        {
            Vector3 enemyPoints = GameManager.Instance.MainCam.WorldToViewportPoint(e[i].transform.position);

            if (enemyPoints.x > 0 && enemyPoints.x < 1
                && enemyPoints.y > 0 && enemyPoints.y < 1)
            {
                enemies.Add(e[i]);
            }
        }
        while (true)
        {
            if (GameManager.Instance.MainCam.WorldToViewportPoint(enemies[index].transform.position).x > 0.5f) //0.5f는 카메라의 절반이다
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
            player.Effect.LightningEffect();
            player.Effect.NightEffect(true);

            if (index < count)
            {
                index++;
            }
            else
            {
                player.Init = false;
                CanUse = false;
                index = 0;
                player.Effect.NightEffect(false);
                yield break;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void OnEnable()
    {
        RemainTime = 0;
    }
}
