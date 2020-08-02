using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class AnalsContainer : OrganWithFluidContainer
{
    [SerializeField] private List<Anal> anals = new List<Anal>();
    public List<Anal> List => anals;
    public override float FluidSlider => List.FluidCurrentTotal() / List.FluidMax();

    public override string FluidStatus => (List.FluidCurrentTotal() / 1000).LorGal();

    public override string Looks
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Anal anal = List[i];
                if (i == 0)
                {
                    builder.Append(anal.Look());
                }
                else
                {
                    builder.Append(anal.Look(false));
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
                Anal anal = List[i];
                if (i == 0)
                {
                    // TODO    builder.Append(anal.LookWithOutFluid());
                }
                else
                {
                    // TODO   builder.Append(anal.LookWithOutFluid(false));
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

    public override void AddNew()
    {
        List.Add(new Anal());
        InvokeOrganChange();
    }

    public override void AddNew(int baseSize)
    {
        List.Add(new Anal(baseSize));
        InvokeOrganChange();
    }

    public void Remove(Anal anal)
    {
        List.Remove(anal);
        InvokeOrganChange();
    }

    public override float ReCycle()
    {
        Anal toShrink = List[List.Count - 1];
        if (toShrink.Shrink())
        {
            Remove(toShrink);
            return 30f;
        }
        return toShrink.Cost;
    }

    public override void ReBind()
    {
        List.ForEach(o => o.SomethingChanged -= InvokeOrganChange);
        List.ForEach(o => o.SomethingChanged += InvokeOrganChange);
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

    public override string FluidStatus => (List.FluidCurrentTotal() / 1000).LorGal();

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

    public override void AddNew()
    {
        List.Add(new Balls());
        InvokeOrganChange();
    }

    public override void AddNew(int baseSize)
    {
        List.Add(new Balls(baseSize));
        InvokeOrganChange();
    }

    public void Remove(Balls balls)
    {
        List.Remove(balls);
        InvokeOrganChange();
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

    public override void ReBind()
    {
        List.ForEach(o => o.SomethingChanged -= InvokeOrganChange);
        List.ForEach(o => o.SomethingChanged += InvokeOrganChange);
    }
}