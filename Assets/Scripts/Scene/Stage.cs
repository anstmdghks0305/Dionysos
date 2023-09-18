using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    
    public int StageBuildIndex;
    public bool active = false;
    // Start is called before the first frame update
    private void Start()
    {
        StageController.action += this.ReceiveActive;
    }
    public void Select()
    {
        if (active == false)
        {
            StageController.action(this);
            this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            SceneManager.LoadScene(StageBuildIndex);
            
        }
    }

    public void ReceiveActive(Stage stage)
    {
        if (stage == this)
            active = true;
        else
        {
            active = false;
            this.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        }
    }


}
