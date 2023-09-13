using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController:MonoBehaviour
{
    private event EventHandler<EventData> Event;
    public UIController PlayerHp;
    public UIController PlayerFever;

    private void Start()
    {
        Event = PlayerHp.doUIEvent;
        if (PlayerFever !=null)
            Event += PlayerFever.doUIEvent;
    }

    public void DoEvent(EventData data)
    {
        if (Event != null)
            Event.Invoke(this, data);
    }
  
}
