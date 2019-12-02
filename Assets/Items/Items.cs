using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class Items : ScriptableObject
{
    [SerializeField]
    private List<Item> items = new List<Item>();

    public List<Item> ItemsDict => items;

    public void Add(Item toAdd)
    {
        items.Add(toAdd);
    }
    public Item GetById(ItemId parId)
    {
        return ItemsDict.Find(i => i.Id == parId);
    }
}