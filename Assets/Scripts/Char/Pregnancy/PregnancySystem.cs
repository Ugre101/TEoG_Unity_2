using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PregnancySystem
{
    [SerializeField] private List<Child> children = new List<Child>();

    public List<Child> Children => children;

    [SerializeField] private CharStats virility = new CharStats(1);

    public CharStats Virility => virility;

    public int VirilityValue
    {
        get
        {
            int baseVal = Virility.Value;
            if (PregnancyBlessings.HasBlessing(PregnancyBlessingsIds.VirileLoad))
            {
                // TODO decide moderfier for blessing; blessing * mod.
                baseVal += pregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.VirileLoad);
            }
            if (pregnancyBlessings.HasBlessing(PregnancyBlessingsIds.PrenancyFreak))
            {
                baseVal += PregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.PrenancyFreak);
            }
            return baseVal;
        }
    }

    [SerializeField] private CharStats fertility = new CharStats(1);

    public CharStats Fertility => fertility;

    public int FertilityValue
    {
        get
        {
            int baseVal = Fertility.Value;
            if (PregnancyBlessings.HasBlessing(PregnancyBlessingsIds.PrenancyFreak))
            {
                baseVal += pregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.PrenancyFreak);
            }
            return baseVal;
        }
    }

    // growth in days
    [SerializeField] private CharStats fetusGrowthRate = new CharStats(1);

    public CharStats FetusGrowthRate => fetusGrowthRate;
    [SerializeField] private PregnancyBlessings pregnancyBlessings = new PregnancyBlessings();
    public PregnancyBlessings PregnancyBlessings => pregnancyBlessings;

    public float FinalGrowthRate
    {
        get
        {
            int growthRate = FetusGrowthRate.Value;
            if (PregnancyBlessings.HasBlessing(PregnancyBlessingsIds.Incubator))
            {
                growthRate += pregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.Incubator);
            }
            return growthRate;
        }
    }

    public int FertilityShrinePoints => fertilityShrinePoints + Pregnant.Value + ImPregnated.Value;

    public void GrowChild() => Children.ForEach(c => c.Grow());

    [SerializeField] private DateSave lastTimePregnant;
    [SerializeField] private DateSave lastTimeImpregnatedSomeOne;
    [SerializeField] private FlagInt pregnant = new FlagInt();
    [SerializeField] private FlagInt imPregnated = new FlagInt();

    public FlagInt Pregnant => pregnant;
    public FlagInt ImPregnated => imPregnated;
    [SerializeField] private int fertilityShrinePoints = 0;

    public void AddFertilityShrinePoints(int toAdd) => fertilityShrinePoints += Mathf.Abs(toAdd);

    // TODO add last time impregnated & imprete add penalty for pregFreak
}

public static class PregnancyExtensions
{
    public static bool Impregnate(this BasicChar mother, BasicChar parFather)
    {
        float motherFet = mother.PregnancySystem.Fertility.Value,
            fatherVir = parFather.PregnancySystem.VirilityValue;
        float motherRoll = Random.Range(0 - motherFet, 200 - motherFet),
            fatherRoll = Random.Range(0 + fatherVir, 50 + fatherVir);
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
                mother.Events.SoloEvents.IGiveBirth(born);
                string amount = born.Count > 1 ? $"a pair of twins babies" : "one baby"; // TODO add more
                string addText = mother.CompareTag(PlayerMain.GetPlayer.tag)
                    ? $"You have given birth to {amount}."
                    : $"{mother.Identity.FullName} has given birth to {amount}";
                EventLog.AddTo(addText);
            }
        }
    }
}