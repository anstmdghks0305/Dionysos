using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

public class StageController : MonoBehaviour
{
    public delegate void MyAction();
    public static Action<Stage> StageSelect;
    public static event Action<int> StageUnlock;
    public static MyAction StageDraw;
    float First;
    private void Start()
    {
        foreach (int i in GameManager.Instance.GameClearData)
        {
            StageUnlock(i);
        }
        StageDraw();
    }
    public void MouseDown()
    {
        First =  GetComponent<RectTransform>().position.x;
    }

    public void Move()
    {
        this.GetComponent<RectTransform>().position += Vector3.left*(Input.mousePosition.x-First);
        First = this.GetComponent<RectTransform>().position.x;
    }
}
