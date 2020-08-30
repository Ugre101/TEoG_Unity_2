using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region Save&Load

    public static void Save()
    {
        // Metrics & Units
        // Gender names
        GenderNames.Save();
        // Fontsizes
        new List<LogFontSize>() { EventLogFont, CombatLogFont, SexLogFont }.ForEach(lf => lf.Save());
        // Misc
        // UgreTools.SetPlayerPrefBool("Vore", Vore);
        UgreTools.SetPlayerPrefBool("Scat", Scat);
        FollowersSettings.Save();
    }

    public static void Load()
    {
        // Units
        // Genders
        GenderNames.Load();
        // FontSizes
        new List<LogFontSize>() { EventLogFont, CombatLogFont, SexLogFont }.ForEach(lf => lf.Load());
        // Misc
        //  Vore = UgreTools.GetPlayerPrefBool("Vore");
        Scat = UgreTools.GetPlayerPrefBool("Scat");
        FollowersSettings.Load();
    }

    #endregion Save&Load

    public static bool DefaultSpriteIsASquare { get; set; } = true;
    public static bool Vore => PlayerMain.Player.Vore.Active;
    public static bool Scat { get; private set; } = false;

    public static bool ToogleVore() => PlayerMain.Player.Vore.ToogleVore;

    public static bool ToogleScat() => Scat = !Scat;

    public static class GenderNames
    {
        public static string Male = "male";

        public static string Female = "female";
        public static string Herm = "herm";
        public static string Cuntboy = "cuntboy";
        public static string Dickgirl = "dickgirl";
        public static string Doll = "doll";

        public static void Load()
        {
            Male = PlayerPrefs.GetString("Male", Male);
            Female = PlayerPrefs.GetString("Female", Female);
            Herm = PlayerPrefs.GetString("Herm", Herm);
            Cuntboy = PlayerPrefs.GetString("Cuntboy", Cuntboy);
            Dickgirl = PlayerPrefs.GetString("Dickgirl", Dickgirl);
            Doll = PlayerPrefs.GetString("Doll", Doll);
        }

        public static void Save()
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

    public static class FollowersSettings
    {
        public static TitleName LeaderTitle = new TitleName("Leader");
        public static TitleName FollowerTitle = new TitleName("Follower");
        public static TitleName TakeHomeBtnTitle = new TitleName("Bring home");
        public static TitleName DormTitle = new TitleName("Dorm");
        private static List<TitleName> titleNames;

        public static List<TitleName> TitleNames
        {
            get
            {
                if (titleNames == null)
                {
                    titleNames = new List<TitleName>() { LeaderTitle, FollowerTitle, TakeHomeBtnTitle, DormTitle };
                }
                return titleNames;
            }
        }

        public static void Save() => TitleNames.ForEach(tn => tn.Save());

        public static void Load() => TitleNames.ForEach(tn => tn.Load());
    }

    public class TitleName
    {
        public string Title { get; private set; }

        public void SetTitle(string newTitle)
        {
            Title = newTitle;
            Save();
        }

        private readonly string SaveName;

        public TitleName(string title)
        {
            Title = title;
            SaveName = title;
        }

        public void Save() => PlayerPrefs.SetString(SaveName, Title);

        public void Load() => Title = PlayerPrefs.GetString(SaveName, Title);
    }
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

    public static ChooseEssence ToggleDrainEss() => DrainEss = DrainEss.CycleThoughEnum();
}