using UnityEngine;

[System.Serializable]
public class TempHealthMod : HealthMod, IDuration
{
    [field: SerializeField] public int Duration { get; private set; }

    public TempHealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource, int parDuration)
        : base(parVal, parModType, parSource, parHealthType)
    {
        Duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    public void TickDown() => Duration--;

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}