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
    public List<string> dist = new List<string>();
    private bool passible;

    private void Start()
    {
        for(int i=0; i< GameManager.Instance.saveData.datas.Count; i++)
        {
            dist.Add(GameManager.Instance.saveData.datas[i].name);
        }
    }
    public void UserNameSet()
    {
        userName = inputText.text;
        
        dist.Add(userName);
        if (dist.Count != dist.Distinct().Count())
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
        //GameManager.Instance.saveData.name.RemoveAt(GameManager.Instance.saveData.name.Count-1);
        dist.RemoveAt(dist.Count - 1);
    }

    void PassibleName()
    {
        NameResult.text = "닉네임을 사용할 수 있습니다.";
        NameResult.color = passibleColor;
        passible = true;
        //GameManager.Instance.saveData.name.RemoveAt(GameManager.Instance.saveData.name.Count - 1);
        dist.RemoveAt(dist.Count - 1);
    }

    public void NameFinal()
    {
        if(passible)
        {
            User user = new User();
            user.name = userName;
            user.score = 0;
            GameManager.Instance.saveData.datas.Add(user);
            SceneManager.LoadScene("StageSelect");
        }
    }
}
