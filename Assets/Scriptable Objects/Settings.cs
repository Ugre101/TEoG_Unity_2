using System;
using UnityEngine;

public static class Settings
{
    #region Save&Load

    public static void Save()
    {
        // Metrics & Units
        UgreTools.SetPlayerPrefBool(impSaveName, Imperial);
        UgreTools.SetPlayerPrefBool(inchSaveName, Inch);
        UgreTools.SetPlayerPrefBool(poundSaveName, Pound);
        UgreTools.SetPlayerPrefBool(gallonSaveName, Gallon);
        // Gender names
        PlayerPrefs.SetString("Male", Male);
        PlayerPrefs.SetString("Female", Female);
        PlayerPrefs.SetString("Herm", Herm);
        PlayerPrefs.SetString("Cuntboy", Cuntboy);
        PlayerPrefs.SetString("Dickgirl", Dickgirl);
        PlayerPrefs.SetString("Doll", Doll);
        // Fontsizes
        PlayerPrefs.SetFloat(eventLogFontSizeSaveName, EventLogFontSize);
        PlayerPrefs.SetFloat(combatLogFontSizeSaveName, CombatLogFontSize);
        PlayerPrefs.SetFloat(sexlogFontSizeSaveName, SexlogFontSize);
        // Misc
        // UgreTools.SetPlayerPrefBool("Vore", Vore);
        UgreTools.SetPlayerPrefBool("Scat", Scat);
    }

    public static void Load()
    {
        // Units
        Imperial = UgreTools.GetPlayerPrefBool(impSaveName);
        Inch = UgreTools.GetPlayerPrefBool(inchSaveName);
        Pound = UgreTools.GetPlayerPrefBool(poundSaveName);
        Gallon = UgreTools.GetPlayerPrefBool(gallonSaveName);
        // Genders
        Male = PlayerPrefs.GetString("Male", Male);
        Female = PlayerPrefs.GetString("Female", Female);
        Herm = PlayerPrefs.GetString("Herm", Herm);
        Cuntboy = PlayerPrefs.GetString("Cuntboy", Cuntboy);
        Dickgirl = PlayerPrefs.GetString("Dickgirl", Dickgirl);
        Doll = PlayerPrefs.GetString("Doll", Doll);
        // FontSizes
        EventLogFontSize = UgreTools.GetFloatPref(EventLogFontSize, eventLogFontSizeSaveName);
        CombatLogFontSize = UgreTools.GetFloatPref(CombatLogFontSize, combatLogFontSizeSaveName);
        SexlogFontSize = UgreTools.GetFloatPref(SexlogFontSize, sexlogFontSizeSaveName);
        // Misc
        //  Vore = UgreTools.GetPlayerPrefBool("Vore");
        Scat = UgreTools.GetPlayerPrefBool("Scat");
    }

    #endregion Save&Load

    #region Unit bools

    private static bool imperial = false;
    private const string impSaveName = "Imperial";
    private static bool inch = false;
    private const string inchSaveName = "Inch";
    private static bool pound = false;
    private const string poundSaveName = "Pound";
    private static bool gallon = false;
    private const string gallonSaveName = "Gallon";

    public static bool Imperial
    {
        get => imperial;
        set
        {
            imperial = value;
            Inch = value;
            Pound = value;
            Gallon = value;
        }
    }

    public static bool Inch
    {
        get => inch;
        private set
        {
            inch = value;
            if (!value)
            {
                imperial = false;
            }
            else if (Pound && Gallon)
            {
                imperial = true;
            }
        }
    }

    public static bool Pound
    {
        get => pound;
        private set
        {
            pound = value;
            if (!value)
            {
                imperial = false;
            }
            else if (Inch && Gallon)
            {
                imperial = true;
            }
        }
    }

    public static bool Gallon
    {
        get => gallon;
        private set
        {
            gallon = value;
            if (!value)
            {
                imperial = false;
            }
            else if (Inch && Pound)
            {
                imperial = true;
            }
        }
    }

