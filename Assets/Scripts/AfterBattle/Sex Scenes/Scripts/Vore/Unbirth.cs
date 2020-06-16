using UnityEngine;
using Vore;

[CreateAssetMenu(fileName = "Unbirth", menuName = ("Sex/Vore/Unbirth"))]
public class Unbirth : VoreScene
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (!player.SexualOrgans.HaveVagina())
        {
            return false;
        }
        return player.Vore.Vagina.CanVore(Other);
    }

    public override string Vore(PlayerMain player, BasicChar other)
    {
        if (player.Vore.Vagina.Vore(other))
        {
            return $"Grabbing {other.Identity.FirstName}, you shove {other.HimHer()} into your pussy!";
        }
        else
        {
            return $"You cannot fit more into your vagina!";
        }
    }
}