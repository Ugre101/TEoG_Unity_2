using System.Collections.Generic;
using UnityEngine;

public class VorePerksSystem
{
    [SerializeField] private List<VorePerk> vorePerks = new List<VorePerk>();
    public List<VorePerk> VorePerks => vorePerks;

    public bool HasPerk(VorePerks type) => VorePerks.Exists(p => p.Type == type);

    public VorePerk GetPerk(VorePerks type) => VorePerks.Find(p => p.Type == type);

    public int GetPerkLevel(VorePerks type) => HasPerk(type) ? GetPerk(type).Level : 0;

    public bool NotMaxLevel(VorePerks type, int maxLevel)
    {
        if (HasPerk(type))
        {
            return GetPerk(type).Level < maxLevel;
        }
        return false;
    }
}

public static class VorePerksExtensions
{
    public static void GainPerk(this BasicChar basicChar, VorePerks type)
    {
        switch (type)
        {
            case VorePerks.Elastic:
                break;

            case VorePerks.Compression:
                break;
        }
        if (basicChar.Vore.Perks.HasPerk(type))
        {
            basicChar.Vore.Perks.GetPerk(type).LevelUp();
        }
        else
        {
            basicChar.Vore.Perks.VorePerks.Add(new VorePerk(type));
        }
    }
}