
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatAss", menuName = ("Sex/Mouth/EatAss"))]
public class EatAss : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        return $"Start";
    }
    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return $"Contine";
    }
}
