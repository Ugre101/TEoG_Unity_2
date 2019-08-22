using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Consumables,
    Misc
}
[CreateAssetMenu(fileName = "Item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite sprite;
    [SerializeField]
    protected ItemTypes type;
    public ItemTypes Type { get { return type; } }
    [SerializeField]
    private string title = "Item";
    public string Title { get { return title; } }
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
