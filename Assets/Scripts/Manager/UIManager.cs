using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] MainUI mainUI;
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        Active = false;
        transform.GetChild(0).gameObject.SetActive(true);
        mainUI = transform.GetComponentInChildren<MainUI>();
        mainUI.gameObject.SetActive(Active);

        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            InStage(false);
        }
        else
        {
            InStage(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Active = !Active;
            mainUI.gameObject.SetActive(Active);
            if(mainUI.transform.GetChild(1).gameObject.activeSelf)
                mainUI.transform.GetChild(1).gameObject.SetActive(false);
            GameManager.Instance.GameStop=Active;
        }
    }

    public void InStage(bool b)
    {
        mainUI.RetryActive(b);
    }
}
