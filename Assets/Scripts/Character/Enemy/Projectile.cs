using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int SerialNum;
    private Vector3 Direction;
    private float Speed;
    private int Damage;

    public void OnEnable()
    {
        Speed = 3;
        Damage = 5;
    }

    public void DirectionControll(Transform targetpos)
    {
        Direction = targetpos.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += Direction * Speed*Time.deltaTime;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
            this.gameObject.SetActive(false);
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
    }
}
