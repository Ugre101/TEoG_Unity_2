using UnityEngine;

[CreateAssetMenu(fileName = "VirilityBooster", menuName = "Item/VirilityBooster")]
public class VirilityBooster : Drinkable
{
    public VirilityBooster()
    {
        ItemId = ItemId.VirilityBooster;
        Title = "Virility booster";
    }

    public override string Use(BasicChar user)
    {
        user.PregnancySystem.Virility.BaseValue++;
        return base.Use(user);
    }
}

[CreateAssetMenu(fileName = "VirilityTempBooster", menuName = "Item/VirilityTempBooster")]
public class VirilityTempBooster : Drinkable
{
    public VirilityTempBooster()
    {
        ItemId = ItemId.VirilityBooster;
        Title = "Virility week booster";
    }

    public override string Use(BasicChar user)
    {
        TempStatMod tempMod = new TempStatMod(10, ModTypes.Flat, typeof(VirilityTempBooster).Name, 168);
        user.PregnancySystem.Virility.AddTempMod(tempMod);
        return base.Use(user);
    }
}