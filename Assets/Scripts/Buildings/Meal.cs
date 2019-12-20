using System.Collections.Generic;

public class Meal
{
    public Meal(float all)
    {
        HpGain = all;
        WpGain = all;
        FatGain = all;
    }

    public Meal(float hpwp, float fat)
    {
        HpGain = hpwp;
        WpGain = hpwp;
        FatGain = fat;
    }

    public Meal(float wp, float hp, float fat)
    {
        HpGain = hp;
        WpGain = wp;
        FatGain = fat;
    }

    public float HpGain { get; private set; }
    public float WpGain { get; private set; }
    public float FatGain { get; private set; }
}

public class MealWithBuffs : Meal
{
    public MealWithBuffs(float all, List<TempStatMod> mods) : base(all)
    {
        TempMods = mods;
    }
    public MealWithBuffs(float all, List<TempHealthMod> mods) : base(all)
    {
        TempHealthMods = mods;
    }
    public MealWithBuffs(float all, List<TempStatMod> statMods,List<TempHealthMod> healthMods) : base(all)
    {
        TempMods = statMods;
        TempHealthMods = healthMods;
    }
    public MealWithBuffs(float hpwp, float fat, List<TempStatMod> mods) : base(hpwp, fat)
    {
        TempMods = mods;
    }

    public MealWithBuffs(float wp, float hp, float fat, List<TempStatMod> mods) : base(wp, hp, fat)
    {
        TempMods = mods;
    }

    public List<TempStatMod> TempMods { get; private set; } = new List<TempStatMod>();
    public List<TempHealthMod> TempHealthMods { get; private set; } = new List<TempHealthMod>();
}