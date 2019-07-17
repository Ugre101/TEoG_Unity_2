using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public Items itemRefs;
    public BasicChar Owner;
    public List<Item> Items = new List<Item>();
    public void Show()
    {
        List<GameObject> showInven = new List<GameObject>();
        foreach(Item i in Items)
        {

        }
    }
    public void AddItem(ItemRefs theitem)
    {
        Debug.Log(itemRefs.items[1].name);
        Item toAdd = itemRefs.items.Find(i => i.name == theitem.ToString());
        if (toAdd == null)
        {
            return;
        }
        if (Items.Exists(i => i.Title == toAdd.Title))
        {
            Items.Find(i => i.Title == toAdd.Title).Amount++;
        }else
        {
            toAdd.Amount = 1;
            Items.Add(toAdd);
        }
    }
}
