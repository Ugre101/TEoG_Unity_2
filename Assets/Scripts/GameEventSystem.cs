using System.Collections.Generic;

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

        public void IGiveBirth(List<Child> child)
        {
            if (basicChar is PlayerMain player)
            {
                if (GiveBirth.skipEvent.Skip)
                {
                    new GiveBirth(player, child).SkipAction();
                }
                else
                {
                    eventMain.QueEvent(() => eventMain.EventSolo(new GiveBirth(player, child)));
                }
            }
        }

        public void INeedToShit()
        {
            if (basicChar is PlayerMain player)
            {
                if (NeedToShit.skipEvent.Skip)
                {
                    new NeedToShit(player).SkipAction();
                }
                else
                {
                    eventMain.QueEvent(() => eventMain.EventSolo(new NeedToShit(player)));
                }
            }
        }

        public void TeleportIsLocked()
        {
            if (basicChar is PlayerMain player)
            {
                eventMain.EventSolo(new PortalIsLocked(player), true);
            }
        }

        public void RaceChange(Races oldRace, Races newRace)
        {
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
    public abstract List<SoloSubEvent> SubEvents { get; }

    public virtual void SkipAction()
    {
    }
}

public abstract class SoloSubEvent
{
    public SoloSubEvent(PlayerMain player)
    {
        this.player = player;
        eventMain = EventMain.GetEventMain;
    }

    protected PlayerMain player;
    protected EventMain eventMain;
    public abstract bool CanLeave { get; }
    public abstract string Title { get; }
    public abstract string Intro { get; }
    public abstract List<SoloSubEvent> SubEvents { get; }
}

public class RaceChange : SoloEvent
{
    public RaceChange(PlayerMain player) : base(player)
    {
    }

    public override string Title => "Race change";

    public override string Intro
    {
        get
        {
            Races newRace = player.RaceSystem.FirstRace;
            return "";
        }
    }

    public override List<SoloSubEvent> SubEvents => throw new System.NotImplementedException();
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
    public override List<SoloSubEvent> SubEvents { get; } = new List<SoloSubEvent>();
    public static SkipEvent skipEvent = new SkipEvent();

    public override void SkipAction()
    {
        base.SkipAction();
    }

    private string intro()
    {
        return "You feel the presure build on your rectum";
    }

    private class NeedToShitSub : SoloSubEvent
    {
        public NeedToShitSub(PlayerMain player) : base(player)
        {
        }

        public override string Title => "Shit";

        public override string Intro
        {
            get
            {
                string shit = player.SexualOrgans.Anals.List.DefecateAll();
                return "" + shit;
            }
        }

        public override List<SoloSubEvent> SubEvents { get; } = new List<SoloSubEvent>();

        public override bool CanLeave => true;
    }

    private class NeedToShitSub2 : SoloSubEvent
    {
        public NeedToShitSub2(PlayerMain player) : base(player)
        {
        }

        public override string Title => "Hold it";

        public override string Intro => throw new System.NotImplementedException();

        public override List<SoloSubEvent> SubEvents => new List<SoloSubEvent>();

        public override bool CanLeave => true;
    }
}

public class GiveBirth : SoloEvent
{
    public GiveBirth(PlayerMain player, List<Child> child) : base(player)
    {
        this.child = child;
        SubEvents.Add(new GiveBirthSub(player, child));
        SubEvents.Add(new GiveBirthSub2(player, child));
    }

    private List<Child> child;
    public override string Title => "Give birth";

    public override string Intro => "The water has gobe";

    public override List<SoloSubEvent> SubEvents { get; } = new List<SoloSubEvent>();
    public static SkipEvent skipEvent = new SkipEvent();

    public override void SkipAction()
    {
        child.ForEach(c => c.ChildIdentity.SetFirstName(RandomName.FemaleName));
        child.ForEach(c => c.ChildIdentity.SetLastName(player.Identity.LastName));
        base.SkipAction();
    }

    private class GiveBirthSub : SoloSubEvent
    {
        public GiveBirthSub(PlayerMain player, List<Child> child) : base(player)
        {
            this.child = child;
        }

        private List<Child> child;
        public override string Title => "Name child";

        public override string Intro
        {
            get
            {
                List<Identity> identities = new List<Identity>();
                child.ForEach(c => identities.Add(c.ChildIdentity));
                eventMain.SummonChangeNames(identities, player.Identity.LastName).Done += eventMain.EndEvent;
                return "";
            }
        }

        public override List<SoloSubEvent> SubEvents => new List<SoloSubEvent>();

        public override bool CanLeave => true;
    }

    private class GiveBirthSub2 : SoloSubEvent
    {
        public GiveBirthSub2(PlayerMain player, List<Child> child) : base(player)
        {
            this.child = child;
        }

        private List<Child> child;
        public override string Title => "Skip & auto name";

        public override string Intro
        {
            get
            {
                child.ForEach(c => c.ChildIdentity.SetFirstName(RandomName.FemaleName));
                child.ForEach(c => c.ChildIdentity.SetLastName(player.Identity.LastName));
                eventMain.EndEvent();
                return "";
            }
        }

        public override List<SoloSubEvent> SubEvents => new List<SoloSubEvent>();

        public override bool CanLeave => true;
    }
}

public class PortalIsLocked : SoloEvent
{
    public PortalIsLocked(PlayerMain player) : base(player)
    {
    }

    public override string Title => "Failed to sync portal";

    public override string Intro => "For some reason you couldn't sync with this portal, maybe if you look around you will find a way.";

    public override List<SoloSubEvent> SubEvents => new List<SoloSubEvent>();
    public static SkipEvent skipEvent = new SkipEvent();
}