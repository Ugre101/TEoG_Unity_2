using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : SexScenes
{
    public PerkInfo essFlow;

    private float ToDrain(playerMain drainer)
    {
        float drain = drainer.EssDrain;
        if (drainer.Perks.HasPerk(PerksTypes.EssenceFlow))
        {
            drain += essFlow.PosetiveValue;
        }
        return drain;
    }

    public override string StartScene(playerMain player, BasicChar other)
    {
        float have = other.LoseFemi(ToDrain(player));
        player.Femi.Gain(have);
        return "Drain femi";
    }

    public override string ContinueScene(playerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}