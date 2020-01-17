using UnityEngine;

[CreateAssetMenu(fileName = "FertilityBooster", menuName = "Item/FertilityBooster")]
public class FertilityBooster : Drinkable
{
    public FertilityBooster()
    {
        ItemId = ItemId.FertilityBooster;
        Title = "Fertility booster";
    }

    public override string Use(BasicChar user)
    {
        user.PregnancySystem.Fertility.BaseValue++;
        return base.Use(user);
    }
}

[CreateAssetMenu(fileName = "FertilityTempBooster", menuName = "Item/FertilityBooster")]
public class FertilityTempBooster : Drinkable
{
    public FertilityTempBooster()
    {
        ItemId = ItemId.FertilityTempBooster;
        Title = "Fertility week booster";
    }

    public override string Use(BasicChar user)
    {
        TempStatMod tempMod = new TempStatMod(10, ModTypes.Flat, typeof(FertilityTempBooster).Name, 168);
        user.PregnancySystem.Fertility.AddTempMod(tempMod);
        return base.Use(user);
    }
}