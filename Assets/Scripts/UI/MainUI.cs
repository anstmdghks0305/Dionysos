using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    SoundControlUI soundControlUI;
    [SerializeField] private GameObject RetryButton;

    public void Awake()
    {
        soundControlUI = transform.GetChild(1).GetComponent<SoundControlUI>();
        soundControlUI.gameObject.SetActive(false);
    }
    public void SoundUIOpen()
    {
        soundControlUI.gameObject.SetActive(true);
    }

    public void GoInputScene(string input)
    {
        soundControlUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        UIManager.Instance.Active = false;
        GameManager.Instance.GameStop = false;
        UIManager.Instance.InStage(false);
        SceneManager.LoadScene(input);
    }

    public void Retry()
    {
        soundControlUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        UIManager.Instance.Active = false;
        GameManager.Instance.GameStop = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryActive(bool b)
    {
        RetryButton.SetActive(b);
    }

    public void ExitButtonDown()
    {
        UIManager.Instance.Active = false;
        this.gameObject.SetActive(false);
        GameManager.Instance.GameStop = false;
    }
}
