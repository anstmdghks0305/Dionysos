using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashBar : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool init = false;
    // Update is called once per frame
    void Update()
    {
        //GetComponent<Image>().fillAmount -= Time.deltaTime / slash.coolTime;
        //if (player.GetComponent<Slash>().coolTime <= 0)
        //{
        //    GetComponent<Image>().fillAmount = 1;
        //    Debug.Log("사용가능");
        //}
        if (player.GetComponent<Slash>().coolTime <= player.GetComponent<Slash>()._coolTime)
        {
            if(!init)
            {
                GetComponent<Image>().fillAmount = 0;
                init = true;
            }

            GetComponent<Image>().fillAmount += Time.deltaTime / player.GetComponent<Slash>()._coolTime;

            
            //if (GetComponent<Image>().fillAmount < 1f)
        }
        else if (player.GetComponent<Slash>().coolTime >= player.GetComponent<Slash>()._coolTime)
        {
            init = false;
        }

    }
}
