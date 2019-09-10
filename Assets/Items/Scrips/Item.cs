using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTypes
{
    Consumables,
    Misc
}
[CreateAssetMenu(fileName = "Item", menuName = "Item/Items holder")]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite sprite;
    protected ItemId itemId;
    public ItemId Id { get { return itemId; } }
    [SerializeField]
    protected ItemTypes type;
    public ItemTypes Type { get { return type; } }
    [SerializeField]
    protected string title = "Item";
    public string Title { get { return title; } }
    [SerializeField]
    protected string desc = "";
    public string Desc { get { return desc; } }
    [SerializeField]
    protected string useName = "Use";
    public string UseName { get { return useName; } }
    public virtual void Use()
    {

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
