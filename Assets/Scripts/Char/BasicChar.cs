using System.Collections.Generic;
using UnityEngine;
using Vore;

public enum Genders
{
    Male,
    Female,
    Herm,
    Dickgirl,
    Cuntboy,
    Doll
}

public enum GenderType
{
    Feminine,
    Masculine
}

public abstract class BasicChar : MonoBehaviour
{
    public BasicChar()
    {
        essence = new EssenceSystem(this);
        looks = new Looks(this);
        vore = new VoreEngine(this);
        age = new Age();
        pregnancySystem = new PregnancySystem(this);
    }

    [SerializeField]
    private BasicCharGame basicCharGame;

    public BasicCharGame BasicCharGame => basicCharGame;

    [Space]
    public string firstName, lastName;

    public string FullName => $"{firstName} {lastName}";

    public Inventory Inventory;

    [SerializeField]
    private RaceSystem raceSystem = new RaceSystem();

    public RaceSystem RaceSystem => raceSystem;
    public string Race => RaceSystem.CurrentRace().ToString();

    public Genders Gender
    {
        get
        {
            if (Dicks.Count > 0 && Vaginas.Count > 0)
            {
                return Genders.Herm;
            }
            else if (Dicks.Count > 0 && Boobs.Total() > 2)
            {
                return Genders.Dickgirl;
            }
            else if (Dicks.Count > 0)
            {
                return Genders.Male;
            }
            else if (Vaginas.Count > 0 && Boobs.Total() > 2)
            {
                return Genders.Female;
            }
            else if (Vaginas.Count > 0)
            {
                return Genders.Cuntboy;
            }
            else
            {
                return Genders.Doll;
            }
        }
    }

    public GenderType GenderType
    {
        get
        {
            switch (Gender)
            {
                case Genders.Cuntboy:
                case Genders.Doll:
                case Genders.Male:
                    return GenderType.Masculine;

                case Genders.Dickgirl:
                case Genders.Female:
                case Genders.Herm:
                default:
                    return GenderType.Feminine;
            }
        }
    }

    [SerializeField]
    private Looks looks;

    public Looks Looks => looks;

    [SerializeField]
    private VoreEngine vore;

    public VoreEngine Vore => vore;

    [SerializeField]
    private Age age;

    public Age Age => age;

    [SerializeField]
    protected Body body;

    public Body Body => body;
    public float Weight => Body.Weight;

