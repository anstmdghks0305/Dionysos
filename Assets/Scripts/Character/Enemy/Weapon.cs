using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        if (name == "공격판정")
        {
            root = transform.parent.parent.GetComponent<ICharacterData>();
            //player = transform.parent.parent.GetComponent<Player>();
            //Damage = player.Damage;
        }
        else
            root = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<ICharacterData>();
        root.Attacking = false;
    }
    private void Update()
    {
        if (root.Attacking && transform.GetComponent<BoxCollider>().enabled == false)
            transform.GetComponent<BoxCollider>().enabled = true;
        else if (!root.Attacking&& transform.GetComponent<BoxCollider>().enabled == true)
            transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target)&&root.Attacking)
        {
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
        else if(other.tag == "Projectile")
        {
            if (name == "공격판정")
            {
                ProjectileController.Instance.UsedProjectilePooling(other.GetComponent<Projectile>());
                other.gameObject.SetActive(false);
            }
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
