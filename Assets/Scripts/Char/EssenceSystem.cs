using System;
using UnityEngine;
[Serializable]
public class EssenceSystem
{
    private BasicChar who;

    public EssenceSystem(BasicChar parWho)
    {
        who = parWho;
    }

    private Essence masc = new Essence();
    private Essence femi = new Essence();

    public Essence Masc { get => masc; }
    public Essence Femi { get => femi; }

    public float bonusDrain = 0;

    /// <summary>
    /// Base value drain, add perk bonuses after localy.
    /// </summary>
    public float BaseEssDrain => 3 + bonusDrain;

    public float bonusGive = 0;

    /// <summary>
    /// Base value give, add perk bonuses after localy
    /// </summary>
    public float baseEssGive => 3 + bonusGive;
}