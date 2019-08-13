using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missonary", menuName = ("Sex/Dick/Missonary"))]
public class Missonary : SexScenes
{
    public override string Text(BasicChar player, BasicChar other)
    {
        string text = $"missonary";
        return text;
    }
}