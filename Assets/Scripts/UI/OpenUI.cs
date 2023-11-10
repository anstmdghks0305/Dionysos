using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUI : MonoBehaviour
{
    bool Active=false;
    public Image TargetUI;

    public void ActiveUI()
    {
        Active = !Active;
        TargetUI.gameObject.SetActive(Active);
    }

}
