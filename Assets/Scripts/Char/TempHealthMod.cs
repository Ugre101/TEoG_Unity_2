using UnityEngine;

[System.Serializable]
public class TempHealthMod : HealthMod, IDuration
{
    [SerializeField] private int duration;
    public int Duration => duration;

    public TempHealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource, int parDuration)
        : base(parVal, parModType, parSource, parHealthType)
    {
        duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    public void TickDown() => duration--;

    public void IncreaseDuration(int toIncrease) => duration += toIncrease;
}