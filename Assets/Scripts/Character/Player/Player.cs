using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterData
{
    public Data Hp { private set; get; }
    public Data Fever{private set;get;}
    public int Speed{ set; get; }
    public int Damage{ set; get; }
    public int AttackSpeed{set; get;}
    public Animator animator{ set; get; }
    public bool Died;
    public State state{ set; get; }
    public EventController eventcontroller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp += (-5);
        eventcontroller.DoEvent(new EventData("Hp", Hp));
    }

    public void Start()
    {
        Hp = new Data(100);

    }


    public void Attack()
    {

    }

    public void Die()
    {
        Died = true;
    }
}
