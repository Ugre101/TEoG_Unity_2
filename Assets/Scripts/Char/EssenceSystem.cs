using System;
using UnityEngine;

[Serializable]
public class EssenceSystem
{
    [SerializeField]
    private Essence masc = new Essence();

    [SerializeField]
    private Essence femi = new Essence();

    public Essence Masc => masc;
    public Essence Femi => femi;

    public float bonusDrain = 0;

    /// <summary>
    /// Base value drain, add perk bonuses after localy.
    /// </summary>
    public float BaseEssDrain => 3 + bonusDrain;

    public float bonusGive = 0;

    /// <summary>
    /// Base value give, add perk bonuses after localy
    /// </summary>
    public float BaseEssGive => 3 + bonusGive;
}