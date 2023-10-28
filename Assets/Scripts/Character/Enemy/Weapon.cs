using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public string Target = "Player";
    public int Damage = 5;
    public ICharacterData root;
    private void Start()
    {
        root = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<ICharacterData>();
        Damage = root.Damage;
        root.Attacking = false;
    }
    private void Update()
    {
        if (root.Attacking && transform.GetComponent<BoxCollider>().enabled == false)
            transform.GetComponent<BoxCollider>().enabled = true;
        else if (!root.Attacking && transform.GetComponent<BoxCollider>().enabled == true)
            transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target) && root.Attacking)
        {
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
    }

    //�̰� �ȵ�
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (root.Attacking == true)
    //    {
    //        if (other.tag == Target)
    //        {
    //            other.GetComponent<ICharacterData>().Damaged(Damage);
    //        }
    //    }
    //}
}
