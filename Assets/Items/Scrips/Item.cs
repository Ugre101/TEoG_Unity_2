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
    [field: SerializeField] public Sprite Sprite { get; protected set; }

    [field: SerializeField] public ItemId ItemId { get; protected set; }

    [field: SerializeField] public ItemTypes Type { get; protected set; }

    [field: SerializeField] public string Title { get; protected set; } = "Item";

    [field: TextArea]
    [field: SerializeField] public string Desc { get; protected set; } = "";

    [field: SerializeField] public string UseName { get; protected set; } = "Use";

    public virtual string Use(BasicChar user)
    {
        return "used";
    }
}

public class Drinkable : Item
{
    public Drinkable()
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

    [field: SerializeField] public List<StatMod> Mods { get; protected set; } = new List<StatMod>();

    [field: SerializeField] public EquipSlot Slot { get; private set; }
}