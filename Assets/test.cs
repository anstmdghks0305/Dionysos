using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    float scale;
    void Start()
    {
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, transform.localScale.y);

        if(Input.GetKeyDown(KeyCode.H))
        {
            scale-=0.2f;
        }
    }
}
