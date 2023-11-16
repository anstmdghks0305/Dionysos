using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Boss;
using System;
using System.Linq;
using UnityEngine.UI;

[Serializable]
public class User
{
    public string name;
    public int score;
}

[Serializable]
public class UserData
{
    public List<User> datas = new List<User>();

    public void Copy(string _name, int _score)
    {

    }
}
[Serializable]
public class PlayerData
{
    public int hp;
    public int fever;
    public int speed;
    public float attackSpeed;
    public float dashDistance;
    public int attackDamage;
    public int powerAttackDamage;
    public int dashDamage;
    public int powerDashDamage;
    public float dashCoolTime;
    public int slashDamage;
    public int powerSlashDamage;
    public float slashCoolTime;
    public int slashCount;
    public int fireballDamage;
    public int powerFireballDamage;
    public float fireballCoolTime;
    public float hurtTime;
    public float feverTime;
    public int powerAttackRange;

    public PlayerData(int _hp, int _fever, int _speed, float _attackSpeed, float _dashDistance, int _attackDamage, int _powerAttackDamage, int _DashDamage, int _powerDashDamage 
        , float _dashCoolTime, int _slashDamage, int _powerSlashDamage, float _slashCoolTime, int _slashCount, int _fireballDamage, int _powerFireballDamage, float _fireballCooltime, float _hurtTime, float _feverTime, int _powerAttackRanage)
    {
        this.hp = _hp;
        this.fever = _fever;
        this.speed = _speed;
        this.attackSpeed = _attackSpeed;
        this.dashDistance = _dashDistance;
        this.attackDamage = _attackDamage;
        this.powerAttackDamage = _powerAttackDamage;
        this.dashDamage = _DashDamage;
        this.powerDashDamage = _powerDashDamage;
        this.dashCoolTime = _dashCoolTime;
        this.slashDamage = _slashDamage;
        this.powerSlashDamage = _powerSlashDamage;
        this.slashCoolTime = _slashCoolTime;
        this.slashCount = _slashCount;
        this.fireballDamage = _fireballDamage;
        this.powerFireballDamage = _powerFireballDamage;
        this.fireballCoolTime = _fireballCooltime;
        this.hurtTime = _hurtTime;
        this.feverTime = _feverTime;
        this.powerAttackRange = _powerAttackRanage;
    }
}
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
    public PlayerData playerData;
    public UserData saveData;
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
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        saveData = new UserData();
        saveData = RankData.RankLoad();
        DontDestroyOnLoad(gameObject);
        if (BossData.Instance != null)
            BossData.Instance.Read();
        Screen.SetResolution(1920, 1080, true);  
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene("EndingScene");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                CurrentStage.CurrentScore += 100;
            }

            else if (SceneManager.GetActiveScene().name == "EndingScene")
            {
                if (Input.GetKeyDown(KeyCode.M))
                    Application.Quit();
            }
        }
    }

    public void EndStage(bool clear)
    {
        if(Stages[CurrentStage.StageName].ResultScore < Stages[CurrentStage.StageName].CurrentScore)
        {
            saveData.datas[saveData.datas.Count - 1].score -= Stages[CurrentStage.StageName].ResultScore;
            Stages[CurrentStage.StageName].ResultScore = Stages[CurrentStage.StageName].CurrentScore;
            saveData.datas[saveData.datas.Count - 1].score += Stages[CurrentStage.StageName].ResultScore;
        }
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
