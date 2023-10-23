using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Target = "Player";
    public int Damage = 5;
    public ICharacterData root;
    Player player;
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
        if (root.Attacking)
            transform.GetComponent<Collider>().enabled = true;
        else
            transform.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target))
        {
            //other.GetComponent<ICharacterData>().Damaged(Damage);
            other.GetComponent<Boss.BossController>().GetDamange(Damage);
        }
/*        else if(other.CompareTag(""))
        {
            if (name == "공격판정")
            {
                Destroy(other.gameObject);
            }
        }*/
    }

    //이거 안됨
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
