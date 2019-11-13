using System;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Settings", menuName = "Tools")]
public class Settings : ScriptableObject//Bad name?
{
    [SerializeField]
    private bool Imperial = false;

    public bool ToogleImp()
    {
        return Imperial = !Imperial;
    }

    private void OnEnable()
    {
        Imperial = PlayerPrefs.HasKey("Imperial") ? PlayerPrefs.GetInt("Imperial") == 1 ? true : false : false;
    }

    public string LorGal(float L)
    {
        if (L == 0)
        {
            return "empty";
        }
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

    public string MorInch(float cm)
    {
        if (cm == 0)
        {
            return "zero?";
        }
        if (Imperial)
        {
            float Inch = Mathf.Round(cm / 2.54f);
            float Feet = Mathf.Floor(Inch / 12);
            float Yard = Mathf.Floor(Feet / 3);
            return Yard > 0 ? $"{Yard}yard" : Feet > 0 ? $"{Feet}feet" : Inch > 0 ? $"{Inch}inches" : $"less than one inch";
        }
        else
        {
            float m = (float)Math.Round(cm / 100,1);
            return m > 5f ? $"{m}m" : $"{Mathf.Round(cm)}cm";
        }
    }

    public string KgorP(float kg)
    {
        if (kg <= 0)
        {
            return "zero?";
        }
        string toReturn = "";
        if (Imperial)
        {
            // int stone = Mathf.FloorToInt(kg * 0.15747304441777f); // when to use stone?
            float pound = Mathf.Round(kg * 2.20462262f);
            float ounce = Mathf.Round(kg * 35.27396194958f);
            toReturn += pound < 1 ? $"{ounce}oz" : $"{pound}lbs";
        }
        else
        {
            if (kg > 1000)
            {
                int tonne = Mathf.FloorToInt(kg / 1000);
                toReturn += tonne + "tonne";
                int left = Mathf.FloorToInt(kg - (tonne * 1000));
                if (left > 99)
                {
                    toReturn += $" and {left}kg";
                }
            }
            else if (kg > 1)
            {
                toReturn += Mathf.FloorToInt(kg) + "kg";
            }
            else
            {
                toReturn += Mathf.Ceil(kg * 1000) + "g";
            }
        }
        return toReturn;
    }

    [SerializeField]
    [Header("Pronuns")]
    public string Male = "male";

    public string Female = "female";
    public string Herm = "herm";
    public string Cuntboy = "cuntboy";
    public string Dickgirl = "dickgirl";
    public string Doll = "doll";

    public string GetGender(BasicChar who, bool capital = false)
    {
        switch (who.Gender)
        {
            case Genders.Herm:
                return capital ? char.ToUpper(Herm[0]) + Herm.Substring(1) : Herm.ToLower();

            case Genders.Male:
                return capital ? char.ToUpper(Male[0]) + Male.Substring(1) : Male.ToLower();

            case Genders.Female:
                return capital ? char.ToUpper(Female[0]) + Female.Substring(1) : Female.ToLower();

            case Genders.Dickgirl:
                return capital ? char.ToUpper(Dickgirl[0]) + Dickgirl.Substring(1) : Dickgirl.ToLower();

            case Genders.Cuntboy:
                return capital ? char.ToUpper(Cuntboy[0]) + Cuntboy.Substring(1) : Cuntboy.ToLower();

            case Genders.Doll:
            default:
                return capital ? char.ToUpper(Doll[0]) + Doll.Substring(1) : Doll.ToLower();
        }
    }
}