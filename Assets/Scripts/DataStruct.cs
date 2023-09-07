using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




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
}
public enum State
{
    Idle,
    Attack,
    Move,
    Die
}

public enum EnemyType
{
    Near,
    Far,
    Boss
}