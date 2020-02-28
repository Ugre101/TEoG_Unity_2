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

    // Static functions to speed up creation of new mods. (DRY)
    public static TempHealthMod CreateFlatHealth(float val, string source, int dur)
        => new TempHealthMod(val, ModTypes.Flat, HealthTypes.Health, source, dur);

    public static TempHealthMod CreatePrecentHealth(float val, string source, int dur)
        => new TempHealthMod(val, ModTypes.Precent, HealthTypes.Health, source, dur);

    public static TempHealthMod CreateFlatWill(float val, string source, int dur)
        => new TempHealthMod(val, ModTypes.Flat, HealthTypes.WillPower, source, dur);

    public static TempHealthMod CreatePrecentWill(float val, string source, int dur)
        => new TempHealthMod(val, ModTypes.Precent, HealthTypes.WillPower, source, dur);
}