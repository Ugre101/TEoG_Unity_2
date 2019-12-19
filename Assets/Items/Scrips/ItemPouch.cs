using UnityEngine;

[CreateAssetMenu(fileName = "Pouch", menuName = "Item/Pouch")]
public class ItemPouch : Item
{
    public ItemPouch()
    {
        ItemId = ItemId.Pouch;
        Type = ItemTypes.Misc;
        Title = "Pouch";
    }

    public override string Use(BasicChar user)
    {
        user.Currency.Gold += Random.Range(10, 30);
        return "You gain some gold.";
    }
}