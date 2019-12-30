using System.Collections.Generic;
using UnityEngine;

public static class Looks
{
    private static string Height(this BasicChar who) => Settings.MorInch(who.Weight);

    private static string Weight(this BasicChar who) => Settings.KgorP(who.Weight);

    public static string Summary(this BasicChar who)
    {
        string title = who.Identity.FullName;
        string desc = $"A {Height(who)} tall {who.Race} {who.Gender.ToString()}";
        string stats = $" {who.Age.AgeYears}years old\nWeight: {Weight(who)}\nHeight: {Height(who)}";
        return $" {title}\n\n{desc}\n{stats}";
    }

    public static string Organs(this BasicChar who)
    {
        string dicks = DicksLook(who);
        // and the rest
        return dicks;
    }

    public static string DickLook(this BasicChar who, int i)
    {
        Dick d = who.SexualOrgans.Dicks[Mathf.Clamp(i, 0, who.SexualOrgans.Dicks.Count - 1)];
        return who.SexualOrgans.Dicks.Count > 0 ? $"a {Settings.MorInch(d.Size)} long dick" : "";
    }

    public static string DicksLook(this BasicChar who)
    {
        List<Dick> dicks = who.SexualOrgans.Dicks;
        string dLooks = "";
        for (int i = 0; i < dicks.Count; i++)
        {
            Dick d = dicks[i];
            string size = Settings.MorInch(d.Size);
            if (i == 0)
            {
                dLooks += $"A {size} long dick";
            }
            else
            {
                dLooks += $"a {size} long dick";
            }
            if (i == dicks.Count - 2)
            {
                dLooks += " and ";
            }
            else
            {
                dLooks += ", ";
            }
        }
        return dLooks + ".";
    }

    public static string BallLook(this BasicChar who, int i)
    {
        Balls b = who.SexualOrgans.Balls[Mathf.Clamp(i, 0, who.SexualOrgans.Balls.Count - 1)];
        return $"a pair of {Settings.MorInch(b.Size)} wide balls";
    }

    public static string BallsLook(this BasicChar who)
    {
        List<Balls> balls = who.SexualOrgans.Balls;
        string bLooks = "";
        for (int i = 0; i < balls.Count; i++)
        {
            if (i == 0)
            {
                bLooks += $"";
            }
            else
            {
                bLooks += $"";
            }
            if (i == balls.Count - 2)
            {
                bLooks += $" and ";
            }
            else
            {
                bLooks += $", ";
            }
        }
        return bLooks + ".";
    }

    public static string BoobLook(Boobs b) => $"an {BoobSizeConvertor(b.Size)} chest";

    public static  string BoobsLook(this BasicChar who)
    {
        List<Boobs> boobs = who.SexualOrgans.Boobs;
        string bLooks = "";
        for (int i = 0; i < boobs.Count; i++)
        {
            Boobs b = boobs[i];
            if (i == 0)
            {
                bLooks += $"An {BoobSizeConvertor(b.Size)} chest";
            }
            else
            {
                bLooks += $"{BoobSizeConvertor(b.Size)} chest";
            }
            if (i == boobs.Count - 2)
            {
                bLooks += $" and ";
            }
            else
            {
                bLooks += $", ";
            }
        }
        return bLooks + ".";
    }

    public static string BoobSizeConvertor(float size)
    {
        List<string> Bra = new List<string>
        {
            "flat","AA","A","B","C","D","DD", "Large F", "G", "Large G","H", "Large H","I", "Large I","J","Large J",
            "K","Large K","L","Large L","M","Large M","N","Large N","O","Large O","scale-breaking"
        };
        int i = Mathf.Clamp(Mathf.FloorToInt(size / 2), 0, Bra.Count - 1);

        string prefix()
        {
            if (i == Bra.Count - 1 || i < 2)
            {
                return "";
            }
            else
            {
                return "-cup";
            }
        };
        return Bra[i] + prefix();
    }

    public static string VagLook(Vagina vag) => $"a {vag.Size} deep vagina";

    public static string VagsLook(this BasicChar who)
    {
        List<Vagina> vaginas = who.SexualOrgans.Vaginas;
        string vLooks = "";
        for (int i = 0; i < vaginas.Count; i++)
        {
            Vagina v = vaginas[i];
            if (i == 0)
            {
                vLooks += $"A {v.Size} deep vagina";
            }
            else
            {
                vLooks += $"{v.Size} deep vagina";
            }
            if (i == vaginas.Count - 2)
            {
                vLooks += $" and ";
            }
            else
            {
                vLooks += $", ";
            }
        }
        return vLooks + ".";
    }
}