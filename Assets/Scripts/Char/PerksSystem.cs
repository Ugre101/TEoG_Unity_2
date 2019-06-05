using System.Collections.Generic;
using UnityEngine;

public enum PerksTypes
{
    FasterRest,
    GiveEss,
    GainEss
}

[System.Serializable]
public class Perk
{
    [SerializeField]
    protected int _baseValue;

    [SerializeField]
    protected PerksTypes _type;

    public int Value { get { return _baseValue; } set { _baseValue = value; } }
    public PerksTypes Type { get { return _type; } }

    public Perk(PerksTypes type)
    {
        _baseValue = 1;
        _type = type;
    }
}

[System.Serializable]
public class Perks
{
    [SerializeField]
    protected List<Perk> perkList = new List<Perk>();

    public void GainPerk(PerksTypes type)
    {
        if (perkList.Exists(p => p.Type == type))
        {
            perkList.Find(p => p.Type == type).Value++;
        }
        else
        {
            perkList.Add(new Perk(type));
        }
    }

    public string DisplayPerk(PerksTypes type)
    {
        switch (type)
        {
            case PerksTypes.FasterRest:
                return perkList.Exists(p => p.Type == type) ? $"Faster rest: {perkList.Find(p => p.Type == type).Value}" : "Faster rest";

            case PerksTypes.GainEss:
                return perkList.Exists(p => p.Type == type) ? $"Drain essence: {perkList.Find(p => p.Type == type).Value}" : "Drain essence";

            case PerksTypes.GiveEss:
                return perkList.Exists(p => p.Type == type) ? $"Give essence: {perkList.Find(p => p.Type == type).Value}" : "Give essence";

            default:
                return "";
        }
    }
    public int PerkBonus(PerksTypes type)
    {
        switch (type)
        {
            case PerksTypes.FasterRest:
                return perkList.Exists(p => p.Type == type) ? perkList.Find(p => p.Type == type).Value : 0;

            case PerksTypes.GainEss:
                return perkList.Exists(p => p.Type == type) ? perkList.Find(p => p.Type == type).Value : 0;

            case PerksTypes.GiveEss:
                return perkList.Exists(p => p.Type == type) ? perkList.Find(p => p.Type == type).Value : 0;

            default:
                return 0;
        }
    }
}