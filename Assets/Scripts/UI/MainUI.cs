using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StageSelect;

public class MainUI : MonoBehaviour
{
    [SerializeField] public StageController stageController;
    SoundControlUI soundControlUI;
    [SerializeField] private GameObject RetryButton;

    void Awake()
    {
        soundControlUI = transform.GetChild(1).GetComponent<SoundControlUI>();
        soundControlUI.gameObject.SetActive(false);
    }

    private void Start()
    {

    }
    public void SoundUIOpen()
    {
        soundControlUI.gameObject.SetActive(true);
    }

    public void GoInputScene(string input)
    {
        soundControlUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        stageController.uiManager.Active = false;
        GameManager.Instance.GameStop = false;
        if(stageController != null)
            stageController.uiManager.InStage(false);
        SceneManager.LoadScene(input);
    }

    public void Retry()
    {
        soundControlUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        if (stageController != null)
            stageController.uiManager.Active = false;
        GameManager.Instance.GameStop = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryActive(bool b)
    {
        RetryButton.SetActive(b);
    }

    public void ExitButtonDown()
    {
        if (stageController != null)
            stageController.uiManager.Active = false;
        this.gameObject.SetActive(false);
        GameManager.Instance.GameStop = false;
    }
}
