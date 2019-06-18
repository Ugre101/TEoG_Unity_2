using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Looks 
{
    public Looks(BasicChar whom) { who = whom; }
    public Settings settings;
    private BasicChar who;
    private string newLine = Environment.NewLine;

    public string Summary
    {
        get
        {
            string name = $"{who.FullName}";
            string weight = $"{settings.KgorP(who.weight)}";
            string height = $"{settings.MorInch(who.height)}";
            string look = $"{who.FullName}"; 
            return look;
        }
    }
    public string Organs
    {
        get
        {
            string dicks = DicksLook(who.Dicks);
            // and the rest
            return dicks;
        }
    }
    public string DickLook(Dick d)
    {
        return $"a {settings.MorInch(d.Size)} long dick";
    }

    public string DicksLook(List<Dick> dicks)
    {
        string dLooks = "";
        for (int i = 0; i < dicks.Count; i++)
        {
            Dick d = dicks[i];
            string size = settings.MorInch(d.Size);
            if (i == 0)
            {
                dLooks += $"A {size} long dick";
            }
            else
            {
                dLooks += $"a {size} long dick";
            }
            if (i == dicks.Count - 1)
            {
                dLooks += ".";
            }
            else if (i == dicks.Count - 2)
            {
                dLooks += " and ";
            }
            else
            {
                dLooks += ", ";
            }
        }
        return dLooks;
    }
    public string BallLook(Balls b)
    {
        return $"a pair of {settings.MorInch(b.Size)} wide balls";
    }
    public string BallsLook()
    {
        List<Balls> balls = who.Balls;
        string bLooks = "";
        for ( int i = 0; i < balls.Count; i++)
        {
            if (i == 0)
            {

            }else
            {

            }
            if (i == balls.Count - 1)
            {

            }else if (i == balls.Count - 2)
            {

            }else
            {

            }
        } 
        return bLooks;
    }
    public string BoobLook(Boobs b)
    {
        return $"";
    }
    public string BoobsLook(List<Boobs> boobs)
    {
        string bLooks = "";
        for (int i = 0; i < boobs.Count; i++)
        {
            if (i == 0)
            {

            }
            else
            {

            }
            if (i == boobs.Count - 1)
            {

            }
            else if (i == boobs.Count - 2)
            {

            }
            else
            {

            }
        }
        return bLooks;
    }
    public string VagLook(Vagina vag)
    {
        return $"";
    }
    public string VagsLook(List<Vagina> vaginas)
    {
        string vLooks = "";
        for (int i = 0; i < vaginas.Count; i++)
        {
            if (i == 0)
            {

            }else
            {

            }
            if (i == vaginas.Count -1)
            {

            }else if (i == vaginas.Count - 2)
            {

            }else
            {

            }
        }
        return vLooks;
    }
}
