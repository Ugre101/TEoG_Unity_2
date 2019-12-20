using UnityEngine;

[System.Serializable]
public class HealthMod
{
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public ModTypes ModType { get; private set; }
    [field: SerializeField] public HealthTypes HealthType { get; private set; }
    [field: SerializeField] public string Source { get; private set; }

    public HealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource)
    {
        Value = parVal;
        ModType = parModType;
        HealthType = parHealthType;
        Source = parSource;
    }
}
