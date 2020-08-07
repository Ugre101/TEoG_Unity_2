using System.Collections.Generic;
using System.Text;

public enum SubjectClass
{
    All,
    Misc,
    Dorm,
}

public static class EventLog
{
    private static readonly List<EventLogText> logTexts = new List<EventLogText>();
    public static bool IsEmpty => logTexts.Count < 1;

    public static void AddTo(string addText, SubjectClass subjectClass = SubjectClass.Misc)
    {
        logTexts.Insert(0, new EventLogText(subjectClass, addText));
        EventTextEvent?.Invoke();
    }

    public static string Print(SubjectClass printOnly = SubjectClass.All)
    {
        if (!IsEmpty)
        {
            StringBuilder toPrint = new StringBuilder();
            if (printOnly != SubjectClass.All)
            {
                foreach (EventLogText line in logTexts.FindAll(l => l.SubjectClass == printOnly))
                {
                    toPrint.Append($"{line.Text}\n\n");
                }
            }
            else
            {
                foreach (EventLogText line in logTexts)
                {
                    toPrint.Append($"{line.Text}\n\n");
                }
            }
            return toPrint.ToString();
        }
        return string.Empty;
    }

    public static EventLogSave Save() => new EventLogSave(logTexts);

    public static void Load(EventLogSave save)
    {
        logTexts.Clear();
        logTexts.AddRange(save.EventLogTexts);

        EventTextEvent?.Invoke();
    }

    public static void ClearLog()
    {
        logTexts.Clear();
        EventTextEvent?.Invoke();
    }

    public delegate void EventText();

    public static event EventText EventTextEvent;
}

public class EventLogText
{
    public EventLogText(SubjectClass subjectClass, string text)
    {
        SubjectClass = subjectClass;
        Text = text;
    }

    public SubjectClass SubjectClass { get; }
    public string Text { get; }
}

[System.Serializable]
public struct EventLogSave
{
    [UnityEngine.SerializeField] private List<EventLogText> eventLogTexts;

    public EventLogSave(List<EventLogText> eventLogTexts)
    {
        this.eventLogTexts = eventLogTexts;
    }

    public List<EventLogText> EventLogTexts => eventLogTexts;
}