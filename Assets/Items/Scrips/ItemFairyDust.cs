using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFairyDust : Item
{
    public ItemFairyDust() : base(ItemId.Potion,"Fairy dust")
    {
        Type = ItemTypes.Consumables;
        UseName = "Lick";
        Desc = "";
    }
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}
