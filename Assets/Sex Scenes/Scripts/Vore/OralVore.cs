using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OralVore", menuName = ("Sex/OralVore"))]
public class OralVore : VoreScene
{
    public override string Text(BasicChar player, BasicChar other)
    {
        string text = $"OralVore";
        return text;
    }
}