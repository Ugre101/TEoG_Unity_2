using UnityEngine;

public static class PlayerMain
{
    public static BasicChar Player { get; private set; } = new BasicChar();

    public static void PlayerInit(string first, string last)
    {
        Player.Identity.SetFirstName(first);
        Player.Identity.SetLastName(last);
    }

    private static void SubscribleToEvents()
    {
        Player.RaceSystem.RaceChange += Player.Events.SoloEvents.RaceChange;
    }

    public static string PlayerID => Player.Identity.Id;

    public static PlayerSave Save() => new PlayerSave(Player);

    public static void Load(PlayerSave save) => JsonUtility.FromJsonOverwrite(save.Who, Player);

    public static void GenderChange()
    {
        if (Player.DidGenderChange())
        {
            // SpriteHandler.ChangeSprite();
        }
    }

    public static void DoEveryMin(int times)
    {
        // Do this in a central timemanger instead of indvidualy so that sleeping speeds up digesion & pregnancy etc.
        //   BasicChar.RefreshOrgans();
        Player.DoEveryMin(times);
    }

    public static void DoEveryHour()
    {
    }

    public static void DoEveryDay()
    {
        Player.DoEveryDay();
    }

    public static void BeforeDestroy()
    {
    }

    public static void Bind()
    {
        DateSystem.NewMinuteEvent += DoEveryMin;
        DateSystem.NewDayEvent += DoEveryDay;
        Player.SexualOrgans.AllOrgans.ForEach(so => so.Change += GenderChange);
        // SpriteHandler.Setup(BasicChar);
    }

    public static void Unbind()
    {
        DateSystem.NewMinuteEvent -= DoEveryMin;
        DateSystem.NewDayEvent -= DoEveryDay;
        Player.SexualOrgans.AllOrgans.ForEach(so => so.Change -= GenderChange);
    }
}