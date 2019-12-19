using System.Collections.Generic;

public class Meal
{
    public Meal(float all)
    {
        HpGain = all;
        WpGain = all;
        FatGain = all;
    }

    public Meal(float all, List<TempStatMod> mods)
    {
        HpGain = all;
        WpGain = all;
        FatGain = all;
        TempMods = mods;
    }

    public Meal(float hpwp, float fat)
    {
        HpGain = hpwp;
        WpGain = hpwp;
        FatGain = fat;
    }

    public Meal(float hpwp, float fat, List<TempStatMod> mods)
    {
        HpGain = hpwp;
        WpGain = hpwp;
        FatGain = fat;
        TempMods = mods;
    }

    public Meal(float wp, float hp, float fat)
    {
        HpGain = hp;
        WpGain = wp;
        FatGain = fat;
    }

    public Meal(float wp, float hp, float fat, List<TempStatMod> mods)
    {
        HpGain = hp;
        WpGain = wp;
        FatGain = fat;
        TempMods = mods;
    }

    public float HpGain { get; private set; }
    public float WpGain { get; private set; }
    public float FatGain { get; private set; }
    public List<TempStatMod> TempMods { get; private set; } = new List<TempStatMod>();
}