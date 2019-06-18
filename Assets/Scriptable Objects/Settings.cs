using System.Collections.Generic;
using UnityEngine;

public enum Genders
{
    Male,
    Female,
    Herm
}

[System.Serializable]
[CreateAssetMenu(fileName = "Settings", menuName = "Tools")]
public class Settings : ScriptableObject//Bad name?
{
    [SerializeField]
    private bool Imperial = false;
    private string Male = "male";
    public bool ToogleImp()
    {
        return Imperial = Imperial ? false : true;
    }

    private void OnEnable()
    {
        Imperial = PlayerPrefs.HasKey("Imperial") ? PlayerPrefs.GetInt("Imperial") == 1 ? true : false : false;
    }

    public string LorGal(float L)
    {
        if (Imperial)
        {
            return Mathf.Round(0.264172052f * L) < 1 ?
                $"{Mathf.Round(L * 4.22675284f)}cups" : $"{Mathf.Round(L * 0.264172052f)}gallon";
        }
        else
        {
            return L < 0.1f ? $"{Mathf.Round(L * 100)}cl" : L < 0.99 ? $"{Mathf.Round(L * 10)}dl" : $"{Mathf.Round(L)}L";
        }
    }

    public string MorInch(float cm)
    {
        if (Imperial)
        {
            float Inch = Mathf.Round(cm / 2.54f);
            float Feet = Mathf.Floor(Inch / 12);
            float Yard = Mathf.Floor(Feet / 3);
            return Yard > 0 ? $"{Yard}yard" : Feet > 0 ? $"{Feet}feet" : Inch > 0 ? $"{Inch}inches" : $"";
        }
        else
        {
            float m = Mathf.Floor(cm / 100);
            return m > 5f ? $"{m}m" : $"{cm}cm";
        }
    }

    public string KgorP(float kg)
    {
        string toReturn = "";
        if (Imperial)
        {
            // int stone = Mathf.FloorToInt(kg * 0.15747304441777f); // when to use stone?
            float pound = kg * 2.20462262f;
            float ounce = kg * 35.27396194958f;
            toReturn += pound < 1 ? $"{ounce}ounce" : $"{pound}pound";
        }
        else
        {
            if (kg > 1000)
            {
                int tonne = Mathf.FloorToInt(kg / 1000);
                toReturn += tonne + "tonne";
                int left = Mathf.FloorToInt(kg - tonne * 1000);
                if (left > 99)
                {
                    toReturn += $" and {left}kg";
                }
            }
            else if (kg > 1)
            {
                toReturn += Mathf.FloorToInt(kg) + "kg";
            }else
            {
                toReturn += Mathf.Ceil(kg * 1000) + "g";
            }
        }
        return toReturn;
    }
 

    public Genders CheckGender(BasicChar who)
    {
        if (who.Dicks.Count > 0 && who.Vaginas.Count > 0)
        {
            return Genders.Herm;
        }
        else if (who.Dicks.Count > 0)
        {
            return Genders.Male;
        }
        else
        {
            return Genders.Female;
        }
    }

    public string GetGender(BasicChar who, bool capital = false)
    {
        if (who.Dicks.Count > 0 && who.Vaginas.Count > 0)
        {
            return capital ? "Herm" : "herm";
        }
        else if (who.Dicks.Count > 0)
        {
            return capital ? char.ToUpper(Male[0]) + Male.Substring(1) : Male.ToLower();
        }
        else
        {
            return "female";
        }
    }
}