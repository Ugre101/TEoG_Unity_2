using System.Collections.Generic;
using UnityEngine;

public enum PerksTypes
{
    FasterRest,
    Gluttony,
    Delicate,
    EnhancedSenses,
    EssenceFlow,
    Thug,
    Greedy,
    EssenceThief,
    EssenceHoarder,
    EssenceShaper,
    HealthyBody,
    StrongMind,
    LowMetabolism,
    Seductress
}

[System.Serializable]
public class Perk
{
    [SerializeField]
    private int lvl;

    public int Level => lvl;

    public void LevelUp() => lvl++;

    [SerializeField]
    private PerksTypes type;

    public PerksTypes Type => type;

    public Perk(PerksTypes type)
    {
        this.lvl = 1;
        this.type = type;
    }
}

[System.Serializable]
public class Perks
{
    [SerializeField]
    private List<Perk> perkList = new List<Perk>();

    public List<Perk> List => perkList;

    public bool HasPerk(PerksTypes type) => perkList.Exists(p => p.Type == type);

    public Perk GetPerk(PerksTypes type) => perkList.Find(p => p.Type == type);

    public int GetPerkLevel(PerksTypes type) => HasPerk(type) ? perkList.Find(p => p.Type == type).Level : 0;

    public bool NotMaxLevel(PerksTypes type, int maxLevel)
    {
        if (perkList.Exists(p => p.Type == type))
        {
            return perkList.Find(p => p.Type == type).Level < maxLevel;
        }
        return false;
    }

    public string DisplayPerk(PerksTypes type)
    {
        switch (type)
        {
            case PerksTypes.FasterRest:
                return perkList.Exists(p => p.Type == type) ? $"Faster rest: {perkList.Find(p => p.Type == type).Level}" : "Faster rest";

            default:
                return "";
        }
    }
}

public static class PerkExtensions
{
    public static void GainPerk(this BasicChar basicChar, PerksTypes type)
    {
        List<Perk> perkList = basicChar.Perks.List;

        switch (type)
        {
            case PerksTypes.FasterRest:
                break;

            case PerksTypes.Gluttony:
                basicChar.HP.Recovery.AddMods(PerkEffects.Gluttony.ExtraRecovery(HealthTypes.Health));
                basicChar.WP.Recovery.AddMods(PerkEffects.Gluttony.ExtraRecovery(HealthTypes.WillPower));
                break;

            case PerksTypes.EssenceFlow:
                break;

            case PerksTypes.EssenceThief:
                basicChar.Essence.StableEssence.AddMods(PerkEffects.EssenecePerks.EssThief.ImproveCapacity);
                break;

            case PerksTypes.EssenceHoarder:
                break;

            case PerksTypes.HealthyBody:
                break;

            case PerksTypes.StrongMind:
                break;

            case PerksTypes.LowMetabolism:
                break;

            case PerksTypes.Delicate:
                break;

            case PerksTypes.EnhancedSenses:
                break;

            case PerksTypes.Thug:
                break;

            case PerksTypes.Greedy:
                break;

            case PerksTypes.EssenceShaper:
                break;

            case PerksTypes.Seductress:
                basicChar.Stats.Charm.AddMods(PerkEffects.Seductress.CharmMod);
                break;
        }
        if (perkList.Exists(p => p.Type == type))
        {
            perkList.Find(p => p.Type == type).LevelUp(); ;
        }
        else
        {
            perkList.Add(new Perk(type));
        }
    }
}