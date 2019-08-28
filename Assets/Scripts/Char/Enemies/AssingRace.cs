using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class AssingRace
{
    public List<WeightedRace> Options = new List<WeightedRace>();
    public void AddOption()
    {
        Options.Add(new WeightedRace());
    }
    public Races GetRace()
    {
        int max = Options.Sum(r => r.Weight);
        Options.RemoveAll(w => w.Weight == 0);
        int acc = 0;
        foreach(WeightedRace weighted in Options)
        {
            acc = weighted.Weight;
            weighted.Weight = acc;
        }
        Debug.Log(max);
        int rnd = Random.Range(0, max);
        return Races.Humanoid;
    }
}
[System.Serializable]
public class WeightedRace
{
    public Races Race;
    [Range(0,10)]
    public int Weight = 0;
}
