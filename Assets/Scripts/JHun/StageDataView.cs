using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataView : MonoBehaviour
{
    [SerializeField]
    public List<StageData> StageView;

    private void Start()
    {
        StageView.Add(GameManager.Instance.Stages["¿ì¸®ÀÇ ²Þ"]);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            StageView[0].CurrentScore++;
        }
    }
}