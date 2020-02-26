using UnityEngine;

[System.Serializable]
public abstract class Mod
{
    [SerializeField] private float value;
    [SerializeField] private ModTypes modType;
    [SerializeField] private string source;

    public Mod(float parValue, ModTypes parType, string parSource)
    {
        this.value = parValue;
        this.modType = parType;
        this.source = parSource;
    }

    public float Value => value;
    public ModTypes ModType => modType;
    public string Source => source;

    public void RenameSource(string newName) => source = newName;
}

/*
public abstract class TempMod : Mod
{
    public TempMod(float parValue, ModTypes parType, string parSource, int parDuration) : base(parValue, parType, parSource)
    {
        Duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    [field: SerializeField] public int Duration { get; protected set; }

    private void TickDown() => Duration--;

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}
*/