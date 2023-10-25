using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventController:MonoBehaviour
{
    private event EventHandler<EventData> Event;
    public UIController Hp;
    public UIController Fever;
    private void OnEnable()
    {
        Event = Hp.doUIEvent;
        if (Fever !=null)
            Event += Fever.doUIEvent;
    }

    public void DoEvent(EventData data)
    {
        if (Event != null)
            Event.Invoke(this, data);
    }
    
    public void Filp(bool filp)
    {
        if (filp)
            Hp.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        else
            Hp.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
    }

  
}
