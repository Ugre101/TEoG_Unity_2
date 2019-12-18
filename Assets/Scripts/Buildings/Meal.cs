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
    // TODO add temp statmod
}