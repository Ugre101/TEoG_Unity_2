using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFairyDust : Item
{
    public ItemFairyDust()
    {
        itemId = ItemId.Potion;
        type = ItemTypes.Consumables;
        title = "Fairy dust";
        useName = "Lick";
        desc = "";
    }
    public override string Use(ThePrey user)
    {
        return base.Use(user);
    }
}
