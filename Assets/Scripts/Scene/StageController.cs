using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.UI;


public class StageController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ScrollRect scrollrect;
    public delegate void MyAction();
    public static Action<Stage> StageSelect;
    public static event Action<int> StageUnlock;
    public static MyAction StageDraw;
    private CancellationTokenSource token;
    private int SelectSize;
    private float First;
    private float TargetPos;
    private float LastDropPos;
    public CanvasScaler scaler;
    public float Ratio;
    /// <summary>
    /// 선택창칸 크기 이걸로 lerp써서 딱딱 칸에 맞춤
    /// </summary>

    private void Start()
    {
        SelectSize = Screen.width;
        Ratio = SelectSize / scaler.referenceResolution.x;
        First = GetComponent<RectTransform>().position.x;
        TargetPos = First;
        token = new CancellationTokenSource();
        foreach (int i in GameManager.Instance.GameClearData)
        {
            StageUnlock(i);
        }
        StageDraw();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollrect.OnBeginDrag(eventData);
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        scrollrect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        token = new CancellationTokenSource();
        LastDropPos = GetComponent<RectTransform>().position.x;
        TargetPosSetting();
        Centering().Forget();
    }
    /// <summary>
    /// 뒤에 더 넘어가서 멈추는 경우(TargetPos)설정이 잘못되어있는경우를 방지하기 위한 함수
    /// </summary>
    public void TargetPosSetting()
    {
        if (LastDropPos - TargetPos > 0)
            TargetPos = ((int)((LastDropPos - TargetPos + SelectSize / 2) / SelectSize)) * SelectSize + TargetPos;
        else
            TargetPos = ((int)((LastDropPos - TargetPos - SelectSize / 2) / SelectSize)) * SelectSize + TargetPos;
        if (TargetPos > First)
            TargetPos = First;
        else if (TargetPos <= First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio)
            TargetPos = First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio;
    }

    async UniTask Centering()
    {

        LastDropPos = GetComponent<RectTransform>().position.x;
        while (true)
        {
            await UniTask.Delay(20, cancellationToken: token.Token);
            LastDropPos = Mathf.Lerp(LastDropPos, TargetPos, 0.3f);
            Debug.Log(LastDropPos);
            if (Mathf.Abs(TargetPos - LastDropPos) < 10)
            {
                LastDropPos = TargetPos;
                token.Cancel();
            }
            GetComponent<RectTransform>().position = new Vector3(LastDropPos, GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().position.z);
        }
    }

    public void LeftMove()
    {
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
        token = new CancellationTokenSource();
        TargetPos += SelectSize;
        if (TargetPos > First)
            TargetPos = First;
        Centering().Forget();
    }

    public void RightMove()
    {
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
        token = new CancellationTokenSource();
        TargetPos -= SelectSize;
        Debug.Log(TargetPos);
        if (TargetPos <= First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio)
            TargetPos = First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio;
        Centering().Forget();
    }

    private void OnDisable()
    {
        StageUnlock = null;
        StageDraw = null;
        StageSelect = null;
    }
}
