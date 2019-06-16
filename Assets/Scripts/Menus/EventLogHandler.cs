using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventLogHandler : MonoBehaviour, IPointerClickHandler
{
    // Public
    public GameUI gameui;
    public EventLog eventlog;
    public KeyBindings keys;
    // Private
    private TextMeshProUGUI _eventLog;

    private bool _oneClick = false;
    private float _time;
    private float _down, _up;
    private static List<string> _loggedText = new List<string>();

    // Remember that this script handles both small eventlog and big.
    // Start is called before the first frame update.
    private void Awake()
    {
        _eventLog = GetComponentInChildren<TextMeshProUGUI>();
        if (_eventLog != null)
        {
            _eventLog.text = "works";
        }
        else
        {
            // if script can't find tmpro kill script
            this.enabled = false;
        }
        _time = Time.time;
        EventLog.eventText += PrintEventlog;
    }

    private void OnEnable()
    {
        PrintEventlog();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (_oneClick)
        {
            if (Time.time - _time > 0.8f)
            {
                // if time since last click more than 1f just reset time
                _time = Time.time;
            }
            else
            {
                // else handle double click
                _oneClick = false;
                if (gameui.bigEventLog())
                {
                    PrintEventlog();
                }
            }
        }
        else
        {
            _oneClick = true;
            _time = Time.time;
        }
    }

    private void PrintEventlog()
    {
        _eventLog.text = eventlog.Print();
    }
}