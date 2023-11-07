using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Boss;
using System;
using System.Linq;
using UnityEngine.UI;

[Serializable]
public struct StagePlayer
{
    public string Name;
    public int Score;
    public bool Clear;
    public StagePlayer(string _name)
    {
        this.Name = _name;
        this.Score = 0;
        this.Clear = false;
    }
};
public class GameManager : Singleton<GameManager>
{
    public Fade Fade;
    public StageData CurrentStage;
    public bool NotBuild;
    public Camera MainCam;
    public List<int> GameClearData = new List<int>();
    public bool GameStop = false;
    public Dictionary<string, StageData> Stages = new Dictionary<string, StageData>();
    public void StageSet(int _stageIndex, int _bpm, int _progress, int _maxScore, string _StageName, string _Difficult)
    {
        Stages.Add(_StageName, new StageData(_stageIndex, _bpm, _progress, _maxScore, _StageName, _Difficult));
    }
    public List<StagePlayer> UserData = new List<StagePlayer>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (BossData.Instance != null)
            BossData.Instance.Read();
        Screen.SetResolution(1920, 1080, true);  
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("EndingScene");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentStage.CurrentScore += 10;
        }
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if(Input.anyKeyDown)
            {
                if (SceneManager.GetActiveScene().name == "EndingScene")
                    Application.Quit();
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void EndStage(bool clear)
    {
        if(Stages[CurrentStage.StageName].ResultScore < Stages[CurrentStage.StageName].CurrentScore)
            Stages[CurrentStage.StageName].ResultScore = Stages[CurrentStage.StageName].CurrentScore;
        Stages[CurrentStage.StageName].Clear = clear;
        StartCoroutine(gotoInputScene("StageSelect"));
    }

    IEnumerator gotoInputScene(string input)
    {
        Fade.Active(1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(input);
    }
}
