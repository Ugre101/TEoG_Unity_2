using UnityEngine;

[System.Serializable]
public class FlagInt
{
    [SerializeField]
    private int value = 0;

    public int Value => value;

    public void Increase(int toIncrease = 0) => value += Mathf.Min(0, toIncrease);
}

[System.Serializable]
public class FlagBool
{
    [SerializeField]
    private bool cleared = false;

    public bool Cleared => cleared;

    /// <summary>Marks flag as completed</summary>
    public void Clear() => cleared = true;

    /// <summary>Marks flag as uncompleted</summary>
    public void UnClear() => cleared = false;
}

[System.Serializable]
public class Flags
{
    public FlagInt Pregnant;
    public FlagInt ImPregnated;
}

[System.Serializable]
public class PlayerFlags
{
    [SerializeField]
    private FlagBool beatenBanditLord = new FlagBool();

    public FlagBool BeatBanditLord => beatenBanditLord;
}