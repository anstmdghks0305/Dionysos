using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private string name;
    private static StageController stagecontroller;
    [SerializeField] private bool active = false;
    public bool Locked = true;
    public StagePlayer stagePlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        StageController.StageSelect += this.ReceiveActive;
        StageController.StageUnlock += this.ReceiveUnlocked;
        gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f); //락걸린 이미지
    }
    private void Start()
    {
        stagecontroller = this.transform.parent.GetComponent<StageController>();

    }
    public void Select()
    {
        if (Locked == true ||GameManager.Instance.GameStop)
            return;
        else if (active == false)
        {
            StageController.StageSelect(this);
            StageController.StageDraw();
        }
        else
        { 
            UIManager.Instance.InStage(true);
            SceneManager.LoadScene(stagePlayer.Name);
        } 
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

    public void ReceiveUnlocked(StagePlayer temp)
    {
        if(name == temp.Name)
        {
            stagePlayer = temp;
        }
        if (stagePlayer.Active)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        stagecontroller.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stagecontroller.OnEndDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        stagecontroller.OnDrag(eventData);
    }
}
