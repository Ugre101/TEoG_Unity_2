public enum Genders
{
    Male,
    Female,
    Herm,
    Dickgirl,
    Cuntboy,
    Doll
}

public enum GenderTypes
{
    Feminine,
    Masculine
}

public static class GenderExtensions
{
    public static Genders Gender(this BasicChar parWho)
    {
        if (parWho.SexualOrgans.Dicks.Count > 0 && parWho.SexualOrgans.Vaginas.Count > 0)
        {
            return Genders.Herm;
        }
        else if (parWho.SexualOrgans.Dicks.Count > 0 && parWho.SexualOrgans.Boobs.Total() > 2)
        {
            return Genders.Dickgirl;
        }
        else if (parWho.SexualOrgans.Dicks.Count > 0)
        {
            return Genders.Male;
        }
        else if (parWho.SexualOrgans.Vaginas.Count > 0 && parWho.SexualOrgans.Boobs.Total() > 2)
        {
            return Genders.Female;
        }
        else if (parWho.SexualOrgans.Vaginas.Count > 0)
        {
            return Genders.Cuntboy;
        }
        else
        {
            return Genders.Doll;
        }
    }

    public static GenderTypes GenderType(this BasicChar parWho)
    {
        switch (parWho.Gender())
        {
            case Genders.Cuntboy:
            case Genders.Doll:
            case Genders.Male:
                return GenderTypes.Masculine;

            case Genders.Dickgirl:
            case Genders.Female:
            case Genders.Herm:
            default:
                return GenderTypes.Feminine;
        }
    }
}