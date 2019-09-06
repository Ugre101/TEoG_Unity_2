using UnityEngine;

public class FlagInt
{
    private int value = 0;
    public int Value { get { return value; } }

    public void Increase(int toIncrease = 0)
    {
        value += Mathf.Min(0, toIncrease);
    }
}

public class Flags
{
    public FlagInt Pregnant;
    public FlagInt ImPregnated;
}

[System.Serializable]
public class PlayerFlags
{
    public bool BeatenBanditLord = false;
}