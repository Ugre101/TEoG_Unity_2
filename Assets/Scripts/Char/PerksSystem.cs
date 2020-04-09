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
    Seductress,
    EssenceBandit,
    EssenceTransformer,
}

[System.Serializable]
public abstract class PerkBase
{
    [SerializeField] protected int lvl = 1;

    public int Level => lvl;

    public void LevelUp() => lvl++;
}

[System.Serializable]
public class Perk : PerkBase
{
    [SerializeField] private PerksTypes type;

    public PerksTypes Type => type;

    public Perk(PerksTypes type) => this.type = type;
}

[System.Serializable]
public class Perks
{
    [SerializeField] private List<Perk> perkList = new List<Perk>();

    public List<Perk> List => perkList;

    public bool HasPerk(PerksTypes type) => perkList.Exists(p => p.Type == type);

    public Perk GetPerk(PerksTypes type) => perkList.Find(p => p.Type == type);

    public int GetPerkLevel(PerksTypes type) => HasPerk(type) ? GetPerk(type).Level : 0;

    public bool NotMaxLevel(PerksTypes type, int maxLevel)
    {
        if (HasPerk(type))
        {
            return GetPerk(type).Level < maxLevel;
        }
        return false;
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
        if (basicChar.Perks.HasPerk(type))
        {
            basicChar.Perks.GetPerk(type).LevelUp(); ;
        }
        else
        {
            perkList.Add(new Perk(type));
        }
    }
}