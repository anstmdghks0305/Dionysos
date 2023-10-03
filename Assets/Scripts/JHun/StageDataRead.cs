using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class StageDataRead : MonoBehaviour
{
    public List<Dictionary<string, object>> StageDataCsv;
    void Awake()
    {
        StageDataCsv = CSVReader.Read("StageData");
        for (int i = 0; i < StageDataCsv.Count; i++)
        {
            int progress = Convert.ToInt32(StageDataCsv[i]["progress"]);
            int maxScore = Convert.ToInt32(StageDataCsv[i]["maxScore"]);
            string StageName = StageDataCsv[i]["StageName"].ToString();
            string Difficult = StageDataCsv[i]["Difficult"].ToString();

            GameManager.Instance.Stages.Add(StageName, new StageData(progress, maxScore, StageName, Difficult));
            Debug.Log($"로드 성공!{Difficult}");
        }
    }
}