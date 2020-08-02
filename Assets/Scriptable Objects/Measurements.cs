using System;
using System.Collections.Generic;
using UnityEngine;

public static class Measurements
{
    public static void Save() => new List<MeasureUnit>() { Inch, Pound, Gallon }.ForEach(mu => mu.Save());

    public static void Load() => new List<MeasureUnit>() { Inch, Pound, Gallon }.ForEach(mu => mu.Load());

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

    #region UnitConvetors

    public static string LorGal(this float L)
    {
        if (L == 0) { return "empty"; }
        if (Gallon.Imperial)
        {
            const float LtoG = 0.264172052f;
            return Mathf.Floor(LtoG * L) < 1 ? $"{Mathf.Round(L * 4.22675284f)}cups" : $"{Mathf.Round(L * LtoG)}gallon";
        }
        else
        {
            if (L < 0.1f)
                return $"{Mathf.Round(L * 100)}cl";
            return L < 0.99 ? $"{Mathf.Round(L * 10)}dl" : $"{(float)Math.Round(L, 1)}L";
        }
    }

    public static string MorInch(this float cm)
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

    public static string KgorP(this float kg)
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

    public static float KgorPWithOutSuffix(this float kg) => Pound.Imperial ? Mathf.Round(kg * 2.20462262f) : Mathf.FloorToInt(kg);

    #endregion UnitConvetors
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