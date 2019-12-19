using UnityEngine;

[System.Serializable]
public class TempStatMod : StatMod
{
    [SerializeField]
    private int hourDuration;

    public int Duration => hourDuration;

    public TempStatMod(float parValue, StatTypes parStatTypes, StatsModType parType, string parSource, int parHours) :
        base(parValue, parStatTypes, parType, parSource)
    {
        hourDuration = parHours;
        DateSystem.NewHourEvent += TickDown;
    }

    private void TickDown()
    {
        hourDuration--;
    }

    public void IncreaseDuration(int toIncrease) => hourDuration += toIncrease;
}