using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int SerialNum;
    public Vector3 Direction;
    private float Speed;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += Direction * Speed;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
            this.gameObject.SetActive(false);
        }
    }
}
