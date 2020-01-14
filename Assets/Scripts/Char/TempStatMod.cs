using UnityEngine;

[System.Serializable]
public class TempStatMod : StatMod, IDuration
{
    [SerializeField] private int duration;
    public int Duration => duration;

    public TempStatMod(float parValue, StatTypes parStatTypes, ModTypes parType, string parSource, int parHours) :
        base(parValue, parStatTypes, parSource, parType)
    {
        duration = parHours;
        DateSystem.NewHourEvent += TickDown;
    }

    public void TickDown() => duration--;

    public void IncreaseDuration(int toIncrease) => duration += toIncrease;
}