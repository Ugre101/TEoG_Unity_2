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