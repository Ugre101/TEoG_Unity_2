using System.Collections.Generic;
using UnityEngine;
using Vore;

[System.Serializable]
public abstract class BasicChar : MonoBehaviour
{
    public BasicChar()
    {
        vore = new VoreEngine(this);
        DateSystem.NewMinuteEvent += DoEveryMin;
    }

    private void DoEveryMin()
    {
        // Do this in a central timemanger instead of indvidualy so that sleeping speeds up digesion & pregnancy etc.
        this.RefreshOrgans();
        this.OverTimeTick();
    }

    [SerializeField] protected Identity identity;

    public Identity Identity => identity;
    [SerializeField] protected RelationshipTracker relationshipTracker = new RelationshipTracker();
    public RelationshipTracker RelationshipTracker => relationshipTracker;
    [SerializeField] private Inventory inventory = new Inventory();

    public Inventory Inventory => inventory;

    [SerializeField] private EquiptItems equiptItems = new EquiptItems();
    public EquiptItems EquiptItems => equiptItems;

    [SerializeField] private RaceSystem raceSystem = new RaceSystem();

    public RaceSystem RaceSystem => raceSystem;
    private Genders lastGender;

    private void DidGenderChange()
    {
        if (lastGender != this.Gender())
        {
            lastGender = this.Gender();
            SpriteHandler.ChangeSprite();
        }
    }

    public string Gender => Settings.GetGender(this);
    public GenderTypes GenderType => this.GenderType();

    [SerializeField] private VoreEngine vore;

    public VoreEngine Vore => vore;

    private VoreChar voreChar;

    public VoreChar VoreChar => voreChar = voreChar != null ? voreChar : GetComponentInChildren<VoreChar>();

    [SerializeField] private Age age = new Age();

    public Age Age => age;

    [SerializeField] protected Body body = new Body(160, 20, 20);

    public Body Body => body;

    public virtual void Awake()
    {
    }

    [SerializeField] private Health hp;

    public Health HP => hp;

    [SerializeField] private Health wp;

    public Health WP => wp;

    [Header("Level,exp, stats & perks")]
    [SerializeField] private ExpSystem expSystem = new ExpSystem();

    public ExpSystem ExpSystem => expSystem;

    [SerializeField] public Perks perk = new Perks();

    public Perks Perks => perk;

    [Header("Stats")]
    [SerializeField] protected StatsContainer stats = new StatsContainer();

    public StatsContainer Stats => stats;

    [Header("Essence")]

    // Maybe a bit overkill but I want to make sure autoEss isn't toggled by mistake

    [SerializeField] private EssenceSystem essence = new EssenceSystem();

    public EssenceSystem Essence => essence;
    public float RestRate => 3.44f + Perks.GetPerkLevel(PerksTypes.FasterRest);

    [Space]
    [SerializeField] private Currency currency = new Currency();

    public Currency Currency => currency;

    [SerializeField] private Flags flags = new Flags();

    public Flags Flags => flags;

    [SerializeField] private PregnancySystem pregnancySystem = new PregnancySystem();

    public PregnancySystem PregnancySystem => pregnancySystem;
    public bool Pregnant => SexualOrgans.Vaginas.Exists(v => v.Womb.HasFetus);

    [Header("Organs")]
    [SerializeField] private Organs sexualOrgans = new Organs();

    public Organs SexualOrgans => sexualOrgans;

    [SerializeField] private SexStats sexStats = new SexStats();

    public SexStats SexStats => sexStats;
    [SerializeField] private CharSpriteHandler spriteHandler;
    public CharSpriteHandler SpriteHandler
    {
        get
        {
            if (spriteHandler == null)
            {
                spriteHandler = GetComponent<CharSpriteHandler>();
            }
            return spriteHandler;
        }
    }
    public virtual void Start()
    {
        identity = new Identity();
        SexualOrgan.SomethingChanged += DidGenderChange;

        expSystem = new ExpSystem(1);
        DateSystem.NewDayEvent += this.GrowFetuses;
        DateSystem.NewDayEvent += PregnancySystem.GrowChild;
        gameEvent = new GameEventSystem(this);
        SpriteHandler.Setup(this);
    }

    protected void InitHealth()
    {
        hp = new Health(new AffectedByStat(Stats.Endurance, 5));
        wp = new Health(new AffectedByStat(Stats.Willpower, 5));
        HP.FullGain();
        WP.FullGain();
    }

    public virtual void OnDestroy()
    {
        SexualOrgan.SomethingChanged -= DidGenderChange;
        DateSystem.NewMinuteEvent -= DoEveryMin;
    }

    [SerializeField] private List<Skill> skills = new List<Skill>();

    public List<Skill> Skills => skills;
    private GameEventSystem gameEvent;
    public GameEventSystem Events => gameEvent;


}