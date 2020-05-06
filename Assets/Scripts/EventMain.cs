using System.Collections.Generic;
using UnityEngine;

public class EventMain : MonoBehaviour
{
    public static EventMain GetEventMain;
    [SerializeField] private TextLog textLog = null;
    [SerializeField] private EventMenuOptionButton optionBtn = null;
    [SerializeField] private Transform optionContainer = null;
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private GameObject eventMenu = null;

    // Percipants
    [SerializeField] private PlayerMain player = null;

    [SerializeField] private ChangeName changeName = null;

    private void Awake()
    {
        if (GetEventMain == null)
        {
            GetEventMain = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
    }

    public void EventSolo(SoloEvent soloEvent, bool CanLeaveDiretly = true)
    {
        textLog.SetText(soloEvent.Intro);
        optionContainer.KillChildren();
        foreach (SoloEvent solo in soloEvent.SubEvents)
        {
            Instantiate(optionBtn, optionContainer).Setup(solo.Title).onClick.AddListener(() => EventSolo(solo));
        }
        Setup(CanLeaveDiretly);
    }

    public void EventWith(BasicChar whom, bool CanLeaveDiretly = true)
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
        gameUIWasActive = canvasMain.HideGameUI();
    }

    private void LeaveBtn() => Instantiate(optionBtn, optionContainer).Setup("Leave").onClick.AddListener(Leave);

    private void Leave()
    {
        eventMenu.SetActive(false);
        canvasMain.Resume();
        if (gameUIWasActive)
        {
            canvasMain.ShowGameUI();
        }
    }

    public void SummonChangeName(Identity basicChar) => Instantiate(changeName, transform).Setup(basicChar);

    public void EndEvent() => Leave();
}

public class GameEventSystem
{
    public GameEventSystem(BasicChar basicChar)
    {
        SoloEvents = new SoloEventsClass(basicChar);
    }

    public SoloEventsClass SoloEvents { get; }

    public class SoloEventsClass : EventsContaier
    {
        public SoloEventsClass(BasicChar basicChar) : base(basicChar)
        {
            VoreEvents = new SoloVoreEvents(basicChar);
        }

        public SoloVoreEvents VoreEvents { get; }

        public void GiveBirth()
        {
            // Option to name
        }

        public void NeedToShit()
        {
            if (basicChar is PlayerMain player)
            {
                eventMain.EventSolo(new NeedToShit(player));
            }
        }

        public void TeleportIsLocked()
        {
            if (basicChar is PlayerMain player)
            {
                eventMain.EventSolo(new PortalIsLocked(player), true);
            }
        }

        public class SoloVoreEvents : EventsContaier
        {
            public SoloVoreEvents(BasicChar basicChar) : base(basicChar)
            {
            }

            public void Hunger()
            {
            }
        }

        public class SoloScatEvents : EventsContaier
        {
            public SoloScatEvents(BasicChar basicChar) : base(basicChar)
            {
            }
        }

        public class SoloFluidEvents : EventsContaier
        {
            public SoloFluidEvents(BasicChar basicChar) : base(basicChar)
            {
            }

            public void MilkOverFlow()
            {
                // Hand milk & Machine & ignore
            }

            public void CumOverFlow()
            {
                // if you for some reason have more than max
                // Maybe item of perk that allows it?
            }
        }
    }
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

public abstract class SoloEvent
{
    public SoloEvent(PlayerMain player)
    {
        this.player = player;
        eventMain = EventMain.GetEventMain;
    }

    protected PlayerMain player;
    protected EventMain eventMain;
    public abstract string Title { get; }
    public abstract string Intro { get; }
    public abstract List<SoloEvent> SubEvents { get; }
}

public class NeedToShit : SoloEvent
{
    public NeedToShit(PlayerMain player) : base(player)
    {
        SubEvents.Add(new NeedToShitSub(player));
        SubEvents.Add(new NeedToShitSub2(player));
    }

    public override string Title => "Need to shit";
    public override string Intro => intro();
    public override List<SoloEvent> SubEvents { get; } = new List<SoloEvent>();

    private string intro()
    {
        return "You feel the presure build on your rectum";
    }

    private class NeedToShitSub : SoloEvent
    {
        public NeedToShitSub(PlayerMain player) : base(player)
        {
        }

        public override string Title => "Shit";

        public override string Intro
        {
            get
            {
                string shit = player.SexualOrgans.Anals.DefecateAll();
                return "" + shit;
            }
        }

        public override List<SoloEvent> SubEvents { get; } = new List<SoloEvent>();
    }

    private class NeedToShitSub2 : SoloEvent
    {
        public NeedToShitSub2(PlayerMain player) : base(player)
        {
        }

        public override string Title => "Hold it";

        public override string Intro => throw new System.NotImplementedException();

        public override List<SoloEvent> SubEvents => throw new System.NotImplementedException();
    }
}

public class GiveBirth : SoloEvent
{
    public GiveBirth(PlayerMain player, Child child) : base(player)
    {
        this.child = child;
        SubEvents.Add(new GiveBirthSub(player, child));
        SubEvents.Add(new GiveBirthSub2(player, child));
    }

    private Child child;
    public override string Title => "Give birth";

    public override string Intro => "The water has gobe";

    public override List<SoloEvent> SubEvents { get; } = new List<SoloEvent>();

    private class GiveBirthSub : SoloEvent
    {
        public GiveBirthSub(PlayerMain player, Child child) : base(player)
        {
            this.child = child;
        }

        private Child child;
        public override string Title => "Name child";

        public override string Intro
        {
            get
            {
                eventMain.SummonChangeName(child.ChildIdentity);
                return "";
            }
        }

        public override List<SoloEvent> SubEvents => throw new System.NotImplementedException();
    }

    private class GiveBirthSub2 : SoloEvent
    {
        public GiveBirthSub2(PlayerMain player, Child child) : base(player)
        {
            this.child = child;
        }

        private Child child;
        public override string Title => "Skip & auto name";

        public override string Intro
        {
            get
            {
                child.ChildIdentity.FirstName = RandomName.FemaleName;
                child.ChildIdentity.LastName = player.Identity.LastName;
                eventMain.EndEvent();
                return "";
            }
        }

        public override List<SoloEvent> SubEvents => throw new System.NotImplementedException();
    }
}

public class PortalIsLocked : SoloEvent
{
    public PortalIsLocked(PlayerMain player) : base(player)
    {
    }

    public override string Title => "Failed to sync portal";

    public override string Intro => "For some reason you couldn't sync with this portal, maybe if you look around you will find a way.";

    public override List<SoloEvent> SubEvents => new List<SoloEvent>();
}