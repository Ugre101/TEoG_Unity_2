using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventLogHandler : MonoBehaviour, IPointerClickHandler
{
    // Public
    public CanvasMain gameui;
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

    private Vector2 curSize { get => rect.sizeDelta; set => rect.sizeDelta = value; }

    [SerializeField]
    private Sprite upArrow = null, downArrow = null;

    private void Start()
    {
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
        }
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
                _ = gameui.BigEventLog();
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
        if (curSize.y > minSize.y)
        {
            lastSize = curSize;
            curSize = minSize;
            btnImg.sprite = upArrow;
        }
        else
        {
            curSize = lastSize;
            btnImg.sprite = downArrow;
        }
    }
}