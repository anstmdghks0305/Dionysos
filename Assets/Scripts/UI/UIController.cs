using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIController:MonoBehaviour
{
    public string Name;
    public Action<Data> action;
    public BarUI bar;
    public TextUI text;

    private void Start()
    {
        action += bar.UIUpdate;
        if(text !=null)
            action += text.UIUpdate;
        //FeverEvent += fever.UIUpdate;
    }
    public void doUIEvent(object sender, EventData data)
    {
        if (data.EventName.Equals(Name))
        {
            action(data.data);
        }
    }


}
