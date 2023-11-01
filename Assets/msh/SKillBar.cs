using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillBar : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image slashPanel;
    [SerializeField] Image dashPanel;
    bool slashInit;
    bool dashInit;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Slash>().coolTime <= player.GetComponent<Slash>()._coolTime)
        {
            if (!slashInit)
            {
                slashPanel.fillAmount = 0;
                slashInit = true;
            }

            slashPanel.fillAmount += Time.deltaTime / player.GetComponent<Slash>()._coolTime;


            //if (GetComponent<Image>().fillAmount < 1f)
        }
        else if (player.GetComponent<Slash>().coolTime >= player.GetComponent<Slash>()._coolTime)
        {
            slashPanel.fillAmount = 100;
            slashInit = false;
        }

        if (player.GetComponent<Dash>().coolTime <= player.GetComponent<Dash>()._coolTime)
        {
            if (!dashInit)
            {
                dashPanel.fillAmount = 0;
                dashInit = true;
            }

            dashPanel.fillAmount += Time.deltaTime / player.GetComponent<Dash>()._coolTime;


            //if (GetComponent<Image>().fillAmount < 1f)
        }
        else if (player.GetComponent<Dash>().coolTime >= player.GetComponent<Dash>()._coolTime)
        {
            dashPanel.fillAmount = 100;
            dashInit = false;
        }
        //FillManage(dashPanel, player.GetComponent<Dash>().coolTime, player.GetComponent<Dash>()._coolTime, dashInit);
    }
}
