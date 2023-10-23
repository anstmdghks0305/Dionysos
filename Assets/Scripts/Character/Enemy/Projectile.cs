using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int SerialNum;
    private Vector3 Direction;
    private float Speed;
    private int Damage;

    public void DirectionControl(Transform targetpos)
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

    public void Initialize(int serialNum, float speed, int damage)
    {
        SerialNum = serialNum;
        Speed = speed;
        Damage = damage;
    }
}
