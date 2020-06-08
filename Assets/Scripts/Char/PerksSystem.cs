using System.Collections.Generic;
using UnityEngine;

public enum PerksTypes
{
    #region Existing perks dont touch unless necesary
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
    #endregion

    SingleMother,
    MasculineVacuum,
    FemenineVacuum,
    MasculineFlow,
    FemenineFlow,
    HermaphroditeVacuum,
    // Remember to always add new perks last in list to avoid breaking saves
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