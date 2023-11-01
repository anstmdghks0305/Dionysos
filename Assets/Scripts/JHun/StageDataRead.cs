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
        for (int index = 0; index < StageDataCsv.Count; index++)
        {
            int bpm = Convert.ToInt32(StageDataCsv[index]["bpm"]);
            int progress = Convert.ToInt32(StageDataCsv[index]["progress"]);
            int maxScore = Convert.ToInt32(StageDataCsv[index]["maxScore"]);
            string StageName = StageDataCsv[index]["StageName"].ToString();
            string Difficult = StageDataCsv[index]["Difficult"].ToString();

            //GameManager.Instance.Stages1.Add(StageName, new StageData(index, bpm, progress, maxScore, StageName, Difficult));
            GameManager.Instance.StageSet(index, bpm, progress, maxScore, StageName, Difficult);
            Debug.Log($"로드 성공!{Difficult}");
        }
    }
}