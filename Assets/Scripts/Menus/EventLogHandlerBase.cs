using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventLogHandlerBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected CanvasMain canvasMain = null;
    [SerializeField] private List<SortButton> sortButtons = new List<SortButton>();
    [SerializeField] protected TextMeshProUGUI logText = null, emptyText = null;
    private bool oneClick = false;
    private float timeFirstClick;

    protected virtual void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        List<SubjectClass> subjectClasses = UgreTools.EnumToList<SubjectClass>();
        for (int i = 0; i < subjectClasses.Count && i < sortButtons.Count; i++)
        {
            SubjectClass subject = subjectClasses[i];
            sortButtons[i].Setup(subject.ToString(), PrintSorted(subject));
        }
    }

    protected virtual void OnEnable()
    {
        EventLog.EventTextEvent += PrintEventlog;
        PrintEventlog();
    }

    private void OnDisable() => EventLog.EventTextEvent -= PrintEventlog;

    protected void SetFontSize(float size) => logText.fontSize = size;

    private void PrintEventlog()
    {
        logText.text = EventLog.Print();
        emptyText.gameObject.SetActive(EventLog.IsEmpty);
    }

    private UnityAction PrintSorted(SubjectClass subject) => () => PrintEventlogSorted(subject);

    private void PrintEventlogSorted(SubjectClass subject)
    {
        logText.text = EventLog.Print(subject);
        emptyText.gameObject.SetActive(EventLog.IsEmpty);
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (oneClick)
        {
            if (data.clickTime - timeFirstClick > Settings.DoubleClickTime)
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