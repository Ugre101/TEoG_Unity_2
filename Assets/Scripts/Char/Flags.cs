using UnityEngine;

[System.Serializable]
public class FlagInt
{
    [SerializeField] private int value = 0;

    public int Value => value;

    public void Increase(int toIncrease = 0) => value += Mathf.Max(0, toIncrease);
}

[System.Serializable]
public class FlagBool
{
    [SerializeField] private bool cleared = false;
    public bool Cleared => cleared;

    /// <summary>Marks flag as completed</summary>
    public void Clear() => cleared = true;

    /// <summary>Marks flag as uncompleted</summary>
    public void UnClear() => cleared = false;

    public void Load(bool setTo) => cleared = setTo;
}

[System.Serializable]
public class Flags
{
}

public class KnowMap
{
    private bool know = false;

    public bool Know
    {
        get => know;
        set
        {
            know = value;
            KnowThisMap?.Invoke();
        }
    }

    public delegate void KnowThis();

    public event KnowThis KnowThisMap;
}

public static class PlayerFlags
{
    public static FlagBool BeatBanditLord { get; } = new FlagBool();
    public static KnowMap BanditMap { get; } = new KnowMap();

    // Count times beaten certain enemies
    public static FlagInt ElfsBeaten { get; } = new FlagInt();

    public static FlagInt FairiesBeaten { get; } = new FlagInt();
    public static FlagInt TimesBeatenBanditLord { get; } = new FlagInt(); // Maybe he just gives up

    public static PlayerFlagsSave Save() => new PlayerFlagsSave(BeatBanditLord.Cleared, BanditMap.Know);

    public static void Load(PlayerFlagsSave playerFlagsSave)
    {
        BeatBanditLord.Load(playerFlagsSave.BeatBanditLord);
        BanditMap.Know = playerFlagsSave.BanditMap;
    }

    public static void CountTimesBeatingEnemy(BasicChar enemy)
    {
        if (enemy.RaceSystem.CurrentRace() == Races.Elf)
        {
            ElfsBeaten.Increase();
        }
        else if (enemy.RaceSystem.CurrentRace() == Races.Fairy)
        {
            FairiesBeaten.Increase();
        }
    }
}

[System.Serializable]
public struct PlayerFlagsSave
{
    [SerializeField] private bool beatBanditLord;
    [SerializeField] private bool banditMap;

    public PlayerFlagsSave(bool beatBandit, bool knowBanditMap)
    {
        beatBanditLord = beatBandit;
        banditMap = knowBanditMap;
    }

    public bool BeatBanditLord => beatBanditLord;
    public bool BanditMap => banditMap;
}