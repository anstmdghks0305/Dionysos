using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    public int Boss_Serial_Num;
    public EventController boss_Hp;
    public Text bossName;
    public int Score;
    public bool First = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(First==false)
        {
            if (GameManager.Instance.CurrentStage.CurrentScore >= Score)
            {
                GameObject temp = EnemyController.Instance.EnemyPooling(this.transform.position, Boss_Serial_Num);
                temp.GetComponent<Enemy>().eventcontroller = boss_Hp;
                boss_Hp.gameObject.SetActive(true);
                boss_Hp.DoEvent(new EventData("Hp", temp.GetComponent<Enemy>().Hp));
                temp.GetComponent<Enemy>().isBoss = true;
                bossName.text= temp.GetComponent<Enemy>().name;
                First = true;
            }
        }
    }
}
