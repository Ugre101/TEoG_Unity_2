
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatAss", menuName = ("Sex/EatAss"))]
public class EatAss : SexScenes
{
    public override string Text(BasicChar player, BasicChar other)
    {
        return $"You eat {other.firstName} ass.";
    }
}
