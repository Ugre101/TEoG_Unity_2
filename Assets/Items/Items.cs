using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class Items : ScriptableObject
{
    public List<Item> items;

    public void Add(Item toAdd)
    {
        items.Add(toAdd);
    }
}

public enum ItemRefs
{
    Item,
    TestPotion
}