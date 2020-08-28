using System.Collections.Generic;
using UnityEngine;
using Vore;

[System.Serializable]
public class BasicChar
{
    [SerializeField] protected Identity identity = new Identity();

    public Identity Identity => identity;
    [SerializeField] protected RelationshipTracker relationshipTracker = new RelationshipTracker();
    public RelationshipTracker RelationshipTracker => relationshipTracker;
    [SerializeField] private Inventory inventory = new Inventory();

    public Inventory Inventory => inventory;

    [SerializeField] private EquiptItems equiptItems = new EquiptItems();
    public EquiptItems EquiptItems => equiptItems;

    [SerializeField] private RaceSystem raceSystem = new RaceSystem();

    public RaceSystem RaceSystem => raceSystem;

    #region Gender

    private Genders lastGender;

    public bool DidGenderChange()
    {
        if (lastGender != GenderExtensions.Gender(this))
        {
            lastGender = GenderExtensions.Gender(this);
            return true;
        }
        return false;
    }

    public string Gender(bool capital = false) => Settings.GetGender(this, capital);

    public GenderTypes GenderType => this.GenderType();

    #endregion Gender

    [SerializeField] private VoreEngine vore;

    public VoreEngine Vore => vore;

    [SerializeField] private Age age = new Age();

    public Age Age => age;

    [SerializeField] protected Body body = new Body(160, 20, 20);

    public Body Body => body;

    [SerializeField] private Health hp, wp;

    public Health HP => hp;

    public Health WP => wp;

    [Header("Level,exp, stats & perks")]
    [SerializeField] private ExpSystem expSystem = new ExpSystem();

    public ExpSystem ExpSystem => expSystem;

    [SerializeField] private Perks perk = new Perks();

    public Perks Perks => perk;

    [Header("Stats")]
    [SerializeField] protected StatsContainer stats = new StatsContainer();

    public StatsContainer Stats => stats;

    [Header("Essence")]

    // Maybe a bit overkill but I want to make sure autoEss isn't toggled by mistake

    [SerializeField] private EssenceSystem essence = new EssenceSystem();

    public EssenceSystem Essence => essence;

    [Space]
    [SerializeField] private Currency currency = new Currency();

    public Currency Currency => currency;

    [SerializeField] private Flags flags = new Flags();

    public Flags Flags => flags;

    [SerializeField] private PregnancySystem pregnancySystem = new PregnancySystem();

    public PregnancySystem PregnancySystem => pregnancySystem;
    public bool Pregnant => SexualOrgans.Vaginas.List.Exists(v => v.Womb.HasFetus);

    [Header("Organs")]
    [SerializeField] private SexualOrgans sexualOrgans = new SexualOrgans();

    public SexualOrgans SexualOrgans => sexualOrgans;

    [SerializeField] private SexStats sexStats = new SexStats();

    public SexStats SexStats => sexStats;

    public BasicChar()
    {
        identity = new Identity();
        expSystem = new ExpSystem(1);
        vore = new VoreEngine(this);
        Events = new GameEventSystem(this);
        hp = new Health(this, new AffectedByStat(StatTypes.End, 5));
        wp = new Health(this, new AffectedByStat(StatTypes.Will, 5));
        Essence.Femi.GainEvent += this.RefreshOrgans;
        Essence.Masc.GainEvent += this.RefreshOrgans;
    }

    public BasicChar(Age age, Body body, ExpSystem expSystem) : this()
    {
        this.age = age;
        this.body = body;
        this.expSystem = expSystem;
    }
    [SerializeField] private List<Skill> skills = new List<Skill>() { new Skill(SkillId.BasicAttack), new Skill(SkillId.BasicTease) };

    public List<Skill> Skills => skills;

    public GameEventSystem Events { get; }

    public delegate void DestroyHolder();

    public event DestroyHolder DestroyHolderEvent;

    public void IfHaveHolderDestoryIt() => DestroyHolderEvent?.Invoke();

    public virtual void DoEveryMin(int times)
    {
        // Do this in a central timemanger instead of indvidualy so that sleeping speeds up digesion & pregnancy etc.
        //   BasicChar.RefreshOrgans();
        this.OverTimeTick(times);
    }

    public virtual void DoEveryHour()
    {
    }

    public virtual void DoEveryDay()
    {
        this.GrowFetuses();
        PregnancySystem.GrowChild();
    }
}