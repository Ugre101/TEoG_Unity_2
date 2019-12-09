using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMasc : SexScenes
{
    public override string StartScene(PlayerMain player, ThePrey other)
    {
        float toGive = player.EssGive;
        player.LoseMasc(toGive);
        other.Masc.Gain(toGive);
        return "Give masc";
    }
}
