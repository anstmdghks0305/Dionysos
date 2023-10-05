using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Target = "Player";
    public int Damage = 5;

    Weapon(int _Damage)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Target)
        {
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
    }
}
