using System.Collections.Generic;
using UnityEngine;

public class Looks
{
    public Looks(BasicChar whom)
    {
        who = whom;
    }

    private Settings settings => who.BasicCharGame.Settings;

    [SerializeField]
    private BasicChar who;

    private string Height { get { return settings.MorInch(who.Weight); } }
    private string Weight { get { return settings.KgorP(who.Weight); } }
    private string Name { get { return who.FullName; } }
    private string Race { get { return who.Race; } }
    private string Gender { get { return settings.GetGender(who); } }
    private string Age { get { return $"{who.Age.AgeYears}years old"; } }

    public string Summary
    {
        get
        {
            string title = Name;
            string desc = $"A {Height} tall {Race} {Gender}";
            string stats = $" {Age}\nWeight: {Weight}\nHeight: {Height}";
            return $" {title}\n\n{desc}\n{stats}";
        }
    }

    public string Organs
    {
        get
        {
            string dicks = DicksLook();
            // and the rest
            return dicks;
        }
    }

    public string DickLook(int i)
    {
        Dick d = who.SexualOrgans.Dicks[Mathf.Clamp(i, 0, who.SexualOrgans.Dicks.Count - 1)];
        return who.SexualOrgans.Dicks.Count > 0 ? $"a {settings.MorInch(d.Size)} long dick" : "";
    }

    public string DicksLook()
    {
        List<Dick> dicks = who.SexualOrgans.Dicks;
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

    public string BallLook(int i)
    {
        Balls b = who.SexualOrgans.Balls[Mathf.Clamp(i, 0, who.SexualOrgans.Balls.Count - 1)];
        return $"a pair of {settings.MorInch(b.Size)} wide balls";
    }

    public string BallsLook()
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

    public string BoobLook(Boobs b)
    {
        return $"an {BoobSizeConvertor(b.Size)} chest";
    }

    public string BoobsLook()
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

    public string BoobSizeConvertor(float size)
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

    public string VagLook(Vagina vag)
    {
        return $"a {vag.Size} deep vagina";
    }

    public string VagsLook()
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