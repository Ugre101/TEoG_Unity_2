using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PregnancySystem
{

    [SerializeField]
    private List<Child> children = new List<Child>();


    [SerializeField]
    private float virility = 1f;

    [SerializeField]
    private float fertility = 1f;

    public float GetVirility => virility;

    public void SetVirility(float parChange) => virility += parChange;

    public float GetFertility => fertility;

    public void SetFertility(float parChange) => fertility += parChange;

 

    // growth in days
    private readonly float baseFetusGrowth = 1f;

    [SerializeField]
    private float bonusFetusGrowth = 0f;

    private float FinalGrowthRate => baseFetusGrowth + bonusFetusGrowth;

    public void GrowFetus(BasicChar who)
    {
        foreach (Vagina v in who.SexualOrgans.Vaginas.FindAll(v => v.Womb.HasFetus))
        {
            if (v.Womb.Grow(FinalGrowthRate))
            {
                List<Child> born = v.Womb.GiveBirth();
                children.AddRange(born);
                string amount = born.Count > 1 ? $"a pair of twins babies" : "one baby"; // TODO add more
                string addText = who.CompareTag("Player") ? $"You have given birth to {amount}."
                    : $"{who.FullName} has given birth to {amount}";
                who.BasicCharGame.EventLog.AddTo(addText);
            }
        }
    }

    public void GrowChild()
    {
        foreach (Child c in children)
        {
            c.Grow();
        }
    }
}
public static class PregnancyExtensions
{
    public static bool Impregnate(this BasicChar mother, BasicChar parFather)
    {
        float motherFet = mother.PregnancySystem.GetFertility;
        float fatherVir = parFather.PregnancySystem.GetVirility;
        float motherRoll = Random.Range(0 - motherFet, 200 - motherFet);
        float fatherRoll = Random.Range(0 + fatherVir, 100 + fatherVir);
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
}