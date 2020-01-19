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
    [SerializeField] private ItemId itemId;
    [SerializeField] private string title = "Item";
    public string Title => title;

    public ItemId ItemId => itemId;

    [field: SerializeField] public Sprite Sprite { get; protected set; }

    [field: SerializeField] public ItemTypes Type { get; protected set; }

    [field: TextArea]
    [field: SerializeField] public string Desc { get; protected set; } = "";

    [field: SerializeField] public string UseName { get; protected set; } = "Use";

    public virtual string Use(BasicChar user)
    {
        return "used";
    }

    public Item()
    {
    }

    public Item(ItemId itemId, string title)
    {
        this.itemId = itemId;
        this.title = title;
    }
}

public class Drinkable : Item
{
    public Drinkable()
    {
        UseName = "Drink";
        Type = ItemTypes.Consumables;
    }

    public Drinkable(ItemId itemId, string title) : base(itemId, title)
    {
        UseName = "Drink";
        Type = ItemTypes.Consumables;
    }
}

public class Edibles : Item
{
    public Edibles()
    {
        UseName = "Eat";
        Type = ItemTypes.Consumables;
    }

    public Edibles(ItemId itemId, string title) : base(itemId, title)
    {
        UseName = "Eat";
        Type = ItemTypes.Consumables;
    }
}

public class Misc : Item
{
    public Misc()
    {
        UseName = "Use";
        Type = ItemTypes.Misc;
    }
}

public class Weapon : Item, IHaveStatMods, IEquip
{
    public Weapon()
    {
        UseName = "Equip";
        Type = ItemTypes.Weapon;
    }

    public Weapon(ItemId itemId, string title) : base(itemId, title)
    {
        UseName = "Equip";
        Type = ItemTypes.Weapon;
    }

    [field: SerializeField] public List<AssingStatmod> Mods { get; protected set; } = new List<AssingStatmod>();

    [field: SerializeField] public EquipSlot Slot { get; private set; }
}