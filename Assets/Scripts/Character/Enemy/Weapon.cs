using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Target = "Player";
    public int Damage = 5;
    public ICharacterData root;
    Weapon(int _Damage)
    {

    }

    private void Start()
    {
        root = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<ICharacterData>();
        root.Attacking = false;
    }
    private void Update()
    {
        Debug.Log(root.Attacking);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (root.Attacking == true)
        {
            if (other.tag == Target)
            {
                other.GetComponent<ICharacterData>().Damaged(Damage);
            }
        }
    }
}