    public virtual void Awake()
    {
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
    public Perks Perk = new Perks();

    [Header("Stats")]
    [SerializeField]
    protected StatsContainer stats = new StatsContainer();

    public StatsContainer Stats => stats;

    public virtual void Init(int lvl, float maxhp, float maxwp)
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
    private EssenceSystem essence;

    public EssenceSystem Essence => essence;
    public Essence Masc => essence.Masc;
    public Essence Femi => essence.Femi;
    public float EssDrain => 3 + Perk.PerkBonus(PerksTypes.GainEss);
    public float EssGive => 3 + Perk.PerkBonus(PerksTypes.GiveEss);
    public float RestRate => 1f + Perk.PerkBonus(PerksTypes.FasterRest);

    public float LoseMasc(float mascToLose)
    {
        float have = Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && (Dicks.Count > 0 || Balls.Count > 0))// have needed organs
            {
                if (Balls.Count > 0 && Dicks.Count > 0)
                {
                    if (Dicks.Total() >= Balls.Total() * 2f + 1f)
                    {
                        fromOrgans += Dicks.ReCycle();
                    }
                    else
                    {
                        fromOrgans += Balls.ReCycle();
                    }
                }
                else if (Balls.Count > 0)
                {
                    fromOrgans += Balls.ReCycle();
                }
                else
                {
                    fromOrgans += Dicks.ReCycle();
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
            while (missing > fromOrgans && (Vaginas.Count > 0 || Boobs.Count > 0))// have needed organs
            {
                if (Boobs.Count > 0 && Vaginas.Count > 0)
                {
                    if (Boobs.Total() >= Vaginas.Total() * 2f + 1f)
                    {
                        fromOrgans += Boobs.ReCycle();
                    }
                    else
                    {
                        fromOrgans += Vaginas.ReCycle();
                    }
                }
                else if (Vaginas.Count > 0)
                {
                    fromOrgans += Vaginas.ReCycle();
                }
                else
                {
                    fromOrgans += Boobs.ReCycle();
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
        set => gold += Mathf.Clamp(value, -gold, Mathf.Infinity);
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

    public Flags Flags;

    [SerializeField]
    private PregnancySystem pregnancySystem;

    public PregnancySystem PregnancySystem => pregnancySystem;

    [Header("Organs")]
    [SerializeField]
    private List<Dick> dicks = new List<Dick>();

    public List<Dick> Dicks => dicks;

    [SerializeField]
    private List<Balls> balls = new List<Balls>();

    public List<Balls> Balls => balls;

    public float CumSlider => Balls.CumTotal() / Balls.CumMax();

    public string CumStatus => $"{Mathf.Round(Balls.CumTotal())}";

    [SerializeField]
    private List<Boobs> boobs = new List<Boobs>();

    public List<Boobs> Boobs => boobs;

    [SerializeField]
    private bool lactating = false;

    public bool Lactating => lactating;

    public float MilkSlider => Boobs.MilkTotal() / Boobs.MilkMax();

    public string MilkStatus => $"{Mathf.Round(Boobs.MilkTotal() / 1000)}";

    [SerializeField]
    private List<Vagina> vaginas = new List<Vagina>();

    public List<Vagina> Vaginas => vaginas;

    [SerializeField]
    private SexStats sexStats = new SexStats();

    public SexStats SexStats => sexStats;

    public virtual void Start()
    {
        if (basicCharGame == null)
        {
            basicCharGame = GetComponent<BasicCharGame>();
        }
        foreach (BasicSkill s in skillsToAdd)
        {
            skills.Add(new UserSkill(s));
        }
        //Inventory.Owner = this;
    }

    private void Update() => RefreshOrgans();

    public void RefreshOrgans()
    {
        Dicks.RemoveAll(d => d.Size <= 0);
        Balls.RemoveAll(b => b.Size <= 0);
        Vaginas.RemoveAll(v => v.Size <= 0);
        Boobs.RemoveAll(b => b.Size <= 0);
        if (autoEss)
        {
            if (Masc.Amount > 0)
            {
                if (Dicks.Total() <= Balls.Total() * 2f + 1f)
                {
                    if (Dicks.Exists(d => Masc.Amount >= d.Cost))
                    {
                        foreach (Dick d in Dicks)
                        {
                            if (Masc.Amount >= d.Cost)
                            {
                                Masc.Lose(d.Grow());
                            }
                        }
                    }
                    else if (Masc.Amount >= Dicks.Cost())
                    {
                        Masc.Lose(Dicks.Cost());
                        Dicks.AddDick();
                    }
                }
                else
                {
                    if (Balls.Exists(b => Masc.Amount >= b.Cost))
                    {
                        foreach (Balls b in Balls)
                        {
                            if (Masc.Amount >= b.Cost)
                            {
                                Masc.Lose(b.Grow());
                            }
                        }
                    }
                    else if (Masc.Amount >= Balls.Cost())
                    {
                        Masc.Lose(Balls.Cost());
                        Balls.AddBalls();
                    }
                }
            }
            if (Femi.Amount > 0)
            {
                if (Boobs.Total() <= Vaginas.Total() * 1.5f + 1f)
                {
                    if (Boobs.Exists(b => Femi.Amount >= b.Cost))
                    {
                        foreach (Boobs b in Boobs)
                        {
                            if (Femi.Amount >= b.Cost)
                            {
                                Femi.Lose(b.Grow());
                            }
                        }
                    }
                    else if (Femi.Amount >= Boobs.Cost())
                    {
                        Femi.Lose(Boobs.Cost());
                        Boobs.AddBoobs();
                    }
                }
                else
                {
                    if (Vaginas.Exists(v => Femi.Amount >= v.Cost))
                    {
                        foreach (Vagina v in Vaginas)
                        {
                            if (Femi.Amount >= v.Cost)
                            {
                                Femi.Lose(v.Grow());
                            }
                        }
                    }
                    else if (Femi.Amount >= Vaginas.Cost())
                    {
                        Femi.Lose(Vaginas.Cost());
                        Vaginas.AddVag();
                    }
                }
            }
        }
    }

    [SerializeField]
    private List<UserSkill> skills = new List<UserSkill>();

    public List<UserSkill> Skills => skills;

    [SerializeField]
    private List<BasicSkill> skillsToAdd = new List<BasicSkill>();
}

[System.Serializable]
public class UserSkill
{
    public UserSkill(BasicSkill basicSkill) => skill = basicSkill;

    public BasicSkill skill;

    public int TurnsLeft { get; private set; } = 0;

    public float CoolDownPercent => skill.CoolDown != 0 ? TurnsLeft / (float)skill.CoolDown : 1;

    public bool Ready => skill.HasCoolDown ? TurnsLeft < 1 : true;

    public void StartCoolDown() => TurnsLeft = skill.CoolDown;

    public void RefreshCoolDown(int n = 1) => TurnsLeft -= n;

    public void ResetCoolDown() => TurnsLeft = 0;
}