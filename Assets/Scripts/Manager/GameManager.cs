using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<int> GameClearData = new List<int>();
    public bool GameStop = false;
    private void Awake()
    {
        GameClearData.Add(0);
        GameClearData.Add(1);
    }
  public  Camera Main = Camera.main;
}
