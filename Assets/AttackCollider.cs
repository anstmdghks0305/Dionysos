using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("enemy"))
        {
            Debug.Log("wow");
            collision.GetComponent<Enemy>().Stun();
        }
    }
}
