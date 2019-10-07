using System.Collections.Generic;
using UnityEngine;

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
    protected BasicCharGame basicCharGame;

    [Space]
    public string firstName, lastName;

    public string FullName => $"{firstName} {lastName}";

    public Inventory Inventory;
    public RaceSystem raceSystem = new RaceSystem();
    public string Race => raceSystem.CurrentRace().ToString();

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

    public Looks Looks;
    public VoreEngine Vore;
    public Age Age;
    public Body Body;
    public float Weight { get => Body.weight.Value; set { Body.weight.Value = value; } }

    public virtual void Awake()
    {
    }

    public Health HP;
    public Health WP;

    [Header("Level,exp, stats & perks")]
    [SerializeField]
    public ExpSystem expSystem = new ExpSystem();

    public int Level => expSystem.Level;
    public int Exp { get => expSystem.Exp; set { expSystem.Exp += value; } }
    public int StatsPoints => expSystem.StatPoints;
    public bool StatBool => expSystem.StatBool();
    public int PerkPoints => expSystem.PerkPoints;

    /// <summary> Checks if char has perkpoints, and if true it subtracts one point and return true. </summary>
    public bool PerkBool => expSystem.PerkBool();

    [SerializeField]
    public Perks Perk = new Perks();

    [Header("Stats")]
    public CharStats strength;

    public float Str => strength.Value;
    public CharStats charm;
    public float Charm => charm.Value;
    public CharStats endurance;
    public float End=> endurance.Value;
    public CharStats dexterity;
    public float Dex => dexterity.Value;
    public CharStats intelligence;
    public CharStats GetStat(StatType stat)
    {
        switch (stat)
        {
            case StatType.Charm:
                return charm;
            case StatType.Dex:
                return dexterity;
            case StatType.End:
                return endurance;
            default:
            case StatType.Str:
                return strength;
        }
    }
    public float Int { get { return intelligence.Value; } }
    public virtual void Init(int lvl, float maxhp, float maxwp)
    {
        HP = new Health(maxhp);
        WP = new Health(maxwp);
        expSystem.Level = lvl;
    }

    [Header("Essence")]
    [SerializeField]
    private bool autoEss = true;

    public bool AutoEss { get { return autoEss; } }
    // Maybe a bit overkill but I want to make sure autoEss isn't toggled by mistake
    public void ToggleAutoEssence() => autoEss = !autoEss;

    [SerializeField]
    private Essence masc = new Essence();

    public Essence Masc => masc; 

    [SerializeField]
    private Essence femi = new Essence();

    public Essence Femi => femi;
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
        get { return Mathf.Floor(gold); }
        set
        {
            gold += Mathf.Clamp(value, -gold, Mathf.Infinity);
        }
    }

    public bool CanAfford(int cost)
    {
        if (cost <= Gold)
        {
            Gold -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Flags Flags;

    [Header("Organs")]
    [SerializeField]
    private List<Dick> dicks = new List<Dick>();

    public List<Dick> Dicks { get { return dicks; } }

    [SerializeField]
    private List<Balls> balls = new List<Balls>();

    public List<Balls> Balls { get { return balls; } }

    public float CumSlider()
    {
        return Balls.CumTotal() / Balls.CumMax();
    }

    public string CumStatus()
    {
        return $"{Mathf.Round(Balls.CumTotal())}";
    }

    [SerializeField]
    private List<Boobs> boobs = new List<Boobs>();

    public List<Boobs> Boobs { get { return boobs; } }

    [SerializeField]
    private bool lactating = false;

    public bool Lactating { get { return lactating; } }

    public float MilkSlider()
    {
        return Boobs.MilkTotal() / Boobs.MilkMax();
    }

    public string MilkStatus()
    {
        return $"{Mathf.Round(Boobs.MilkTotal() / 1000)}";
    }

    [SerializeField]
    private List<Vagina> vaginas = new List<Vagina>();

    public List<Vagina> Vaginas { get { return vaginas; } }
    public SexStats sexStats = new SexStats();

    public virtual void Start()
    {
        basicCharGame = GetComponent<BasicCharGame>();
        Looks = new Looks(basicCharGame.settings, this);
        Vore = new VoreEngine(basicCharGame.eventLog, this);
        Age = new Age();
        //Inventory.Owner = this;
    }

    private void Update()
    {
        RefreshOrgans();
    }

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
                        masc.Lose(Balls.Cost());
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
}