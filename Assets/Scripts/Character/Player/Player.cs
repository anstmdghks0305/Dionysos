using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterData
{
    
    public Data Hp
    {
        set
        {

        }
        get
        {
            return this.Hp;
        }
    }


    public Data Fever
    {
        set
        {

        }
        get
        {
            return this.Fever;
        }
    }
    public int Speed
    {
        set;
        get;
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

    public bool Died;
    public State state
    {
        get; set;
    }
    public void OnTriggerEnter(Collider other)
    {
        Hp += (-5);
        PlayerUIController.HpEvent(Hp);
    }

    public void Attack()
    {

    }

    public void Die()
    {
        Died = true;
    }
}
