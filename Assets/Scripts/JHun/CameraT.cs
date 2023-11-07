using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.MainCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
}
