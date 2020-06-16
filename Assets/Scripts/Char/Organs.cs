using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Organs
{
    // if genderpref auto ess will adapt to prefer certain organs over others. But it doesn't make tf impossible just harder.
    [SerializeField] private bool genderPrefActive = false;

    public bool GenderPrefActive => genderPrefActive;
    public bool SetGenderPrefActive { set => genderPrefActive = value; }
    public bool ToggleGenderPrefActive => genderPrefActive = !genderPrefActive;
    [SerializeField] private Genders genderPref = Genders.Doll;
    public Genders GenderPref => genderPref;
    public Genders SetGenderPref { set => genderPref = value; }

    [SerializeField] private DickContainer dicks = new DickContainer();

    public DickContainer Dicks => dicks;

    [SerializeField] private BallsContaier balls = new BallsContaier();

    public BallsContaier Balls => balls;

    [SerializeField] private BoobsConatiner boobs = new BoobsConatiner();

    public BoobsConatiner Boobs => boobs;
    [SerializeField] private CharStats boobsBonusRefillRate = new CharStats();
    public CharStats BoobsBonusRefillRate => boobsBonusRefillRate;
    [SerializeField] private bool lactating = false;

    public bool Lactating => lactating;

    //  public float MilkSlider => Boobs.FluidCurrentTotal() / Boobs.FluidMax();

    //    public string MilkStatus => $"{Mathf.Round(Boobs.FluidCurrentTotal() / 1000)}";

    [SerializeField] private VaginaContainer vaginas = new VaginaContainer();

    public VaginaContainer Vaginas => vaginas;

    [SerializeField] private List<Anal> anals = new List<Anal>();
    public List<Anal> Anals => anals;

    public float ScatSlider => Anals.FluidCurrentTotal() / Anals.FluidMax();
    public string ScatStatus => $"{Mathf.Round(Anals.FluidCurrentTotal() / 1000)}";
    // TODO scat totals
}

[System.Serializable]
public abstract class OrganContainer

{
    public abstract void AddNew();

    public abstract void AddNew(int baseSize = 1);

    public abstract string Looks { get; }

    public abstract float ReCycle();

    public abstract float AddCost { get; }

    public abstract float BiggestSizeValue { get; }
    public string BiggestSizeMorInch => Settings.MorInch(BiggestSizeValue);

    public delegate void OrganChanged();

    public abstract event OrganChanged Change;
}

[System.Serializable]
public abstract class OrganWithFluidContainer : OrganContainer
{
    public abstract float FluidSlider { get; }
    public abstract string FluidStatus { get; }
    public abstract string LooksWithOutFluids { get; }
}

[System.Serializable]
public class DickContainer : OrganContainer
{
    [SerializeField] private List<Dick> dicks = new List<Dick>();
    public List<Dick> List => dicks;

    public override string Looks
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Dick dick = List[i];
                if (i == 0)
                {
                    builder.Append(dick.Look());
                }
                else
                {
                    builder.Append(dick.Look(false));
                }
                if (i == List.Count - 2)
                {
                    builder.Append(" and ");
                }
                else if (i == List.Count - 1)
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }
    }

    public override float AddCost => Mathf.Round(30 * Mathf.Pow(4, List.Count));

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override event OrganChanged Change;

    public override void AddNew(int baseSize)
    {
        List.Add(new Dick(baseSize));
    }

    public override void AddNew()
    {
        List.Add(new Dick());
    }

    public override float ReCycle()
    {
        Dick toShrink = List[List.Count - 1];
        if (toShrink.Shrink())
        {
            Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }

    public void Remove(Dick toRemove)
    {
        List.Remove(toRemove);
    }
}

[System.Serializable]
public class BallsContaier : OrganWithFluidContainer
{
    [SerializeField] private List<Balls> balls = new List<Balls>();
    public List<Balls> List => balls;
    [SerializeField] private CharStats ballsBunusRefillRate = new CharStats(0);
    public CharStats BallsBunusRefillRate => ballsBunusRefillRate;

