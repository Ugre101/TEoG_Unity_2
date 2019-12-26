using UnityEngine;

[System.Serializable]
public class HealthMod : Mod
{
    [field: SerializeField] public HealthTypes HealthType { get; private set; }

    public HealthMod(float parVal, ModTypes parModType, string parSource, HealthTypes parHealthType) : base(parVal, parModType, parSource)
    {
        HealthType = parHealthType;
    }
}