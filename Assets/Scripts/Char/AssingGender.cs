using System;

namespace EnemyCreatorStuff
{
    public static class AssingGender
    {
        private static readonly Genders[] allGenders = (Genders[])Enum.GetValues(typeof(Genders));
        /// <summary>
        /// Assing a random gender 
        /// </summary>
        /// <param name="amount">Total amount of essence to assing</param>
        public static void GetEssense(this BasicChar who, float amount)
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
                    // TODO add stable essence so I can give them essence without is transforming them
                    break;
            }
        }

        public static void GetEssense(this BasicChar who, float amount, Genders genderLock)
        {
            Essence Masc = who.Essence.Masc, Femi = who.Essence.Femi;
            switch (genderLock)
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
        /// <summary> </summary>
        /// <param name="favourGenderType"></param>
        public static void GetEssense(this BasicChar who, float amount, GenderTypes favourGenderType)
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
}