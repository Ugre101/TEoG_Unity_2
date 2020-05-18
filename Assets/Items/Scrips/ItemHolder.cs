using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class ItemHolder : ScriptableObject
{
    [SerializeField] private List<Item> items = new List<Item>();

    public List<Item> ItemsDict => items;

    public void Add(Item toAdd) => ItemsDict.Add(toAdd);

    public Item GetById(ItemIds parId)
    {
        try
        {
            return ItemsDict.Find(i => i.ItemId == parId);
        }
        catch
        {
            throw new System.ArgumentException($"Item with id\"{parId}\" doesn't exist in itemholder.", "parId");
        }
    }

    public bool HasItem(ItemIds id) => ItemsDict.Exists(i => i.ItemId == id);
}