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
        text.Add(addText);
        eventText?.Invoke();
    }
    public string Print()
    {
        string toPrint = "";
        List<string> lastFirst = text;
        lastFirst.Reverse();
        foreach(string line in lastFirst)
        {
            toPrint += string.Format("{0}{1}{1}", line, System.Environment.NewLine);
        }
        return toPrint;
    }
    public delegate void EventText();
    public static event EventText eventText;
}
