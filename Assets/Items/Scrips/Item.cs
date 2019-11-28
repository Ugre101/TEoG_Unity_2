using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Consumables,
    Misc,
    Weapon
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Items holder")]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField]
    protected Sprite sprite;

    public Sprite Sprite => sprite;

    [SerializeField]
    protected ItemId itemId;

    public ItemId Id => itemId;

    [SerializeField]
    protected ItemTypes type;

    public ItemTypes Type => type;

    [SerializeField]
    protected string title = "Item";

    public string Title => title;

    [SerializeField]
    [TextArea]
    protected string desc = "";

    public string Desc => desc;

    [SerializeField]
    protected string useName = "Use";

    public string UseName => useName;

    public virtual string Use(BasicChar user)
    {
        return "used";
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

public class Weapon : Item
{
    public Weapon()
    {
        useName = "Equip";
        type = ItemTypes.Weapon;
    }

    [SerializeField]
    protected List<StatMods> mods = new List<StatMods>();

    public List<StatMods> Mods => mods;
}