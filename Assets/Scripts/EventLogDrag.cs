using UnityEngine;
using UnityEngine.EventSystems;

public class EventLogDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private RectTransform canvas = null;

    [SerializeField]
    private RectTransform rectT = null;

    private Vector2 RectSizeDelta { get => rectT.sizeDelta; set => rectT.sizeDelta = value; }

    [Header("Settings")]
    [Range(0, 50)]
    [SerializeField]
    private float minHeight = 0f;

    [Range(0, 50)]
    [SerializeField]
    private float topDist = 0f;

    private Vector2 sDelta;
    private Vector2 mPos;
    private float yScale;
    private float maxHeight;

    public void OnBeginDrag(PointerEventData eventData)
    {
        sDelta = RectSizeDelta;
        yScale = canvas.localScale.y;
        maxHeight = Screen.height / yScale - topDist;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mPos = eventData.position;
        sDelta.y = Mathf.Clamp(mPos.y / yScale, minHeight, maxHeight);
        RectSizeDelta = sDelta;
    }
}