    public static bool ToogleImperial() => Imperial = !Imperial;

    public static bool ToogleInch() => Inch = !Inch;

    public static bool TooglePound() => Pound = !Pound;

    public static bool ToogleGallon() => Gallon = !Gallon;

    #endregion Unit bools

    public static bool Vore => PlayerMain.GetPlayer.Vore.Active;
    public static bool Scat { get; private set; } = false;

    public static bool ToogleVore() => PlayerMain.GetPlayer.Vore.ToogleVore;

    public static bool ToogleScat() => Scat = !Scat;

    #region UnitConvetors

    public static string LorGal(float L)
    {
        if (L == 0) { return "empty"; }
        if (Gallon)
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
        if (Inch)
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
        if (kg <= 0) { return "0"; }
        if (Pound)
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

    public static float KgorPWithOutSuffix(float kg)
    {
        if (Pound)
        {
            float pound = Mathf.Round(kg * 2.20462262f);
            return pound;
        }
        else
        {
            return Mathf.FloorToInt(kg);
        }
    }

    #endregion UnitConvetors

    public static string Male = "male";

    public static string Female = "female";
    public static string Herm = "herm";
    public static string Cuntboy = "cuntboy";
    public static string Dickgirl = "dickgirl";
    public static string Doll = "doll";

    public static string GetGender(BasicChar who, bool capital = false)
    {
        switch (who.Gender())
        {
            case Genders.Herm: return CapOrLower(Herm);
            case Genders.Male: return CapOrLower(Male);
            case Genders.Female: return CapOrLower(Female);
            case Genders.Dickgirl: return CapOrLower(Dickgirl);
            case Genders.Cuntboy: return CapOrLower(Cuntboy);
            case Genders.Doll:
            default: return CapOrLower(Doll);
        }
        string CapOrLower(string gender) => capital ? char.ToUpper(gender[0]) + gender.Substring(1) : gender.ToLower();
    }

    public const float DoubleClickTime = 2f;
    private static float eventLogFontSize = 14f;
    private const string eventLogFontSizeSaveName = "EventlogFontSize";
    private static float combatLogFontSize = 14f;
    private const string combatLogFontSizeSaveName = "CombatlogFontSize";
    private static float sexlogFontSize = 14f;
    private const string sexlogFontSizeSaveName = "SexlogFontSize";

    public static float EventLogFontSize { get => eventLogFontSize; private set => eventLogFontSize = Mathf.Max(value, 0.5f); }

    public static float EventLogFontSizeDown => EventLogFontSize -= 0.5f;
    public static float EventLogFontSizeUp => EventLogFontSize += 0.5f;
    public static float CombatLogFontSize { get => combatLogFontSize; private set => combatLogFontSize = Mathf.Max(value, 0.5f); }
    public static float CombatLogFontSizeDown => CombatLogFontSize -= 0.5f;
    public static float CombatLogFontSizeUp => CombatLogFontSize += 0.5f;
    public static float SexlogFontSize { get => sexlogFontSize; private set => sexlogFontSize = Mathf.Max(value, 0.5f); }
    public static float SexlogFontSizeDown => SexlogFontSize -= 0.5f;
    public static float SexlogFontSizeUp => SexlogFontSize += 0.5f;
}

public enum ChooseEssence
{
    Masc,
    Femi,
    Both,
    None,
}

public static class VoreSettings
{
    public static ChooseEssence DrainEss { get; private set; } = ChooseEssence.Both;

    public static ChooseEssence ToggleDrainEss()
    {
        switch (DrainEss)
        {
            case ChooseEssence.Masc:
                DrainEss = ChooseEssence.Femi;
                break;

            case ChooseEssence.Femi:
                DrainEss = ChooseEssence.Both;
                break;

            case ChooseEssence.Both:
                DrainEss = ChooseEssence.None;
                break;

            case ChooseEssence.None:
                DrainEss = ChooseEssence.Masc;
                break;

            default:
                DrainEss = ChooseEssence.Both;
                break;
        }
        return DrainEss;
    }
}