    public override float FluidSlider => List.FluidCurrentTotal() / List.FluidMax();

    public override string FluidStatus => Settings.LorGal(List.FluidCurrentTotal() / 1000);

    public override string Looks
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Balls balls = List[i];
                if (i == 0)
                {
                    builder.Append(balls.Look());
                }
                else
                {
                    builder.Append(balls.Look(false));
                }
                if (i == List.Count - 2)
                {
                    builder.Append(" and ");
                }
                else if (i == List.Count - 1)
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }
    }

    public override string LooksWithOutFluids
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Balls balls = List[i];
                if (i == 0)
                {
                    builder.Append(balls.LookWithOutFluid());
                }
                else
                {
                    builder.Append(balls.LookWithOutFluid(false));
                }
                if (i == List.Count - 2)
                {
                    builder.Append(" and ");
                }
                else if (i == List.Count - 1)
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }
    }

    public override float AddCost => Mathf.Round(30 * Mathf.Pow(4, List.Count));

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override event OrganChanged Change;

    public override void AddNew()
    {
        List.Add(new Balls());
    }

    public override void AddNew(int baseSize)
    {
        List.Add(new Balls(baseSize));
    }

    public void Remove(Balls balls)
    {
        List.Remove(balls);
    }

    public override float ReCycle()
    {
        Balls toShrink = List[balls.Count - 1];
        if (toShrink.Shrink())
        {
            Remove(toShrink);
            return 30f;
        }
        return toShrink.Cost;
    }
}

[System.Serializable]
public class BoobsConatiner : OrganWithFluidContainer
{
    [SerializeField] private List<Boobs> balls = new List<Boobs>();
    public List<Boobs> List => balls;

    public override float FluidSlider => List.FluidCurrentTotal() / List.FluidMax();

    public override string FluidStatus => Settings.LorGal(List.FluidCurrentTotal() / 1000);

    public override string LooksWithOutFluids => throw new System.NotImplementedException();

    public override string Looks => List.Looks();

    public override float AddCost => List.Cost();

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override event OrganChanged Change;

    public override void AddNew()
    {
        List.Add(new Boobs());
    }

    public override void AddNew(int baseSize)
    {
        List.Add(new Boobs(baseSize));
    }

    public override float ReCycle()
    {
        return List.ReCycle();
    }
}

[System.Serializable]
public class VaginaContainer : OrganContainer
{
    [SerializeField] private List<Vagina> balls = new List<Vagina>();
    public List<Vagina> List => balls;

    public override string Looks => List.Looks();

    public override float AddCost => List.Cost();

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override event OrganChanged Change;

    public override void AddNew()
    {
        List.Add(new Vagina());
    }

    public override void AddNew(int baseSize = 1)
    {
        List.Add(new Vagina(baseSize));
    }

    public override float ReCycle()
    {
        return List.ReCycle();
    }
}

public static class OrganExtension
{
    public static bool HaveDick(this Organs orgs) => orgs.Dicks.List.Count > 0;

    public static bool HaveDick(this Organs orgs, float minSize) => orgs.Dicks.List.Count > 0 ? orgs.Dicks.List.BiggestSize() >= minSize : false;

    public static bool HaveBalls(this Organs organs) => organs.Balls.List.Count > 0;

    public static bool HaveBalls(this Organs organs, float minSize) => organs.Balls.List.Count > 0 ? organs.Balls.List.BiggestSize() >= minSize : false;

    public static bool HaveVagina(this Organs organs) => organs.Vaginas.List.Count > 0;

    public static bool HaveVagina(this Organs organs, float minSize) => organs.Vaginas.List.Count > 0 ? organs.Vaginas.List.BiggestSize() >= minSize : false;

    // if have boobs check if boobs are big enough.
    public static bool HaveBoobs(this Organs organs) => organs.Boobs.List.Count > 0 ? organs.Boobs.List.BiggestSize() > 3 : false;

    public static bool HaveBoobs(this Organs organs, float minSize) => organs.Boobs.List.Count > 0 ? organs.Boobs.List.BiggestSize() >= minSize : false;
}