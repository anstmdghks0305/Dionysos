using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    [SerializeField]private int progress;
    [SerializeField]private int maxScore;
    [SerializeField]private int currentProgress;
    [SerializeField]private int currentScore;
    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = Mathf.Clamp(value, 0, maxScore);
        }
    }
    public int CurrentProgress
    {
        get => currentProgress;
        set
        {
            if(currentProgress >= progress - 1)
            {
                GameEnd();
                Clear = true;
            }
            currentProgress = Mathf.Clamp(value, 0, progress);
        }
    }
    public int ResultScore;
    public string StageName;
    public string Difficult;
    public bool Clear = false;
    public void GameEnd()
    {
        ResultScore = currentScore;
    }


    public StageData(int _progress, int _maxScore, string _StageName, string _Difficult)
    {
        progress = _progress;
        maxScore = _maxScore;
        StageName = _StageName;
        Difficult = _Difficult;
    }
}
