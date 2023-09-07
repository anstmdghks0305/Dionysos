using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController:MonoBehaviour
{
    public delegate void Fuc(Data data);
    public static Fuc HpEvent;
    public static Fuc FeverEvent;
    public BarUI hp;
    public BarUI fever;

    private void Start()
    {
        HpEvent += hp.UIUpdate;
        FeverEvent += fever.UIUpdate;
    }



}
