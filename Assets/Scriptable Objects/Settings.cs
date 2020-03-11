using System;
using UnityEngine;

public static class Settings
{
    public static void Save()
    {
        UgreTools.SetPlayerPrefBool("Imperial", Imperial);
        UgreTools.SetPlayerPrefBool("Vore", Vore);
        PlayerPrefs.SetString("Male", Male);
        PlayerPrefs.SetString("Female", Female);
        PlayerPrefs.SetString("Herm", Herm);
        PlayerPrefs.SetString("Cuntboy", Cuntboy);
        PlayerPrefs.SetString("Dickgirl", Dickgirl);
        PlayerPrefs.SetString("Doll", Doll);
    }

    public static void Load()
    {
        Imperial = UgreTools.GetPlayerPrefBool("Imperial");
        Vore = UgreTools.GetPlayerPrefBool("Vore");
        Male = PlayerPrefs.GetString("Male", Male);
        Female = PlayerPrefs.GetString("Female", Female);
        Herm = PlayerPrefs.GetString("Herm", Herm);
        Cuntboy = PlayerPrefs.GetString("Cuntboy", Cuntboy);
        Dickgirl = PlayerPrefs.GetString("Dickgirl", Dickgirl);
        Doll = PlayerPrefs.GetString("Doll", Doll);
    }

    private static bool imperial = false;

    public static bool Imperial
    {
        get => imperial;
        set
        {
            Inch = value;
            Pound = value;
            imperial = value;
        }
    }

    public static bool Inch { get; private set; } = false;
    public static bool Pound { get; private set; } = false;
    public static bool Vore { get; private set; } = false;
    public static bool Scat { get; private set; } = false;

    public static bool ToogleImperial() => Imperial = !Imperial;

    public static bool ToogleVore() => Vore = !Vore;

    public static bool ToogleScat() => Scat = !Scat;

    public static string LorGal(float L)
    {
        if (L == 0) { return "empty"; }
        if (Imperial)
        {
            return Mathf.Floor(0.264172052f * L) < 1 ?
                $"{Mathf.Round(L * 4.22675284f)}cups" : $"{Mathf.Round(L * 0.264172052f)}gallon";
        }
        else
        {
            return L < 0.1f ? $"{Mathf.Round(L * 100)}cl" : L < 0.99 ? $"{Mathf.Round(L * 10)}dl" : $"{(float)Math.Round(L, 1)}L";
        }
    }

    public static string MorInch(float cm)
    {
        if (cm == 0) { return "zero?"; }
        if (Imperial)
        {
            float Inch = Mathf.Round(cm / 2.54f);
            float Feet = Mathf.Floor(Inch / 12);
            float Yard = Mathf.Floor(Feet / 3);
            return Yard > 0 ? $"{Yard}yard" : Feet > 0 ? $"{Feet}feet" : Inch > 0 ? $"{Inch}inches" : $"less than one inch";
        }
        else
        {
            float m = (float)Math.Round(cm / 100, 1);
            return m > 5f ? $"{m}m" : $"{Mathf.Round(cm)}cm";
        }
    }

    public static string KgorP(float kg)
    {
        if (kg <= 0) { return "zero?"; }
        if (Imperial)
        {
            // int stone = Mathf.FloorToInt(kg * 0.15747304441777f); // when to use stone?
            float pound = Mathf.Round(kg * 2.20462262f);
            float ounce = Mathf.Round(kg * 35.27396194958f);
            return pound < 1 ? $"{ounce}oz" : $"{pound}lbs";
        }
        else
        {
            if (kg > 1000)
            {
                int tonne = Mathf.FloorToInt(kg / 1000);
                string toReturn = tonne + "tonne";
                int left = Mathf.FloorToInt(kg - (tonne * 1000));
                if (left > 99)
                {
                    toReturn += $" and {left}kg";
                }
                return toReturn;
            }
            else if (kg > 1) { return Mathf.FloorToInt(kg) + "kg"; }
            else { return Mathf.Ceil(kg * 1000) + "g"; }
        }
    }

    public static string Male = "male";
    public static string Female = "female";
    public static string Herm = "herm";
    public static string Cuntboy = "cuntboy";
    public static string Dickgirl = "dickgirl";
    public static string Doll = "doll";

    public static string GetGender(BasicChar who, bool capital = false)
    {
        switch (who.Gender)
        {
            case Genders.Herm: return CapOrLower(Herm);

            case Genders.Male: return CapOrLower(Male);

            case Genders.Female: return CapOrLower(Female);

            case Genders.Dickgirl: return CapOrLower(Dickgirl);

            case Genders.Cuntboy: return CapOrLower(Cuntboy);

            case Genders.Doll:
            default:
                return CapOrLower(Doll);
        }
        string CapOrLower(string gender) => capital ? char.ToUpper(gender[0]) + gender.Substring(1) : gender.ToLower();
    }
}