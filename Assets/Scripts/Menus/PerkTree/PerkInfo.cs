using UnityEngine;
[CreateAssetMenu(fileName = "Perk info",menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [SerializeField]
    [TextArea]
    private string perkInfo = "";
    public string Info => perkInfo;
}
// store perk info in a ScriptableObject so that it's consistent
