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
    public InventoryItem(ItemId toAdd, int parInvPos)
    {
        id = toAdd;
        amount = 1;
        invPos = parInvPos;
    }

    public InventoryItem(ItemId toAdd, int parInvPos, int num) : this(toAdd, parInvPos) => amount = num;

    public InventoryItem(ItemId toAdd, int parInvPos, int num, bool reusable) : this(toAdd, parInvPos, num) => this.reusable = reusable;

    public InventoryItem(ItemId toAdd, int parInvPos, bool reusable) : this(toAdd, parInvPos) => this.reusable = reusable;

    [SerializeField] private ItemId id;

    [SerializeField] private int amount;

    [SerializeField] private int invPos = -1;
    [SerializeField] private bool reusable = false;
    public ItemId Id => id;
    public int Amount { get => amount; set => amount = value; }
    public int InvPos { get => invPos; set => invPos = value; }
    public bool Reusable => reusable;
}

public static class InventoryExtensions
{
    /// <summary>Find a item by it's invPos </summary>
    public static InventoryItem FindByPos(this List<InventoryItem> inventory, int invPos) => inventory.Find(i => i.InvPos == invPos);

    /// <summary>Return if item exist by it's invPos </summary>
    public static bool ExistByPos(this List<InventoryItem> inventory, int invPos) => inventory.Exists(i => i.InvPos == invPos);

    public static void Clean(this Inventory inv) => inv.Items.RemoveAll(i => i.Amount < 1);
}