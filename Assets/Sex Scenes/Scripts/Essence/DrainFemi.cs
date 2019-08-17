using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainFemi : SexScenes
{
    public override string StartScene(playerMain player, BasicChar other)
    {
        float toDrain = player.EssDrain;
        float have = 0f;
        other.LoseFemi(toDrain);
        player.Femi.Gain(have);
        return "Drain femi";
    }
}
