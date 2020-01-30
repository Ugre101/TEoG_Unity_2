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
    [SerializeField] protected ItemId itemId;
    [SerializeField] protected string title = "Item", useName = "Use";
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected ItemTypes type;
    [SerializeField] protected int sellValue = 0;

    [TextArea]
    [SerializeField] protected string desc = string.Empty;

    public string Title => title;
    public string UseName => useName;
    public ItemId ItemId => itemId;
    public Sprite Sprite => sprite;
    public ItemTypes Type => type;
    public int SellValue => sellValue;
    public string Desc => desc;

    public virtual string Use(BasicChar user) => "used";

    public Item(ItemId itemId, string title, ItemTypes itemTypes)
    {
        this.itemId = itemId;
        this.title = title;
        this.type = itemTypes;
    }
}

public class Drinkable : Item
{
    public Drinkable(ItemId itemId, string title) : base(itemId, title, ItemTypes.Consumables)
    {
        useName = "Drink";
    }
}

public class Edibles : Item
{
    public Edibles(ItemId itemId, string title) : base(itemId, title, ItemTypes.Consumables)
    {
        useName = "Eat";
    }
}

public class Misc : Item
{
    public Misc(ItemId itemId, string title) : base(itemId, title, ItemTypes.Misc)
    {
        useName = "Use";
    }
}

public class Weapon : Item, IHaveStatMods, IEquip
{
    [SerializeField] protected List<AssingStatmod> mods = new List<AssingStatmod>();
    [SerializeField] protected EquipSlot slot;

    public Weapon(ItemId itemId, string title) : base(itemId, title, ItemTypes.Weapon)
    {
        useName = "Equip";
    }

    public List<AssingStatmod> Mods => mods;
    public EquipSlot Slot => slot;
}