using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenCapture.CaptureScreenshot("Title.png");
    }

    private void OnMouseDown()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
