public class GiveMasc : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float toGive = player.EssGive();
        player.LoseMasc(toGive);
        other.Masc.Gain(toGive);
        return "Give masc";
    }
}