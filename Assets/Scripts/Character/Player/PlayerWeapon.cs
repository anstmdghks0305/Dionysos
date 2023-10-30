using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    public int Damage = 5;
    ICharacterData root;

    private void Start()
    {
        root = transform.parent.parent.GetComponent<ICharacterData>();
        root.Attacking = false;
    }
    private void Update()
    {
        if (root.Attacking)
            transform.GetComponent<BoxCollider>().enabled = true;
        else if (!root.Attacking)
            transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
        else if (other.CompareTag("Projectile") || other.name == "ExplosionArrow(Clone)")
        {
            ProjectileController.Instance.UsedProjectilePooling(other.GetComponent<Projectile>());
            other.gameObject.SetActive(false);
        }

    }
}
