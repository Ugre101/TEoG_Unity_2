using System;

public static class AssingGender
{
    private static readonly Genders[] allGenders = (Genders[])Enum.GetValues(typeof(Genders));

    public static void GetEssense(BasicChar who, float amount)
    {
        Genders gender = allGenders[UnityEngine.Random.Range(0, allGenders.Length - 1)];
        Essence Masc = who.Essence.Masc, Femi = who.Essence.Femi;
        switch (gender)
        {
            case Genders.Male:
                Masc.Gain(amount);
                break;

            case Genders.Female:
                Femi.Gain(amount);
                break;

            case Genders.Herm:
                Masc.Gain(amount / 2);
                Femi.Gain(amount / 2);
                break;

            case Genders.Dickgirl:
                // who.Femi.Gain(amount / 2);
                who.SexualOrgans.Dicks.AddDick();
                who.SexualOrgans.Boobs.AddBoobs();
                break;

            case Genders.Cuntboy:
                who.SexualOrgans.Vaginas.AddVag();
                break;

            case Genders.Doll:
            default:
                break;
        }
    }
}