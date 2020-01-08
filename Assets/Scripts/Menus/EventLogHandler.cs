using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventLogHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CanvasMain canvasMain = null;

    // Private
    [SerializeField]
    private TextMeshProUGUI logText = null;

    [SerializeField]
    private RectTransform rect = null;

    [SerializeField]
    private Image btnImg = null;

    private bool oneClick = false;
    private float timeFirstClick;

    private Vector2 defaultSize = new Vector2(180f, 190f), lastSize = new Vector2(180f, 190f), minSize = new Vector2(180f, 20f);

    private Vector2 CurSize { get => rect.sizeDelta; set => rect.sizeDelta = value; }

    [SerializeField]
    private Sprite upArrow = null, downArrow = null;

    private void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        rect = rect != null ? rect : GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        EventLog.EventTextEvent += PrintEventlog;
        PrintEventlog();
    }

    private void OnDisable()
    {
        EventLog.EventTextEvent -= PrintEventlog;
    }

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

    private void PrintEventlog()
    {
        logText.text = EventLog.Print();
    }

    public void ToggleSize()
    {
        if (CurSize.y > minSize.y)
        {
            lastSize = CurSize;
            CurSize = minSize;
            btnImg.sprite = upArrow;
        }
        else
        {
            CurSize = lastSize;
            btnImg.sprite = downArrow;
        }
    }
}