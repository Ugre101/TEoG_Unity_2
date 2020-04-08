using UnityEngine;

[System.Serializable]
public class VorePerk : PerkBase
{
    [SerializeField] private VorePerks type;
    public VorePerks Type => type;

    public VorePerk(VorePerks type) => this.type = type;
}
