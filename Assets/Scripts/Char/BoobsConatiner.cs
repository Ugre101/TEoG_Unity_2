using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BoobsConatiner : OrganWithFluidContainer
{
    [SerializeField] private List<Boobs> balls = new List<Boobs>();
    public List<Boobs> List => balls;
    [SerializeField] private CharStats boobsBonusRefillRate = new CharStats();
    public CharStats BoobsBonusRefillRate => boobsBonusRefillRate;
    [SerializeField] private bool lactating = false;

    public bool Lactating => lactating;
    public override float FluidSlider => List.FluidCurrentTotal() / List.FluidMax();

    public override string FluidStatus => Settings.LorGal(List.FluidCurrentTotal() / 1000);

    public override string LooksWithOutFluids => throw new System.NotImplementedException();

    public override string Looks => List.Looks();

    public override float AddCost => List.Cost();

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override void AddNew()
    {
        List.Add(new Boobs());
        InvokeOrganChange();
    }

    public override void AddNew(int baseSize)
    {
        List.Add(new Boobs(baseSize));
        InvokeOrganChange();
    }

    public override float ReCycle()
    {
        return List.ReCycle();
    }

    public void Remove(Boobs toRemove)
    {
        List.Remove(toRemove);
        InvokeOrganChange();
    }

    public override void ReBind()
    {
        List.ForEach(o => o.SomethingChanged -= InvokeOrganChange);
        List.ForEach(o => o.SomethingChanged += InvokeOrganChange);
    }
}
