using System.Collections.Generic;
using UnityEngine;

public class PregnancySystem
{
    public PregnancySystem(BasicChar parWho)
    {
        who = parWho;
    }

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

    // growth in days
    private float baseFetusGrowth = 1f;

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