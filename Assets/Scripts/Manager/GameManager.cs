using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Boss;
using System;
public class GameManager : Singleton<GameManager>
{
    [Serializable]
    public struct StagePlayer
    {
        public string Name;
        public int Score;
        public bool Active;
        public bool Clear;
        public StagePlayer(string _name)
        {
            this.Name = _name;
            this.Score = 0;
            if(_name == "快府狼 厕")
                this.Active = true;
            else
                this.Active = false;
            this.Clear = false;
        }
    };

    public bool NotBuild;
    public Camera MainCam;
    public List<int> GameClearData = new List<int>();
    public bool GameStop = false;
    public Dictionary<string, StageData> Stages = new Dictionary<string, StageData>();
    public void StageSet(int _stageIndex, int _bpm, int _progress, int _maxScore, string _StageName, string _Difficult)
    {
        Stages.Add(_StageName, new StageData(_stageIndex, _bpm, _progress, _maxScore, _StageName, _Difficult)); 
        StageScore.Add(_StageName, new StagePlayer(_StageName));
        //ssview.Add(StageScore[_StageName]);
    }
    public Dictionary<string, StagePlayer> StageScore = new Dictionary<string, StagePlayer>();
    //[SerializeField] private List<StagePlayer> ssview = new List<StagePlayer>();
    [SerializeField] private MainUI mainUi;
    protected override void Awake()
    {
        if(!NotBuild)
            base.Awake();
        
        //BossData.Instance.Read();
        Screen.SetResolution(1920, 1080, true);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        MainCam = Camera.main;
        GameClearData.Add(0);
        GameClearData.Add(1);
        GameClearData.Add(2);
        EnemyDataInputer.EnemyDataInput();
        ProjectileInputer.ProjectileDataInput();
        Debug.Log(StageScore["快府狼 厕"].Active);
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

    public void InStage(string name)
    {
        
    }
}
