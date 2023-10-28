using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    public string Target = "Player";
    public int Damage = 5;
    public ICharacterData root;
    PlayerWeapon(int _Damage)
    {

    }

    private void Start()
    {
        if (name == "AttackCol")
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
        else if (!root.Attacking && transform.GetComponent<BoxCollider>().enabled == true)
            transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target) && root.Attacking)
        {
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
        else if (other.tag == "Projectile")
        {
            if (name == "AttackCol")
            {
                ProjectileController.Instance.UsedProjectilePooling(other.GetComponent<Projectile>());
                other.gameObject.SetActive(false);
            }
        }
    }

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
