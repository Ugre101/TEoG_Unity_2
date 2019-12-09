
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatAss", menuName = ("Sex/Mouth/EatAss"))]
public class EatAss : SexScenes
{
    public override string StartScene(PlayerMain player, ThePrey other)
    {
        return $"Start";
    }
    public override string ContinueScene(PlayerMain player, ThePrey other)
    {
        return $"Contine";
    }
}
