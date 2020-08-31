using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class ItemHolder : ScriptableObject
{
    [SerializeField] private List<Item> items = new List<Item>();

    public Dictionary<ItemIds, Item> ItemsDict { get; private set; }

    private void OnValidate() => ItemsDict = items.ToDictionary(id => id.ItemId);

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
        if (!HasItem(toAdd.ItemId))
        {
            items.Add(toAdd);
            items.Sort();// Make it easier to find stuff manually
        }else
        {
            Debug.Log("Already has item.");
        }
    }

    public bool HasItem(ItemIds id) => items.Exists(i => i.ItemId == id);
}