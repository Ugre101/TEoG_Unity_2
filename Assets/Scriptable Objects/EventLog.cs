using System.Collections.Generic;
using System.Text;

public static class EventLog
{
    private static readonly List<string> text = new List<string>();
    public static bool IsEmpty => text.Count < 1;

    public static void AddTo(string addText)
    {
        text.Insert(0, addText);
        EventTextEvent?.Invoke();
    }

    public static string Print()
    {
        if (!IsEmpty)
        {
            StringBuilder toPrint = new StringBuilder();
            foreach (string line in text)
            {
                toPrint.Append(line + "\n\n");
            }
            return toPrint.ToString();
        }
        return string.Empty;
    }

    public static EventLogSave Save() => new EventLogSave(text);

    public static void Load(EventLogSave save)
    {
        text.Clear();
        text.AddRange(save.Logs);
        EventTextEvent?.Invoke();
    }

    public static void ClearLog()
    {
        text.Clear();
        EventTextEvent?.Invoke();
    }

    public delegate void EventText();

    public static event EventText EventTextEvent;
}

[System.Serializable]
public struct EventLogSave
{
    [UnityEngine.SerializeField] private List<string> logs;

    public EventLogSave(List<string> logs) => this.logs = logs;

    public List<string> Logs => logs;
}