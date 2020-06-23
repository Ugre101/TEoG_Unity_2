public enum Genders
{
    Doll,
    Male,
    Cuntboy,
    Dickgirl,
    Herm,
    Female
}

public enum GenderTypes
{
    Feminine,
    Masculine,
    Neutral
}

public static class GenderExtensions
{
    public static Genders Gender(this BasicChar parWho)
    {
        if (parWho.SexualOrgans.HaveDick() && parWho.SexualOrgans.HaveVagina())
        {
            return Genders.Herm;
        }
        else if (parWho.SexualOrgans.HaveDick() && parWho.SexualOrgans.HaveBoobs(3))
        {
            return Genders.Dickgirl;
        }
        else if (parWho.SexualOrgans.HaveDick())
        {
            return Genders.Male;
        }
        else if (parWho.SexualOrgans.HaveVagina() && parWho.SexualOrgans.HaveBoobs())
        {
            return Genders.Female;
        }
        else if (parWho.SexualOrgans.HaveVagina())
        {
            return Genders.Cuntboy;
        }
        else
        {
            return Genders.Doll;
        }
    }

    public static string GenderToString(Genders genders)
    {
        switch (genders)
        {
            case Genders.Doll: return Settings.Doll;
            case Genders.Male: return Settings.Male;
            case Genders.Cuntboy: return Settings.Cuntboy;
            case Genders.Dickgirl: return Settings.Dickgirl;
            case Genders.Herm: return Settings.Herm;
            case Genders.Female: return Settings.Female;
            default: return Settings.Doll;
        }
    }

    public static GenderTypes GenderType(this BasicChar parWho)
    {
        switch (Gender(parWho))
        {
            case Genders.Cuntboy:
            case Genders.Male:
                return GenderTypes.Masculine;

            case Genders.Dickgirl:
            case Genders.Female:
            case Genders.Herm:
                return GenderTypes.Feminine;

            case Genders.Doll:
            default:
                return GenderTypes.Neutral;
        }
    }

    public static string HisHer(this BasicChar basicChar, bool capital = false)
    {
        switch (Gender(basicChar))
        {
            case Genders.Male:
            case Genders.Cuntboy:
                return capital ? "His" : "his";

            case Genders.Female:
            case Genders.Herm:
            case Genders.Dickgirl:
                return capital ? "Her" : "her";

            case Genders.Doll:
            default:
                return capital ? "Theirs" : "theirs";
        }
    }

    public static string HimHer(this BasicChar basicChar, bool capital = false)
    {
        switch (Gender(basicChar))
        {
            case Genders.Male:
            case Genders.Cuntboy:
                return capital ? "Him" : "him";

            case Genders.Female:
            case Genders.Herm:
            case Genders.Dickgirl:
                return capital ? "Her" : "her";

            case Genders.Doll:
            default:
                return capital ? "Them" : "them";
        }
    }
    public static string HimHerSelf(this BasicChar basicChar, bool capital = false)
    {
        switch (Gender(basicChar))
        {
            case Genders.Male:
            case Genders.Cuntboy:
                return capital ? "Himself" : "himself";

            case Genders.Female:
            case Genders.Herm:
            case Genders.Dickgirl:
                return capital ? "Herself" : "herself";

            case Genders.Doll:
            default:
                return capital ? "Themself" : "themself";
        }
    }
    public static string HeShe(this BasicChar basicChar, bool capital = false)
    {
        switch (Gender(basicChar))
        {
            case Genders.Male:
            case Genders.Cuntboy:
                return capital ? "He" : "he";

            case Genders.Female:
            case Genders.Herm:
            case Genders.Dickgirl:
                return capital ? "She" : "she";

            case Genders.Doll:
            default:
                return capital ? "They" : "they";
        }
    }

}