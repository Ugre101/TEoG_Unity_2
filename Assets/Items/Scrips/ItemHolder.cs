using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class ItemHolder : ScriptableObject
{
    [SerializeField] private List<Item> items = new List<Item>();

    private bool firstUse = true;

    public Dictionary<ItemIds, Item> ItemsDict
    {
        get
        {
            if (firstUse)
            {
                items1 = items.ToDictionary(id => id.ItemId);
                firstUse = false;
            }
            return items1;
        }
    }

    private Dictionary<ItemIds, Item> items1;

    public Item GetById(ItemIds parId)
    {
        if (ItemsDict.ContainsKey(parId))
        {
            return ItemsDict[parId];
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

    public bool HasItem(ItemIds id) => items.Exists(i => i.ItemId == id);

}