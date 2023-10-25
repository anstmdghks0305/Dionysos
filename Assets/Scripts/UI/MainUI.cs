using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    SoundControlUI soundControlUI;
    public short StageSceneBuildIndex = 1;

    public void Awake()
    {
        soundControlUI = transform.GetComponentInChildren<SoundControlUI>();
        soundControlUI.gameObject.SetActive(false);
    }
    public void SoundUIOpen()
    {
        soundControlUI.gameObject.SetActive(true);
    }

    public void GoStageScene()
    {
        soundControlUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        UIManager.Instance.Active = false;
        GameManager.Instance.GameStop = false;
        SceneManager.LoadScene(StageSceneBuildIndex);
    }

    public void ExitButtonDown()
    {
        UIManager.Instance.Active = false;
        this.gameObject.SetActive(false);
        GameManager.Instance.GameStop = false;
    }
}
