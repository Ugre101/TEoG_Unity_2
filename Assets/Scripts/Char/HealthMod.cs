using UnityEngine;

[System.Serializable]
public class HealthMod : Mod
{
    [SerializeField] private HealthTypes healthType;

    public HealthTypes HealthType => healthType;

    public HealthMod(float parVal, ModTypes parModType, string parSource, HealthTypes parHealthType) : base(parVal, parModType, parSource)
    {
        this.healthType = parHealthType;
    }
}