using UnityEngine;

[System.Serializable]
public class TempStatMod : StatMod, IDuration
{
    [SerializeField] private int duration;
    public int Duration => duration;

    public TempStatMod(float parValue, ModTypes parType, string parSource, int parHours) : base(parValue, parSource, parType)
    {
        duration = parHours;
        DateSystem.NewHourEvent += TickDown;
    }

    public void TickDown() => duration--;

    public void IncreaseDuration(int toIncrease) => duration += toIncrease;
}

[System.Serializable]
public class AssingTempStatMod
{
    [SerializeField] private TempStatMod tempStatMod;
    [SerializeField] private StatTypes statTypes;

    public AssingTempStatMod(TempStatMod tempStatMod, StatTypes statTypes)
    {
        this.tempStatMod = tempStatMod;
        this.statTypes = statTypes;
    }

    public TempStatMod TempStatMod => tempStatMod;
    public StatTypes StatTypes => statTypes;
}