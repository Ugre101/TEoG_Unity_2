using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Womb 
{
    private List<Fetus> fetuses = new List<Fetus>();

    public bool Grow()
    {
        bool waitingToBeBorn = false;
        foreach(Fetus f in fetuses)
        {
            if (f.Grow())
            {
                waitingToBeBorn = true;
            }
        }
        return waitingToBeBorn;
    }

    public List<Child> GiveBirth()
    {
        List<Child> children = new List<Child>();
        foreach(Fetus f in fetuses.FindAll(c => c.ReadyToBeBorn))
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
    public void GetImpregnated(BasicChar parMother, BasicChar parFather,float parMultiChildBonus = 0,float parBaseChildAmount = 1f)
    {
        int amount = Mathf.FloorToInt(Random.Range(1f, 2.1f + Mathf.Max(-1f, parMultiChildBonus)));
    }
}
