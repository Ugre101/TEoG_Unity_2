using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[Serializable]
public class PregnancySystem
{
    public PregnancySystem(BasicChar parWho)
    {
        who = parWho;
    }
    [SerializeField]
    private BasicChar who;
    private List<Child> children = new List<Child>();

    [SerializeField]
    private float virility = 1f;

    [SerializeField]
    private float fertility = 1f;

    public float GetVirility => virility;

    public void SetVirility(float parChange) => virility += parChange;

    public float GetFertility => fertility;

    public void SetFertility(float parChange) => fertility += parChange;

    public bool Impregnate(BasicChar parFather)
    {
        // TODO add random.range where, motherRoll gets smaller and father higher
        float motherRoll = GetFertility;
        float fatherRoll = parFather.PregnancySystem.GetVirility;
        if (motherRoll < fatherRoll)
        {
            // if mother has empty womb then impregnate first empty womb
            if (who.Vaginas.Exists(v => !v.Womb.HasFetus))
            {
                who.Vaginas.Find(v => !v.Womb.HasFetus).Womb.GetImpregnated(who, parFather);
            }
        }
        return false;
    }

    // growth in days
    private readonly float baseFetusGrowth = 1f;
    [SerializeField]
    private float bonusFetusGrowth = 0f;
    private float FinalGrowthRate => baseFetusGrowth + bonusFetusGrowth;

    public void GrowFetus()
    {
        foreach (Vagina v in who.Vaginas.FindAll(v => v.Womb.HasFetus))
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
    public void GrowAll()
    {
        GrowFetus();
        GrowChild();
    }
}