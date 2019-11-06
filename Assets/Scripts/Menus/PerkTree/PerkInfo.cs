using UnityEngine;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [SerializeField]
    [TextArea]
    private string perkInfo = "";

    public string Info => perkInfo;

    [SerializeField]
    [TextArea]
    private string perkEffects = "";

    public string Effects => perkEffects;

    [Tooltip(" ")]
    [SerializeField]
    private float posValue = 0;
    [Tooltip(" ")]
    [SerializeField]
    private float negValue = 0;

    public float PosetiveValue => posValue;
    public float NegativeValue => negValue;
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/
