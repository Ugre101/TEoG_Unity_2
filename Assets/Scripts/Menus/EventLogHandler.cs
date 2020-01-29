using UnityEngine;
using UnityEngine.UI;

public class EventLogHandler : EventLogHandlerBase
{
    [SerializeField] private RectTransform rect = null;

    [SerializeField] private Image btnImg = null;

    private Vector2 lastSize = new Vector2(180f, 190f), minSize = new Vector2(180f, 20f);

    private Vector2 CurSize { get => rect.sizeDelta; set => rect.sizeDelta = value; }

    [SerializeField] private Sprite upArrow = null, downArrow = null;

    protected override void Start()
    {
        base.Start();
        rect = rect != null ? rect : GetComponent<RectTransform>();
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