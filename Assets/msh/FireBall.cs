using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Time.deltaTime * 10;
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
