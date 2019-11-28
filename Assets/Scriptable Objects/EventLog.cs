using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eventlog",menuName ="Eventlog")]
public class EventLog : ScriptableObject
{
    [SerializeField]
    private List<string> text = new List<string>();

    public void AddTo(string addText)
    {
        text.Insert(0,addText);
        EventTextEvent?.Invoke();
    }
    public string Print()
    {
        string toPrint = "";
        foreach(string line in text)
        {
            toPrint += line + "\n\n";
        }
        return toPrint;
    }

    public void ClearLog()
    {
        text.Clear();
        EventTextEvent?.Invoke();
    }

    public delegate void EventText();
    public static event EventText EventTextEvent;
}
