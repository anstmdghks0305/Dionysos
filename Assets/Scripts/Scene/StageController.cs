using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageController : MonoBehaviour
{
    public delegate void MyAction();
    public static Action<Stage> StageSelect;
    public static event Action<int> StageUnlock;
    public static MyAction StageDraw;

    private void Start()
    {
        foreach (int i in GameManager.Instance.GameClearData)
        {
            StageUnlock(i);
        }
        StageDraw();
    }
}
