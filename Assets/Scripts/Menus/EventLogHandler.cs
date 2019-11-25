using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventLogHandler : MonoBehaviour, IPointerClickHandler
{
    // Public
    public GameUI gameui;
    public EventLog eventLog;
    public KeyBindings keys;
    // Private
    [SerializeField]
    private TextMeshProUGUI logText;

    private bool oneClick = false;
    private float timeFirstClick;

    private void OnEnable()
    {
        EventLog.eventText += PrintEventlog;
        PrintEventlog();
    }
    private void OnDisable()
    {
        EventLog.eventText -= PrintEventlog;
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
        logText.text = eventLog.Print();
    }
}