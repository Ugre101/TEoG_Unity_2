using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFairyDust : Item
{
    public ItemFairyDust()
    {
        ItemId = ItemId.Potion;
        Type = ItemTypes.Consumables;
        Title = "Fairy dust";
        UseName = "Lick";
        Desc = "";
    }
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}
