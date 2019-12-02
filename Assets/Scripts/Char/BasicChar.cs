using System.Collections.Generic;
using UnityEngine;
using Vore;

[System.Serializable]
public abstract class BasicChar : MonoBehaviour
{
    public BasicChar()
    {
        looks = new Looks(this);
        vore = new VoreEngine(this);
    }

    // can't Serialize because it gets saved
    private BasicCharGame basicCharGame;

    public BasicCharGame BasicCharGame => basicCharGame;

    [Space]
    public string firstName, lastName;

    public string FullName => $"{firstName} {lastName}";

    [SerializeField]
    private Inventory inventory = new Inventory();

    public Inventory Inventory => inventory;

    [SerializeField]
    private RaceSystem raceSystem = new RaceSystem();

    public RaceSystem RaceSystem => raceSystem;
    public string Race => RaceSystem.CurrentRace().ToString();

    [SerializeField]
    private Looks looks;

    public Looks Looks => looks;

    public Genders Gender => this.Gender();
    public GenderTypes GenderType => this.GenderType();

    [SerializeField]
    private VoreEngine vore;

    public VoreEngine Vore => vore;

    [SerializeField]
    private Age age = new Age();

    public Age Age => age;

    [SerializeField]
    protected Body body;

    public Body Body => body;
    public float Weight => Body.Weight;

    public virtual void Awake()
    {
        if (basicCharGame == null)
        {
            basicCharGame = GetComponent<BasicCharGame>();
        }
    }

    [SerializeField]
    private Health hp;

    [SerializeField]
    private Health wp;

    public Health HP => hp;
    public Health WP => wp;

    [Header("Level,exp, stats & perks")]
    [SerializeField]
    private ExpSystem expSystem = new ExpSystem();

    public ExpSystem ExpSystem => expSystem;

    [SerializeField]
    public Perks perk = new Perks();

    public Perks Perks => perk;

    [Header("Stats")]
    [SerializeField]
    protected StatsContainer stats = new StatsContainer();

    public StatsContainer Stats => stats;

    public virtual void Init(int lvl, int maxhp, int maxwp)
    {
        hp = new Health(maxhp);
        wp = new Health(maxwp);
        expSystem.Level = lvl;
    }

    [Header("Essence")]
    [SerializeField]
    private bool autoEss = true;

    public bool AutoEss => autoEss;

    // Maybe a bit overkill but I want to make sure autoEss isn't toggled by mistake
    public void ToggleAutoEssence() => autoEss = !autoEss;

    [SerializeField]
    private EssenceSystem essence = new EssenceSystem();

    public EssenceSystem Essence => essence;
    public Essence Masc => essence.Masc;
    public Essence Femi => essence.Femi;
    public float EssDrain => 3 + Perks.PerkBonus(PerksTypes.GainEss);
    public float EssGive => 3 + Perks.PerkBonus(PerksTypes.GiveEss);
    public float RestRate => 1f + Perks.PerkBonus(PerksTypes.FasterRest);
    public bool CanDrainMasc => Masc.Amount > 0 || SexualOrgans.Balls.Count > 0 || SexualOrgans.Dicks.Count > 0;
    public bool CanDrainFemi => Femi.Amount > 0 || SexualOrgans.Boobs.Count > 0 || SexualOrgans.Dicks.Count > 0;

    public float LoseMasc(float mascToLose)
    {
        float have = Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && (SexualOrgans.Dicks.Count > 0 || SexualOrgans.Balls.Count > 0))// have needed organs
            {
                if (SexualOrgans.Balls.Count > 0 && SexualOrgans.Dicks.Count > 0)
                {
                    if (SexualOrgans.Dicks.Total() >= SexualOrgans.Balls.Total() * 2f + 1f)
                    {
                        fromOrgans += SexualOrgans.Dicks.ReCycle();
                    }
                    else
                    {
                        fromOrgans += SexualOrgans.Balls.ReCycle();
                    }
                }
                else if (SexualOrgans.Balls.Count > 0)
                {
                    fromOrgans += SexualOrgans.Balls.ReCycle();
                }
                else
                {
                    fromOrgans += SexualOrgans.Dicks.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                Masc.Gain(left);
            }
        }
        return have;
    }

    public float LoseFemi(float femiToLose)
    {
        float have = Femi.Lose(femiToLose);
        float missing = femiToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && (SexualOrgans.Vaginas.Count > 0 || SexualOrgans.Boobs.Count > 0))// have needed organs
            {
                if (SexualOrgans.Boobs.Count > 0 && SexualOrgans.Vaginas.Count > 0)
                {
                    if (SexualOrgans.Boobs.Total() >= SexualOrgans.Vaginas.Total() * 2f + 1f)
                    {
                        fromOrgans += SexualOrgans.Boobs.ReCycle();
                    }
                    else
                    {
                        fromOrgans += SexualOrgans.Vaginas.ReCycle();
                    }
                }
                else if (SexualOrgans.Vaginas.Count > 0)
                {
                    fromOrgans += SexualOrgans.Vaginas.ReCycle();
                }
                else
                {
                    fromOrgans += SexualOrgans.Boobs.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                Femi.Gain(left);
            }
        }
        return have;
    }

    [Space]
    [SerializeField]
    private float gold = 0;

    public float Gold
    {
        get => Mathf.Floor(gold);
        set => gold = Mathf.Clamp(value, -gold, Mathf.Infinity);
    }

    /// <summary>
    /// Checks if you can afford it and if you can; pay it else return false and lose zero gold.
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool CanAfford(int cost)
    {
        if (cost <= Gold)
        {
            Gold -= cost;
            return true;
        }
        return false;
    }

    [SerializeField]
    private Flags flags = new Flags();

    public Flags Flags => flags;

    [SerializeField]
    private PregnancySystem pregnancySystem = new PregnancySystem();

    public PregnancySystem PregnancySystem => pregnancySystem;
    public bool Pregnant => SexualOrgans.Vaginas.Exists(v => v.Womb.HasFetus);

    [Header("Organs")]
    [SerializeField]
    private Organs sexualOrgans = new Organs();

    public Organs SexualOrgans => sexualOrgans;

    [SerializeField]
    private SexStats sexStats = new SexStats();

    public SexStats SexStats => sexStats;

    public virtual void Start()
    {
    }

    private void Update()
    {
        SexualOrgans.RefreshOrgans(this, AutoEss);
    }

    [SerializeField]
    private List<Skill> skills = new List<Skill>();

    public List<Skill> Skills => skills;
}