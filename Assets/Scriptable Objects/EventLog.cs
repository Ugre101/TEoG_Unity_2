using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventLog 
{
    private static List<string> text = new List<string>();

    public static void AddTo(string addText)
    {
        text.Insert(0,addText);
        EventTextEvent?.Invoke();
    }
    public static string Print()
    {
        string toPrint = "";
        foreach(string line in text)
        {
            toPrint += line + "\n\n";
        }
        return toPrint;
    }

    public static void ClearLog()
    {
        text.Clear();
        EventTextEvent?.Invoke();
    }

    public delegate void EventText();
    public static event EventText EventTextEvent;
}
