using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RankData : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int score;
    public Text nameText;
    public Text scoreText;
    private void Start()
    {
        name = GameManager.Instance.UserData[GameManager.Instance.UserData.Count - 1].Name;
        for (int i = 0; i < GameManager.Instance.Stages.Count; i++)
        {
            var key = GameManager.Instance.Stages.FirstOrDefault(x => x.Value.stageIndex == i).Key;
            score += GameManager.Instance.Stages[key].ResultScore;
        }
        nameText.text = "닉네임 : " + name;
        scoreText.text = "최종 점수 : " + score.ToString();
    }
}
