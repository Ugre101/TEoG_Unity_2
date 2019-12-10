using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drain masc", menuName = ("Sex/Essence/Drain masc"))]
public class DrainMasc : SexScenes
{
    public PerkInfo essFlow;

    private float ToDrain(PlayerMain drainer)
    {
        float drain = drainer.EssDrain;
        if (drainer.Perks.HasPerk(PerksTypes.EssenceFlow))
        {
            drain += essFlow.PosetiveValue;
        }
        return drain;
    }
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float have = other.LoseMasc(ToDrain(player));
        player.Masc.Gain(have);
        return "Drain masc";
    }
    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
