using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmSync : MonoBehaviour
{
    public Text SyncText;
    private string str;


    void Update()
    {
        str = string.Format("{0:0.00}", RhythmManager.Instance.RhythmSyncValue);
        SyncText.text = str;
    }

    public void SyncInput(string input)
    {
        switch(input)
        {
            case "Up":
                RhythmManager.Instance.RhythmSyncValue += 0.01f;
                break;
            case "Down":
                RhythmManager.Instance.RhythmSyncValue -= 0.01f;
                break;
            case "Reset":
                RhythmManager.Instance.RhythmSyncValue = 0.75f;
                break;
            default:
                break;
        }
    }

    public void Exit(string input)
    {
        SceneManager.LoadScene(input);
    }
}
