using UnityEngine;
[CreateAssetMenu(fileName = "Perk info",menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [SerializeField]
    [TextArea]
    private string perkInfo = "";
    public string Info => perkInfo;
    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;
}
