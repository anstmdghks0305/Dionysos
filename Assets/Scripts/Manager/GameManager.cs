using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Boss;
using System;

[Serializable]
public struct StagePlayer
{
    public int num;
    public string Name;
    public int Score;
    public bool Active;
    public bool Clear;
    public StagePlayer(string _name, int _num)
    {
        this.num = _num;
        this.Name = _name;
        this.Score = 0;
        if (_name == "¿ì¸®ÀÇ ²Þ")
            this.Active = true;
        else
            this.Active = false;
        this.Clear = false;
    }
};
public class GameManager : Singleton<GameManager>
{
    public bool NotBuild;
    public Camera MainCam;
    public List<int> GameClearData = new List<int>();
    public bool GameStop = false;
    public Dictionary<string, StageData> Stages = new Dictionary<string, StageData>();
    public void StageSet(int _stageIndex, int _bpm, int _progress, int _maxScore, string _StageName, string _Difficult)
    {
        Stages.Add(_StageName, new StageData(_stageIndex, _bpm, _progress, _maxScore, _StageName, _Difficult));
        StageP.Add(_stageIndex, new StagePlayer(_StageName, _stageIndex));
        //ssview.Add(StageScore[_StageName]);
    }
    public Dictionary<int, StagePlayer> StageP = new Dictionary<int, StagePlayer>();
    //[SerializeField] private List<StagePlayer> ssview = new List<StagePlayer>();
    protected override void Awake()
    {
        if(!NotBuild)
            base.Awake();
        
        if(BossData.Instance != null)
            BossData.Instance.Read();
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //EnemyDataInputer.EnemyDataInput();
        try
        {
            ProjectileInputer.ProjectileDataInput();
        }
        catch (NullReferenceException ie)
        {

        }  
    }
    private void Update()
    { 
        if(SceneManager.GetActiveScene().name == "Title")
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void InStage(string name)
    {
        
    }
}
