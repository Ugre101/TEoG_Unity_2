using System.Collections.Generic;
using UnityEngine;

public class Womb
{
    private List<Fetus> fetuses = new List<Fetus>();
    public bool HasFetus => fetuses.Count > 0;

    public bool Grow(float parDaysToGrow = 1f)
    {
        bool waitingToBeBorn = false;
        foreach (Fetus f in fetuses)
        {
            if (f.Grow(parDaysToGrow))
            {
                waitingToBeBorn = true;
            }
        }
        return waitingToBeBorn;
    }

    public List<Child> GiveBirth()
    {
        List<Child> children = new List<Child>();
        foreach (Fetus f in fetuses.FindAll(c => c.ReadyToBeBorn))
        {
            children.Add(f.GiveBirth());
        }
        return children;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="parMother"></param>
    /// <param name="parFather"></param>
    /// <param name="parMultiChildBonus"> amount = floor(range(1,2.1)); where amount equals the amount of fetuses added to womb.</param>
    public void GetImpregnated(BasicChar parMother, BasicChar parFather, float parMultiChildBonus = 0, int parBaseChildAmount = 1)
    {
        int amount = Mathf.FloorToInt(Random.Range(parBaseChildAmount, 2.1f + Mathf.Max(-1f, parMultiChildBonus)));
        for (int i = 0; i < amount; i++)
        {
            // TODO give secondraces a slim chance to win and make it so babies can become half race of parents.
            // Roll is slighty biased for mother which is intended.
            int roll = Random.Range(0, 10);
            Races babyRace = roll < 5 ?
                parMother.RaceSystem.CurrentRace() : parFather.RaceSystem.CurrentRace();
            fetuses.Add(new Fetus(babyRace, parFather, parMother));
        }
    }
}