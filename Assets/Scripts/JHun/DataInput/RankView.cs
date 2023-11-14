using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class RankView : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int score;

    public Text nameText;
    public Text scoreText;
    public Text RankName;
    public Text RankScore;
    [SerializeField] string rankname;
    [SerializeField] string rankscore;
    private void Start()
    {
        RankData.RankSave(GameManager.Instance.saveData);
        name = GameManager.Instance.saveData.datas[GameManager.Instance.saveData.datas.Count - 1].name;
        UserData sortData = GameManager.Instance.saveData;
        sortData.datas.Sort((a, b) => b.score.CompareTo(a.score));
        
        for (int i = 0; i < GameManager.Instance.Stages.Count; i++)
        {
            var key = GameManager.Instance.Stages.FirstOrDefault(x => x.Value.stageIndex == i).Key;
            score += GameManager.Instance.Stages[key].ResultScore;
        }
        nameText.text = "닉네임 : " + name;
        scoreText.text = "최종 점수 : " + score.ToString();
        for(int i=0; i<5; i++)
        {
            if (i >= sortData.datas.Count)
            {
                break;
            }
            rankname += $"{i + 1}등  {sortData.datas[i].name}";
            rankname += "\n";
            rankscore += $"|            {sortData.datas[i].score}";
            rankscore += "\n";
        }
        RankName.text = rankname;
        RankScore.text = rankscore;
    }
}
