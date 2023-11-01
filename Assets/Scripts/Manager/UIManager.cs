using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    MainUI mainUI;
    public bool Active;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        Active = false;
        transform.GetChild(0).gameObject.SetActive(true);
        mainUI = transform.GetComponentInChildren<MainUI>();
        mainUI.gameObject.SetActive(Active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Active = !Active;
            mainUI.gameObject.SetActive(Active);
            GameManager.Instance.GameStop=Active;
        }
    }

    public void InStage(bool b)
    {
        mainUI.RetryActive(b);
    }
}
