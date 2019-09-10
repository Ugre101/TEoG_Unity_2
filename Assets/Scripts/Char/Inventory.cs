using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    // Base amount
    private readonly int baseAmount = 40;

    // Maybe add bonus to inventory size?
    public int SlotsAmount { get { return baseAmount; } }

    //public Items itemRefs;

    //public BasicChar Owner;
    public List<InventoryItem> Items = new List<InventoryItem>();

    public bool AddItem(ItemId theitem)
    {
        /*   Item toAdd = itemRefs.items.Find(i => i.name == theitem.ToString());
        if (toAdd == null || Items.Count == SlotsAmount)
        {
            return false;
        } */
        if (Items.Exists(i => i.id == theitem))
        {
            Items.Find(i => i.id == theitem).amount++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(theitem)
            {
                invPos = FirstEmpty()
            };
            Items.Add(newItem);
        }
        return true;
    }

    private int FirstEmpty()
    {
        int First = 0;
        while (Items.Exists(inv => inv.invPos == First))
        {
            First++;
        }
        return First;
    }
}

[System.Serializable]
public class InventoryItem
{
    public InventoryItem(ItemId toAdd, int num = 1)
    {
        id = toAdd;
        amount = num;
    }

   // public Item item;
    public ItemId id;
    public int amount;
    public int invPos = -1;
}
public enum ItemId
{
    Pouch,
    Potion
}