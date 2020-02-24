﻿using System.Collections.Generic;
using UnityEngine;
using Vore;

[System.Serializable]
public abstract class BasicChar : MonoBehaviour
{
    public BasicChar()
    {
        vore = new VoreEngine(this);
    }

    [SerializeField] protected Identity identity;

    public Identity Identity => identity;
    [SerializeField] protected RelationshipTracker relationshipTracker = new RelationshipTracker();
    public RelationshipTracker RelationshipTracker => relationshipTracker;
    [SerializeField] private Inventory inventory = new Inventory();

    public Inventory Inventory => inventory;

    [SerializeField] private RaceSystem raceSystem = new RaceSystem();

    public RaceSystem RaceSystem => raceSystem;
    private Genders lastGender;

    private void DidGenderChange()
    {
        if (lastGender != this.Gender)
        {
            lastGender = this.Gender;
            GenderChangeEvent?.Invoke();
        }
    }

    public delegate void GenderChange();

    public event GenderChange GenderChangeEvent;

    public Genders Gender => this.Gender();
    public GenderTypes GenderType => this.GenderType();

    [SerializeField] private VoreEngine vore;

    public VoreEngine Vore => vore;

    private VoreChar voreChar;

    public VoreChar VoreChar => voreChar = voreChar != null ? voreChar : GetComponentInChildren<VoreChar>();

    [SerializeField] private Age age = new Age();

    public Age Age => age;

    [SerializeField] protected Body body = new Body(160, 20, 20);

    public Body Body => body;
    public float Weight => Body.Weight;

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
    [SerializeField] private bool autoEss = true;

    public bool AutoEss => autoEss;

    // Maybe a bit overkill but I want to make sure autoEss isn't toggled by mistake
    public void ToggleAutoEssence() => autoEss = !autoEss;

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

    public virtual void Start()
    {
        identity = new Identity();
        Essence.Masc.EssenceSliderEvent += DidGenderChange;
        essence.Femi.EssenceSliderEvent += DidGenderChange;
        expSystem = new ExpSystem(1);
        StartCoroutine(BasicCharExtensions.TickEverySecond(this));
        DateSystem.NewDayEvent += this.GrowFetuses;
        DateSystem.NewDayEvent += PregnancySystem.GrowChild;
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
        Essence.Masc.EssenceSliderEvent -= DidGenderChange;
        essence.Femi.EssenceSliderEvent -= DidGenderChange;
    }

    public virtual void Update() => this.RefreshOrgans(AutoEss);

    [SerializeField] private List<Skill> skills = new List<Skill>();

    public List<Skill> Skills => skills;
}