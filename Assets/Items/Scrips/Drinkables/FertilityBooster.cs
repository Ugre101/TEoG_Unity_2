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