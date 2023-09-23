using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventData:EventArgs
{
    public string EventName;
    public Data data;
    public EventData(string str,Data _data)
    {
        EventName = str;
        data = _data;
    }
}

//Hp,Fever(Max,Current 관리용)
public struct Data
{
    private int Current;
    private int Max;

    public Data(int value)
    {
        Max = value;
        Current = Max;
    }

    public static Data operator +(Data data, int value)
    {

        data.Current += value;
        return data;
    }
    public static Data operator -(Data data, int value)
    {
        data.Current -= value;
        return data;
    }

    public float ShowFillAmount()
    {
        return (float)Current / Max;
    }
    public int ShowCurrentHp()
    {
        return Current;
    }


    public string ShowText()
    {
        return Current.ToString() + " / " + Max.ToString();
    }
}
//상태
public enum State
{
    Idle,
    Attack,
    Move,
    Die,
    Stun
}


//몬스터 타입
public enum EnemyType
{
    Near,
    Far,
    Boss
}