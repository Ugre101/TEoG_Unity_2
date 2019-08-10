using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OralVore", menuName = ("Sex/OralVore"))]
public class OralVore : SexScenes
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (player.Vore.Active)
        {
            
        }else
        {
            return false;
        }
        return base.CanDo(player, Other);
    }
    public override string Text(BasicChar player, BasicChar other)
    {
        string text = $"OralVore";
        return text;
    }
}