using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    public int Boss_Serial_Num;
    private EventController boss_Hp;
    public int Score;
    public bool First = false;
    public GameObject BossWarning;
    
    void Start()
    {
        BossWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (First == false)
        {
            if (GameManager.Instance.CurrentStage.CurrentScore >= Score)
            {
                StartCoroutine(BossActive());
            }
        }
    }

    IEnumerator BossActive()
    {
        First = true;
        if (GameManager.Instance.CurrentStage.maxScore != 999999)
            BossWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        if (GameManager.Instance.CurrentStage.maxScore != 999999)
            BossWarning.gameObject.SetActive(false);
        GameObject temp = EnemyController.Instance.EnemyPooling(this.transform.position, Boss_Serial_Num);
        boss_Hp = temp.GetComponent<Enemy>().eventcontroller;
        temp.GetComponent<Enemy>().isBoss = true;
        boss_Hp.transform.GetChild(1).GetComponent<Text>().text = temp.GetComponent<Enemy>().name;
    }

    
}
