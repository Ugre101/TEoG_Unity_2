using UnityEngine;

[System.Serializable]
public class TempStatMod : StatMod
{
    [field: SerializeField] public int Duration { get; private set; }

    public TempStatMod(float parValue, StatTypes parStatTypes, ModTypes parType, string parSource, int parHours) :
        base(parValue, parStatTypes, parSource, parType)
    {
        Duration = parHours;
        DateSystem.NewHourEvent += TickDown;
    }

    private void TickDown()
    {
        Duration--;
    }

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}