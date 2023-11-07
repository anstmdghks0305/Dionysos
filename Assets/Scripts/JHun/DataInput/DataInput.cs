using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataInput : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private string userName;
    [SerializeField] private Text NameResult;
    [SerializeField] private Color passibleColor;
    [SerializeField] private Color duplicationColor;
    private bool passible;
    public void UserNameSet()
    {
        userName = inputText.text;
        GameManager.Instance.UserData.Add(new StagePlayer(userName));
        if (GameManager.Instance.UserData.Count != GameManager.Instance.UserData.Distinct().Count())
        {
            DuplicationName();
        }
        else
        {
            PassibleName();
        }
    }

    void DuplicationName()
    {
        NameResult.text = "닉네임이 이미 존재합니다.";
        NameResult.color = duplicationColor;
        passible = false;
        GameManager.Instance.UserData.RemoveAt(GameManager.Instance.UserData.Count-1);
    }

    void PassibleName()
    {
        NameResult.text = "닉네임을 사용할 수 있습니다.";
        NameResult.color = passibleColor;
        passible = true;
        GameManager.Instance.UserData.RemoveAt(GameManager.Instance.UserData.Count - 1);
    }

    public void NameFinal()
    {
        if(passible)
        {
            GameManager.Instance.UserData.Add(new StagePlayer(userName));
            SceneManager.LoadScene("StageSelect");
        }
    }
}
