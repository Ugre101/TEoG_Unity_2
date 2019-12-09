using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : SexScenes
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
        float have = other.LoseFemi(ToDrain(player));
        player.Femi.Gain(have);
        return "Drain femi";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}