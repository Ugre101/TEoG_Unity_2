using UnityEngine;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [SerializeField]
    [TextArea]
    private string perkInfo = "";

    public string Info => perkInfo;
    [Tooltip(" ")]
    [SerializeField]
    private float value = 0;

    public float Value => value;
}
/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces. 
*/
