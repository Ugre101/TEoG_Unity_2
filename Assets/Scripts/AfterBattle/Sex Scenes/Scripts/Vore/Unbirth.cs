﻿using UnityEngine;
using Vore;

[CreateAssetMenu(fileName = "Unbirth", menuName = ("Sex/Vore/Unbirth"))]
public class Unbirth : VoreScene
{
    public override bool CanDo(BasicChar player, ThePrey Other)
    {
        if (player.SexualOrgans.Vaginas.Count < 1)
        {
            return false;
        }
        return player.Vore.Vagina.CanVore(Other);
    }

    public override string Vore(PlayerMain player, ThePrey other)
    {
        if (player.Vore.Vagina.Vore(other))
        {
            player.VoreChar.Vagina.AddPrey(other);
            return $"Grabbing your opponent, you shove into your pussy!";
        }
        else
        {
            return $"You cannot fit more into your vagina!";
        }
    }
}