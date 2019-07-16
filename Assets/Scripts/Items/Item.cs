using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Consumables,
    Misc
}
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
[System.Serializable]
public class Items : ScriptableObject
{
    public List<Item> items;
}
public enum ItemRefs
{
    Item,
    TestPotion
}
[CreateAssetMenu(fileName = "Item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public int Amount = 1;
    public Sprite sprite;
    [SerializeField]
    protected ItemTypes type;
    public ItemTypes Type { get { return type; } }
    [SerializeField]
    private string title = "Item";
    public string Title { get { return title; } }
    protected string useName = "Use";
    public string UseName { get { return useName; } }
    public virtual bool Use(BasicChar user)
    {
        Amount--;
        return Amount < 1;
    }

    public bool Remove(int toRemove =1)
    {
        Amount -= toRemove;
        return Amount > 0;
    }
}
public class Drinks : Item
{
    public Drinks()
    {
        useName = "Drink";
        type = ItemTypes.Consumables;
    }
}
public class Edibles : Item
{
    public Edibles()
    {
        useName = "Eat";
        type = ItemTypes.Consumables;
    }
}
public class Misc : Item
{
    public Misc()
    {
        useName = "Use";
        type = ItemTypes.Misc;
    }
}
