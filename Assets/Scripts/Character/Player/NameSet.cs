using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSet : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private Text nameText;
    void Start()
    {
        nameText = gameObject.GetComponent<Text>();
        name = GameManager.Instance.UserData[GameManager.Instance.UserData.Count - 1].Name;
        nameText.text = name;  
    }
}
