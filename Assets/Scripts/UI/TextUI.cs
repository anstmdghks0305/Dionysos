using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextUI : MonoBehaviour
{
    private Text text;
    void Awake()
    {
        text = this.GetComponent<Text>();
    }
    public void UIUpdate(Data data)
    {
        text.text = data.ShowText();
    }
}
