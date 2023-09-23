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

    public Animator animator
    {
        get;
    }

    public State state
    {
        get;set;
    }
    public IState IState
    {
        get;set;
    }
    public void Attack();
    public void Move();
    public void Idle();
    public Transform where();
}
