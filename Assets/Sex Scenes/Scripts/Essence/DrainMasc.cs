using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainMasc : SexScenes
{
    public override string StartScene(playerMain player, BasicChar other)
    {
        float toDrain = player.EssDrain;
        float have = 0f;
        have += other.LoseMasc(toDrain);
        player.Masc.Gain(have);
        return "Drain masc";
    }
}
