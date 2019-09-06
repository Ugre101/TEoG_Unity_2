using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    // Base amount
    private readonly int baseAmount = 40;

    // Maybe add bonus to inventory size?
    public int SlotsAmount { get { return baseAmount; } }

    public Items itemRefs;

    //public BasicChar Owner;
    public List<InventoryItem> Items = new List<InventoryItem>();

    public bool AddItem(ItemRefs theitem)
    {
        Item toAdd = itemRefs.items.Find(i => i.name == theitem.ToString());
        if (toAdd == null || Items.Count == SlotsAmount)
        {
            return false;
        }
        if (Items.Exists(i => i.item.Title == toAdd.Title))
        {
            Items.Find(i => i.item.Title == toAdd.Title).amount++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(toAdd)
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
    public InventoryItem(Item toAdd, int num = 1)
    {
        item = toAdd;
        amount = num;
    }

    public Item item;
    public int amount;
    public int invPos = -1;
}