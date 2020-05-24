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

public class KnowTeleport
{
    private bool know = false;
}

public static class PlayerFlags
{
    public static FlagBool BeatBanditLord { get; private set; } = new FlagBool();
    public static KnowMap BanditMap { get; private set; } = new KnowMap();

    public static PlayerFlagsSave Save() => new PlayerFlagsSave(BeatBanditLord.Cleared, BanditMap.Know);

    public static void Load(PlayerFlagsSave playerFlagsSave)
    {
        BeatBanditLord.Load(playerFlagsSave.BeatBanditLord);
        BanditMap.Know = playerFlagsSave.BanditMap;
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