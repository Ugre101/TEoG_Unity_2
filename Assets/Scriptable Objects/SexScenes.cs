using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "Sex scene", menuName = ("Sex/Test"))]
public class SexScenes : ScriptableObject
{
    [Header("Player needs")]
    public bool PlayerDick;
    public bool PlayerBalls;
    public bool PlayerBoobs;
    public bool PlayerVagina;
    [Header("Other needs")]
    public bool Dick;
    public bool Balls;
    public bool Boobs;
    public bool Vagina;
    public virtual bool CanDo(BasicChar player, BasicChar Other)
    {
        if (PlayerDick)
        {
            if (player.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBalls)
        {
            if (player.Balls.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBoobs)
        {
            if (player.Boobs.Max(b => b.Size) < 3)
            {
                return false;
            }
        }
        if (PlayerVagina)
        {
            if (player.Vaginas.Count < 1)
            {
                return false;
            }
        }
        if (Dick)
        {
            if (Other.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (Balls)
        {
            if (Other.Balls.Count < 1)
            {
                return false;
            }
        }
        if (Boobs)
        {
            if (Other.Boobs.Max(b => b.Size) < 3)
            {
                return false;
            }
        }
        if (Vagina)
        {
            if (Other.Vaginas.Count < 1)
            {
                return false;
            }
        }
        return true;
    }
    public virtual string Text(BasicChar player, BasicChar other)
    {
        string text = $"{player.FullName} friks {other.FullName}";
        return text;
    }
    public virtual void DoScene(playerMain player, BasicChar other)
    {
        player.sexStats.GainArousal(1);
        other.sexStats.GainArousal(2);
    }
}


