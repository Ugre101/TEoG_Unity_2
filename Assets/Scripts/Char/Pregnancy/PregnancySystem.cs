using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PregnancySystem
{
    [SerializeField] private List<Child> children = new List<Child>();

    public List<Child> Children => children;

    [SerializeField] private CharStats virility = new CharStats(1);

    public CharStats Virility => virility;

    [SerializeField] private CharStats fertility = new CharStats(1);

    public CharStats Fertility => fertility;

    // growth in days
    private readonly float baseFetusGrowth = 1f;

    [SerializeField] private float bonusFetusGrowth = 0f;

    public float FinalGrowthRate => baseFetusGrowth + bonusFetusGrowth;

    public void GrowChild() => Children.ForEach(c => c.Grow());
}

public static class PregnancyExtensions
{
    public static bool Impregnate(this BasicChar mother, BasicChar parFather)
    {
        float motherFet = mother.PregnancySystem.Fertility.Value,
            fatherVir = parFather.PregnancySystem.Virility.Value;
        float motherRoll = Random.Range(0 - motherFet, 200 - motherFet),
            fatherRoll = Random.Range(0 + fatherVir, 100 + fatherVir);
        if (motherRoll < fatherRoll)
        {
            // if mother has empty womb then impregnate first empty womb
            if (mother.SexualOrgans.Vaginas.Exists(v => !v.Womb.HasFetus))
            {
                mother.SexualOrgans.Vaginas.Find(v => !v.Womb.HasFetus).Womb.GetImpregnated(mother, parFather);
                return true;
            }
        }
        return false;
    }

    public static void GrowFetuses(this BasicChar mother)
    {
        foreach (Vagina v in mother.SexualOrgans.Vaginas.FindAll(v => v.Womb.HasFetus))
        {
            PregnancySystem pregnancySystem = mother.PregnancySystem;
            if (v.Womb.Grow(pregnancySystem.FinalGrowthRate))
            {
                List<Child> born = v.Womb.GiveBirth();
                pregnancySystem.Children.AddRange(born);
                string amount = born.Count > 1 ? $"a pair of twins babies" : "one baby"; // TODO add more
                string addText = mother.CompareTag(PlayerMain.GetPlayer.tag)
                    ? $"You have given birth to {amount}."
                    : $"{mother.Identity.FullName} has given birth to {amount}";
                EventLog.AddTo(addText);
            }
        }
    }
}