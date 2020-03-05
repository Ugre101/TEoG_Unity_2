using UnityEngine;

[CreateAssetMenu(fileName = "Stat info", menuName = "Perks/Stat info")]
public class StatInfo : BaseInfo
{
    [SerializeField] private StatTypes stat = StatTypes.Charm;
    public StatTypes Stat => stat;
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/