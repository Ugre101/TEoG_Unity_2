using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class ItemHolder : ScriptableObject
{
    [SerializeField] private List<Item> items = new List<Item>();

    private bool firstUse = true;

    private Dictionary<ItemIds, Item> itemsDict;

    public Dictionary<ItemIds, Item> ItemsDict
    {
        get
        {
            if (firstUse)
            {
                itemsDict = items.ToDictionary(id => id.ItemId);
                firstUse = false;
            }
            return itemsDict;
        }
    }

    public Item GetById(ItemIds parId)
    {
        if (ItemsDict.TryGetValue(parId, out Item item))
        {
            return item;
        }
        else
        {
            throw new System.ArgumentException($"Item with id\"{parId}\" doesn't exist in itemholder.", "parId");
        }
    }

    public void Add(Item toAdd)
    {
        items.Add(toAdd);
        items.Sort();// Make it easier to find stuff manually
    }

    public bool HasItem(ItemIds id) => ItemsDict.ContainsKey(id);
}