using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public Inventory(BasicChar owner) { Owner = owner; }
    protected BasicChar Owner;
    public List<Item> Items = new List<Item>();
    public void Show()
    {
        List<GameObject> showInven = new List<GameObject>();
        foreach(Item i in Items)
        {

        }
    }
    public void AddItem(Item toAdd)
    {
        toAdd.User = Owner;
        if (Items.Exists(i => i.Name == toAdd.Name))
        {
            Items.Find(i => i.Name == toAdd.Name).Amount++;
        }else
        {
            Items.Add(toAdd);
        }
    }
    public void AddConsumable(Consumables which)
    {

    }
}
