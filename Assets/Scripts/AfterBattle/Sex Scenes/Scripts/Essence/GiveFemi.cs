using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveFemi : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float toGive = player.EssGive();
        player.LoseFemi(toGive);
        other.Essence.Femi.Gain(toGive);
        return "Give femi";
    }
}
