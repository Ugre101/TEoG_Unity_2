using UnityEngine;

[CreateAssetMenu(fileName = "Give femi", menuName = ("Sex/Essence/Give femi"))]
public class GiveFemi : EssScene
{
    public override bool CanDo(BasicChar basicChar)
    {
        return basicChar.Perks.HasPerk(PerksTypes.EssenceShaper) ? basicChar.SexStats.SessionOrgasm > 0 : false;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float toGive = player.EssGive();
        player.LoseFemi(toGive);
        other.Essence.Femi.Gain(toGive);
        return "Give femi";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}