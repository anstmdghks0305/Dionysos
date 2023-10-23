using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Boss;

public class GameManager : Singleton<GameManager>
{
    public bool NotBuild;
    public Camera MainCam;
    public List<int> GameClearData = new List<int>();
    public bool GameStop = false;
    public Dictionary<string, StageData> Stages = new Dictionary<string, StageData>();
    private void Awake()
    {
        BossData.Instance.Read();
        Screen.SetResolution(1920, 1080, true);
    }
    private void Start()
    {
        MainCam = Camera.main;
        GameClearData.Add(2);
        EnemyDataInputer.EnemyDataInput();
    }
    private void Update()
    { 
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
