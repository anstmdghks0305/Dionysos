using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image slashPanel;
    [SerializeField] Image dashPanel;
    [SerializeField] Image fireballPanel;
    bool slashInit;
    bool dashInit;
    bool fireballInit;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().fever)
        {
            slashPanel.fillAmount = 0;
            dashPanel.fillAmount = 0;
            fireballPanel.fillAmount = 0;
        }
        else
        {
            if (player.GetComponent<Slash>().coolTime <= player.GetComponent<Slash>()._coolTime)
            {
                if (!slashInit)
                {
                    slashPanel.fillAmount = 100;
                    slashInit = true;
                }

                slashPanel.fillAmount -= Time.deltaTime / player.GetComponent<Slash>()._coolTime;


                //if (GetComponent<Image>().fillAmount < 1f)
            }
            else if (player.GetComponent<Slash>().coolTime >= player.GetComponent<Slash>()._coolTime)
            {
                slashPanel.fillAmount = 0;
                slashInit = false;
            }

            if (player.GetComponent<Dash>().coolTime <= player.GetComponent<Dash>()._coolTime)
            {
                if (!dashInit)
                {
                    dashPanel.fillAmount = 100;
                    dashInit = true;
                }

                dashPanel.fillAmount -= Time.deltaTime / player.GetComponent<Dash>()._coolTime;

                Debug.Log(Time.deltaTime);

                //if (GetComponent<Image>().fillAmount < 1f)
            }
            else if (player.GetComponent<Dash>().coolTime >= player.GetComponent<Dash>()._coolTime)
            {
                dashPanel.fillAmount = 0;
                dashInit = false;
            }

            if (player.GetComponent<Player>().fireBallTime <= player.GetComponent<Player>().fireBallCoolTime)
            {
                if (!fireballInit)
                {
                    fireballPanel.fillAmount = 100;
                    fireballInit = true;
                }

                fireballPanel.fillAmount -= Time.deltaTime / player.GetComponent<Player>().fireBallCoolTime;

                //if (GetComponent<Image>().fillAmount < 1f)
            }
            else if (player.GetComponent<Player>().fireBallTime > player.GetComponent<Player>().fireBallCoolTime)
            {
                fireballPanel.fillAmount = 0;
                fireballInit = false;
            }
        }

        //FillManage(dashPanel, player.GetComponent<Dash>().coolTime, player.GetComponent<Dash>()._coolTime, dashInit);
    }
}
