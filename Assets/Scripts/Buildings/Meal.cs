using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Meal
{
    [SerializeField] private float hpGain;
    [SerializeField] private float wpGain;
    [SerializeField] private float fatGain;

    public Meal(float wp, float hp, float fat)
    {
        hpGain = hp;
        wpGain = wp;
        fatGain = fat;
    }

    public Meal(float hpwp, float fat) : this(hpwp, hpwp, fat)
    {
    }

    public Meal(float all) : this(all, all, all)
    {
    }

    public float HpGain => hpGain;
    public float WpGain => wpGain;
    public float FatGain => fatGain;
}

[System.Serializable]
public class MealWithBuffs : Meal
{
    [SerializeField] private List<TempStatMod> tempMods = new List<TempStatMod>();
    [SerializeField] private List<TempHealthMod> tempHealthMods = new List<TempHealthMod>();

    public MealWithBuffs(float all, List<TempStatMod> mods) : base(all)
    {
        tempMods = mods;
    }

    public MealWithBuffs(float all, List<TempHealthMod> mods) : base(all)
    {
        tempHealthMods = mods;
    }

    public MealWithBuffs(float all, List<TempStatMod> statMods, List<TempHealthMod> healthMods) : base(all)
    {
        tempMods = statMods;
        tempHealthMods = healthMods;
    }

    public MealWithBuffs(float hpwp, float fat, List<TempStatMod> mods) : base(hpwp, fat)
    {
        tempMods = mods;
    }

    public MealWithBuffs(float wp, float hp, float fat, List<TempStatMod> mods) : base(wp, hp, fat)
    {
        tempMods = mods;
    }

    public List<TempStatMod> TempMods => tempMods;
    public List<TempHealthMod> TempHealthMods => tempHealthMods;
}