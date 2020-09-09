using UnityEngine;

public static class PlayerMain
{
    public static BasicChar Player { get; private set; } = new BasicChar();

    public static void PlayerInit(string first, string last)
    {
        Player.Identity.SetFirstName(first);
        Player.Identity.SetLastName(last);
    }

    public static string PlayerId => Player.Identity.Id;

    public static PlayerSave Save() => new PlayerSave(Player);

    public static void Load(PlayerSave save)
    {
        Unbind();
        if (save.Who != null)
            JsonUtility.FromJsonOverwrite(save.Who, Player);
        else
            Debug.LogError("Player save was null");
        Bind();
    }

    private static void GenderChange()
    {
        if (Player.DidGenderChange())
        {
            // SpriteHandler.ChangeSprite();
        }
    }

    private static void DoEveryMin(int times)
    {
        // Do this in a central timemanger instead of indvidualy so that sleeping speeds up digesion & pregnancy etc.
        //   BasicChar.RefreshOrgans();
        Player.DoEveryMin(times);
    }

    private static void DoEveryHour()
    {
    }

    private static void DoEveryDay()
    {
        Player.DoEveryDay();
    }

    private static void BeforeDestroy()
    {
    }

    public static void Bind()
    {
        DateSystem.NewMinuteEvent += DoEveryMin;
        DateSystem.NewDayEvent += DoEveryDay;
        Player.SexualOrgans.AllOrgans.ForEach(so => so.Change += GenderChange);
        Player.RaceSystem.RaceChange += Player.Events.SoloEvents.RaceChange;

        // SpriteHandler.Setup(BasicChar);
    }

    private static void Unbind()
    {
        DateSystem.NewMinuteEvent -= DoEveryMin;
        DateSystem.NewDayEvent -= DoEveryDay;
        Player.SexualOrgans.AllOrgans.ForEach(so => so.Change -= GenderChange);
        Player.RaceSystem.RaceChange -= Player.Events.SoloEvents.RaceChange;
    }
}