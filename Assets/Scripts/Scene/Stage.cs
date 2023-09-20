using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{

    public int StageBuildIndex;
    private bool active = false;
    public bool Locked = true;
    // Start is called before the first frame update
    private void Awake()
    {
        StageController.StageSelect += this.ReceiveActive;
        StageController.StageUnlock += this.ReceiveUnlocked;
        this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f); //락걸린 이미지
    }
    private void Start()
    {
    }
    public void Select()
    {
        if (Locked == true)
            return;
        else if (active == false)
        {
            StageController.StageSelect(this);
            StageController.StageDraw();
        }
        else
            SceneManager.LoadScene(StageBuildIndex);
    }

    public void ReceiveActive(Stage stage)
    {
        if (Locked == true)
            return;
        if (stage == this)
            active = true;
        else
            active = false;
    }

    public void ReceiveUnlocked(int stage)
    {
        if (stage == StageBuildIndex)
        {
            Locked = false;
            StageController.StageDraw += this.Draw;
        }
    }

    public void Draw()
    {
        if (Locked == true)
            return;
        if (active == true)
        {
            this.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f); // 첫 선택시 애니메이션등 넣을것
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f); // 선택 안된 놈의 애니메이션등
        }
    }
}
