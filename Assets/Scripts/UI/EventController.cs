using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    private event EventHandler<EventData> Event;
    public UIController Hp;
    public UIController Fever;

    private void Start()
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
  
}
