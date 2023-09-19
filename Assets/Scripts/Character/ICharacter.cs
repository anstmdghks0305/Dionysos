using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public interface ICharacterData
{
    public Data Hp
    {
        get;
    }
    public int Speed
    {
        get;
        set;
    }
    public int Damage
    {
        get;
    }

    public int AttackSpeed
    {
        get;
    }

    public Animator animator
    {
        get;
    }

    public State state
    {
        get;set;
    }
    public void Attack();
    public void Move();
}
