using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventLogHandlerBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected CanvasMain canvasMain = null;

    [SerializeField] protected TextMeshProUGUI logText = null;
    private bool oneClick = false;
    private float timeFirstClick;

    protected virtual void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
    }

    protected virtual void OnEnable()
    {
        EventLog.EventTextEvent += PrintEventlog;
        PrintEventlog();
    }

    private void OnDisable() => EventLog.EventTextEvent -= PrintEventlog;

    protected void SetFontSize(float size) => logText.fontSize = size;

    private void PrintEventlog() => logText.text = EventLog.Print();

    public void OnPointerClick(PointerEventData data)
    {
        if (oneClick)
        {
            if (data.clickTime - timeFirstClick > 0.7f)
            {
                // if time since last click more than 1f just reset time
                timeFirstClick = data.clickTime;
            }
            else
            {
                oneClick = false;
                _ = canvasMain.BigEventLog();
            }
        }
        else
        {
            oneClick = true;
            timeFirstClick = data.clickTime;
        }
    }
}