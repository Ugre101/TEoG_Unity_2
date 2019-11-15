using System;

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

    public bool CanDrainMasc => Masc.Amount > 0 || who.Balls.Count > 0 || who.Dicks.Count > 0;
    public bool CanDrainFemi => Femi.Amount > 0 || who.Boobs.Count > 0 || who.Dicks.Count > 0;
}