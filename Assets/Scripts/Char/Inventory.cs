using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    // Base amount
    private readonly int baseAmount = 40;

    [SerializeField] private int bonusAmount = 0;

    public void SetBonusAmount(int amount) => bonusAmount = amount;

    public int SlotsAmount => baseAmount + bonusAmount;

    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

    public List<InventoryItem> Items => items;

    public bool AddItem(ItemId theitem)
    {
        if (Items.Exists(i => i.Id == theitem))
        {
            Items.Find(i => i.Id == theitem).Amount++;
        }
        else if (items.Count <= SlotsAmount)
        {
            Items.Add(new InventoryItem(theitem, FirstEmpty()));
        }
        else
        {
            return false;
        }
        return true;
    }

    private int FirstEmpty()
    {
        int First = 0;
        while (Items.Exists(inv => inv.InvPos == First))
        {
            First++;
        }
        return First;
    }
}

[System.Serializable]
public class InventoryItem
{
    public InventoryItem(ItemId toAdd, int parInvPos, int num = 1)
    {
        id = toAdd;
        amount = num;
        invPos = parInvPos;
    }

    [SerializeField]
    private ItemId id;

    [SerializeField]
    private int amount;

    [SerializeField]
    private int invPos = -1;

    public ItemId Id => id;
    public int Amount { get => amount; set => amount = value; }
    public int InvPos { get => invPos; set => invPos = value; }
}

public static class InventoryExtensions
{
    /// <summary>Find a item by it's invPos </summary>
    public static InventoryItem FindByPos(this List<InventoryItem> inventory, int invPos)
    {
        return inventory.Find(i => i.InvPos == invPos);
    }

    /// <summary>Return if item exist by it's invPos </summary>
    public static bool ExistByPos(this List<InventoryItem> inventory, int invPos)
    {
        return inventory.Exists(i => i.InvPos == invPos);
    }
}