using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarUI: MonoBehaviour
{
    private Image Bar;
    // Start is called before the first frame update
    void Awake()
    {
        Bar = this.GetComponent<Image>();
        Bar.type = Image.Type.Filled;
    }

    public void UIUpdate(Data data)
    {
        Bar.fillAmount = data.ShowFillAmount();
        
    }
}
