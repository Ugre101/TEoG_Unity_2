using System;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region Save&Load

    public static void Save()
    {
        // Metrics & Units
        new List<MeasureUnit>() { Inch, Pound, Gallon }.ForEach(mu => mu.Save());
        // Gender names
        GenderNames.Load();
        // Fontsizes
        new List<LogFontSize>() { EventLogFont, CombatLogFont, SexLogFont }.ForEach(lf => lf.Save());
        // Misc
        // UgreTools.SetPlayerPrefBool("Vore", Vore);
        UgreTools.SetPlayerPrefBool("Scat", Scat);
    }

    public static void Load()
    {
        // Units
        new List<MeasureUnit>() { Inch, Pound, Gallon }.ForEach(mu => mu.Load());
        // Genders
        GenderNames.Save();
        // FontSizes
        new List<LogFontSize>() { EventLogFont, CombatLogFont, SexLogFont }.ForEach(lf => lf.Load());
        // Misc
        //  Vore = UgreTools.GetPlayerPrefBool("Vore");
        Scat = UgreTools.GetPlayerPrefBool("Scat");
    }

    #endregion Save&Load

    #region Unit bools

    public static MeasureUnit Inch { get; } = new MeasureUnit("Inch");
    public static MeasureUnit Pound { get; } = new MeasureUnit("Pound");
    public static MeasureUnit Gallon { get; } = new MeasureUnit("Gallon");

    public static bool Imperial
    {
        get => Inch.Imperial && Pound.Imperial && Gallon.Imperial;
        set
        {
            Inch.Imperial = value;
            Pound.Imperial = value;
            Gallon.Imperial = value;
        }
    }

    public static bool ToogleImperial() => Imperial = !Imperial;

    #endregion Unit bools

    public static bool DefaultSpriteIsASquare { get; set; } = true;
    public static bool Vore => PlayerHolder.Player.Vore.Active;
    public static bool Scat { get; private set; } = false;

    public static bool ToogleVore() => PlayerHolder.Player.Vore.ToogleVore;

    public static bool ToogleScat() => Scat = !Scat;

    #region UnitConvetors

    public static string LorGal(float L)
    {
        if (L == 0)
            return "empty";
        if (Gallon.Imperial)
        {
            float LtoG = 0.264172052f;
            return Mathf.Floor(LtoG * L) < 1 ? $"{Mathf.Round(L * 4.22675284f)}cups" : $"{Mathf.Round(L * LtoG)}gallon";
        }
        else
        {
            if (L < 0.1f)
                return $"{Mathf.Round(L * 100)}cl";
            return L < 0.99 ? $"{Mathf.Round(L * 10)}dl" : $"{(float)Math.Round(L, 1)}L";
        }
    }

    public static string MorInch(float cm)
    {
        if (cm == 0) { return "zero?"; }
        if (Inch.Imperial)
        {
            float Inch = Mathf.Round(cm / 2.54f);
            float Feet = Mathf.Floor(Inch / 12);
            float Yard = Mathf.Floor(Feet / 3);
            if (Yard > 0)
                return $"{Yard}yard";
            else if (Feet > 0)
                return $"{Feet}feet";
            else
                return Inch > 0 ? $"{Inch}inches" : $"less than one inch";
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
        if (Pound.Imperial)
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
                    toReturn += $" and {left}kg";
                return toReturn;
            }
            else if (kg > 1)
                return Mathf.FloorToInt(kg) + "kg";
            else
                return Mathf.Ceil(kg * 1000) + "g";
        }
    }

    public static float KgorPWithOutSuffix(float kg) => Pound.Imperial ? Mathf.Round(kg * 2.20462262f) : Mathf.FloorToInt(kg);

    #endregion UnitConvetors

    public static class GenderNames
    {
        public static string Male = "male";

        public static string Female = "female";
        public static string Herm = "herm";
        public static string Cuntboy = "cuntboy";
        public static string Dickgirl = "dickgirl";
        public static string Doll = "doll";

        public static void Save()
        {
            Male = PlayerPrefs.GetString("Male", Male);
            Female = PlayerPrefs.GetString("Female", Female);
            Herm = PlayerPrefs.GetString("Herm", Herm);
            Cuntboy = PlayerPrefs.GetString("Cuntboy", Cuntboy);
            Dickgirl = PlayerPrefs.GetString("Dickgirl", Dickgirl);
            Doll = PlayerPrefs.GetString("Doll", Doll);
        }

        internal static void Load()
        {
            PlayerPrefs.SetString("Male", Male);
            PlayerPrefs.SetString("Female", Female);
            PlayerPrefs.SetString("Herm", Herm);
            PlayerPrefs.SetString("Cuntboy", Cuntboy);
            PlayerPrefs.SetString("Dickgirl", Dickgirl);
            PlayerPrefs.SetString("Doll", Doll);
        }
    }

    public static string GetGender(this BasicChar who, bool capital = false)
    {
        switch (GenderExtensions.Gender(who))
        {
            case Genders.Herm: return CapOrLower(GenderNames.Herm);
            case Genders.Male: return CapOrLower(GenderNames.Male);
            case Genders.Female: return CapOrLower(GenderNames.Female);
            case Genders.Dickgirl: return CapOrLower(GenderNames.Dickgirl);
            case Genders.Cuntboy: return CapOrLower(GenderNames.Cuntboy);
            case Genders.Doll:
            default: return CapOrLower(GenderNames.Doll);
        }
        string CapOrLower(string gender) => capital ? char.ToUpper(gender[0]) + gender.Substring(1) : gender.ToLower();
    }

    public const float DoubleClickTime = 1.2f;

    public static LogFontSize EventLogFont { get; } = new LogFontSize("EventlogFontSize");
    public static LogFontSize CombatLogFont { get; } = new LogFontSize("CombatlogFontSize");
    public static LogFontSize SexLogFont { get; } = new LogFontSize("SexlogFontSize");
}

public class MeasureUnit
{
    private readonly string saveName;

    public MeasureUnit(string saveName) => this.saveName = saveName;

    public bool Imperial { get; set; } = true;
    public bool Toggle => Imperial = !Imperial;

    public void Save() => UgreTools.SetPlayerPrefBool(saveName, Imperial);

    public void Load() => Imperial = UgreTools.GetPlayerPrefBool(saveName);
}

public class LogFontSize
{
    private float size = 14;
    private readonly string saveName;

    public LogFontSize(float size, string saveName)
    {
        this.size = size;
        this.saveName = saveName;
    }

    public LogFontSize(string saveName) : this(14f, saveName)
    {
    }

    public float Size { get => size; private set => size = Mathf.Max(value, 0.5f); }
    public float Down => Size -= 0.5f;
    public float Up => Size += 0.5f;

    public void Save() => PlayerPrefs.SetFloat(saveName, Size);

    public void Load() => Size = PlayerPrefs.GetFloat(saveName, Size);
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