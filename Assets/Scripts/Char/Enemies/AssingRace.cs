using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AssingRace
{
    public List<WeightedRace> Options;

    public void AddOption()
    {
        Options.Add(new WeightedRace());
    }

    /// <summary>
    /// Weighted random roll for starting race
    /// </summary>
    /// <returns></returns>
    public Races GetRace()
    {
        if (Options.Count == 0)
        {
            return Races.Humanoid;
        }
        // remove values with weight = 0.
        Options.RemoveAll(w => w.Weight == 0);
        // Get the total sum of all values
        int max = Options.Sum(r => r.Weight);
        int acc = 0;
        foreach (WeightedRace weighted in Options)
        {
            acc += weighted.Weight;
            weighted.Weight = acc;
        }
        int rnd = Random.Range(0, max);
        foreach (WeightedRace weighted in Options)
        {
            if (rnd < weighted.Weight)
            {
                return weighted.Race;
            }
        }
        return Races.Humanoid;
    }
}

[System.Serializable]
public class WeightedRace
{
    public Races Race;

    [Range(0, 10)]
    public int Weight = 0;
}