using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Doggystyle", menuName = ("Sex/Dick/Doggystyle"))]
public class DoggyStyle : SexScenes
{
    public override string Text(BasicChar player, BasicChar other)
    {
        string text = $"DoggyStyle";
        return text;
    }
}