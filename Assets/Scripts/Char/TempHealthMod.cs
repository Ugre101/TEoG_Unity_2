using UnityEngine;

[System.Serializable]
public class TempHealthMod : HealthMod
{
    [field: SerializeField] public int Duration { get; private set; }

    public TempHealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource, int parDuration)
        : base(parVal, parModType, parSource, parHealthType)
    {
        Duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    private void TickDown() => Duration--;

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}