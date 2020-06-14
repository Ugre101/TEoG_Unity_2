using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventMain : MonoBehaviour
{
    public static EventMain GetEventMain { get; set; }

    [SerializeField] private TextLog textLog = null;
    [SerializeField] private EventMenuOptionButton optionBtn = null;
    [SerializeField] private Transform optionContainer = null;
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private GameObject eventMenu = null;

    // Percipants
    [SerializeField] private PlayerMain lplayer = null;

    [SerializeField] private ChangeName changeName = null;
    [SerializeField] private ChangeNames changeNames = null;

    private void Awake()
    {
        if (GetEventMain == null)
        {
            GetEventMain = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        GameManager.GameStateChangeEvent += PlayQuedEvent;
    }

    private List<UnityAction> quedEvents = new List<UnityAction>();

    public void QueEvent(UnityAction eventToQue)
    {
        if (GameManager.CurState == GameState.Free)
        {
            eventToQue?.Invoke();
        }
        else
        {
            quedEvents.Add(eventToQue);
            Debug.Log(quedEvents.Count);
        }
    }

    public void PlayQuedEvent(GameState newState)
    {
        if (newState == GameState.Free && quedEvents.Count > 0)
        {
            quedEvents[0]?.Invoke();
            quedEvents.RemoveAt(0);
        }
    }

    public void EventSolo(SoloEvent soloEvent, bool CanLeaveDiretly = true)
    {
        textLog.SetText(soloEvent.Intro);
        optionContainer.KillChildren();
        foreach (SoloSubEvent solo in soloEvent.SubEvents)
        {
            Instantiate(optionBtn, optionContainer).Setup(solo.Title).onClick.AddListener(() => EventSubSolo(solo));
        }
        Setup(CanLeaveDiretly);
    }

    private void EventSubSolo(SoloSubEvent subEvent)
    {
        textLog.SetText(subEvent.Intro);
        optionContainer.KillChildren();
        if (subEvent.SubEvents != null)
        {
            foreach (SoloSubEvent solo in subEvent.SubEvents)
            {
                Instantiate(optionBtn, optionContainer).Setup(solo.Title).onClick.AddListener(() => EventSubSolo(solo));
            }
        }
        Setup(subEvent.CanLeave);
    }

    public void EventWith(BasicChar whom, bool CanLeaveDiretly)
    {
        Setup(CanLeaveDiretly);
    }

    public void EventWithSeveral(List<BasicChar> basicChars, bool CanLeaveDiretly = true)
    {
        Setup(CanLeaveDiretly);
    }

    private bool gameUIWasActive;

    private void Setup(bool canLeave)
    {
        if (canLeave)
        {
            LeaveBtn();
        }
        eventMenu.SetActive(true);
        GameManager.SetCurState(GameState.Event);
    }

    private void LeaveBtn() => Instantiate(optionBtn, optionContainer).Setup("Leave").onClick.AddListener(Leave);

    private void Leave()
    {
        eventMenu.SetActive(false);
        canvasMain.Resume();
    }

    public void SummonChangeName(Identity basicChar) => Instantiate(changeName, transform).Setup(basicChar);

    public ChangeNames SummonChangeNames(List<Identity> identities) => Instantiate(changeNames, transform).Setup(identities);

    public ChangeNames SummonChangeNames(List<Identity> identities, string lastName) => Instantiate(changeNames, transform).Setup(identities, lastName);

    public void EndEvent() => Leave();
}

public abstract class EventsContaier
{
    protected readonly BasicChar basicChar;
    protected EventMain eventMain => EventMain.GetEventMain;

    public EventsContaier(BasicChar basicChar)
    {
        this.basicChar = basicChar;
    }
}

public class SkipEvent
{
    public bool Skip { get; private set; }
    public bool ToggleSkip => Skip = !Skip;
}