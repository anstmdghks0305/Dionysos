using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    //public GameObject aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");

        Vector3 position = transform.position;
        //Vector3 aposition = aim.transform.position;
        position.x += hori * Time.deltaTime * 10f;
        position.z += verti * Time.deltaTime * 10f;
        //aposition.x += hori * Time.deltaTime * 10f;
        //aposition.y += verti * Time.deltaTime * 10f;
        transform.position = position;
        //aim.transform.position = aposition;
    }
}
