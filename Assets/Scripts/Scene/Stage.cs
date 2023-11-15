using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manager;

namespace StageSelect
{
    public class Stage : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private string name;
        [SerializeField] private Image select;
        private static StageController stagecontroller;
        public bool active = false;
        public bool Locked = true;
        public StageData stageData;
        public Image Lock;
        // Start is called before the first frame update
        private void Awake()
        {
            StageController.StageSelect += this.ReceiveActive;
            StageController.StageUnlock += this.ReceiveUnlocked;
            //Lock.color = new Color(0.6f, 0.6f, 0.6f); //락걸린 이미지
        }
        private void Start()
        {
            stagecontroller = this.transform.parent.GetComponent<StageController>();
            stagecontroller.Stages.Add(this);

        }
        public void Select(string input)
        {
            if (Locked == true || GameManager.Instance.GameStop)
                return;
            else if (active == false)
            {
                StageController.StageSelect(this);
                StageController.StageDraw();
            }
            else
            {
                GameManager.Instance.CurrentStage = GameManager.Instance.Stages[stageData.StageName];
                GameManager.Instance.CurrentStage.CurrentScore = 0;
                stagecontroller.uiManager.InStage(true);
                SoundManager.Instance.PlayBGMSound(GameManager.Instance.CurrentStage.StageName);
                SceneManager.LoadScene(input);
            }
        }

        public void ReceiveActive(Stage stage)
        {
            if (Locked == true)
                return;
            if (stage == this)
            {
                active = true;
            }    
            else
            {
                active = false;
            }
                
        }

        public void ReceiveUnlocked(StageData temp)
        {
            if (name == temp.StageName)
            {
                stageData = temp;
            }
            if (stageData.Active)
            {
                Locked = false;
                Lock.gameObject.SetActive(false);
                StageController.StageDraw += this.Draw;
            }
        }

        public void Draw()
        {
            if (Locked == true)
                return;
            if (active == true)
            {
                //this.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f); // 첫 선택시 애니메이션등 넣을것
                select.transform.position = gameObject.transform.position;
                select.gameObject.SetActive(true);
            }
            else
            {
                //this.GetComponent<Image>().color = new Color(1f, 1f, 1f); // 선택 안된 놈의 애니메이션등
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
}

