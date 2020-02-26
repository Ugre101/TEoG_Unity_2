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
    [SerializeField] protected int value = 0;

    [TextArea]
    [SerializeField] protected string desc = string.Empty;

    public string Title => title;
    public string UseName => useName;
    public ItemId ItemId => itemId;
    public Sprite Sprite => sprite;
    public ItemTypes Type => type;
    public int Value => value;
    public int SellValue => Mathf.FloorToInt(value * 0.6f);
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
    public Weapon(ItemId itemId, string title) : base(itemId, title, ItemTypes.Weapon)
    {
        useName = "Equip";
        Slots = new List<EquipSlot>() { EquipSlot.RightHand };
    }

    public List<AssingStatmod> Mods { get; } = new List<AssingStatmod>();
    public List<EquipSlot> Slots { get; } = new List<EquipSlot>();
}