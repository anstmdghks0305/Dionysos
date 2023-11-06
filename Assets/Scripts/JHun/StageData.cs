using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class StageData
{
    [SerializeField]private int progress;
    [SerializeField]private int maxScore;
    [SerializeField]private int currentProgress;
    [SerializeField]private int currentScore;
    [SerializeField]private bool clear;
    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            if (currentScore >= maxScore)
            {
                GameManager.Instance.EndStage(true);
            }
            //currentScore = Mathf.Clamp(value, 0, maxScore);
            
        }
    }
    public int CurrentProgress
    {
        get => currentProgress;
        set
        {
            if(currentProgress >= progress - 1)
            {
                //GameEnd();
                Clear = true;
            }
            currentProgress = Mathf.Clamp(value, 0, progress);
        }
    }
    public int stageIndex;
    public int bpm;
    public int ResultScore;
    public string StageName;
    public string Difficult;
    public bool Clear
    {
        get => clear;
        set
        {
            clear = value;
            if(clear)
            {
                if(stageIndex < GameManager.Instance.Stages.Count - 1)
                {
                    var key = GameManager.Instance.Stages.FirstOrDefault(x => x.Value.stageIndex == (stageIndex + 1)).Key;
                    GameManager.Instance.Stages[key].Active = true;
                }
            }
        }
    }
    public bool Active = false;

    public StageData(int _stageIndex, int _bpm, int _progress, int _maxScore, string _StageName, string _Difficult)
    {
        stageIndex = _stageIndex;
        bpm = _bpm;
        progress = _progress;
        maxScore = _maxScore;
        StageName = _StageName;
        Difficult = _Difficult;
        if (_StageName == "map1")
        {
            Active = true;
        }
    }
}
