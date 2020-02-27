using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ItemTypes
{
    Consumables,
    Misc,
    Weapon,
    Armour
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Items holder")]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField] protected ItemIds itemId;
    [SerializeField] protected string title = "Item", useName = "Use";
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected ItemTypes type;
    [SerializeField] protected int value = 0;

    [TextArea]
    [SerializeField] protected string desc = string.Empty;

    public string Title => title;
    public string UseName => useName;
    public ItemIds ItemId => itemId;
    public Sprite Sprite => sprite;
    public ItemTypes Type => type;
    public int Value => value;
    public int SellValue => Mathf.FloorToInt(value * 0.6f);
    public string Desc => desc;
    public string TypeName => this.GetType().Name;

    public virtual string Use(BasicChar user) => "used";

    public Item(ItemIds itemId, string title, ItemTypes itemTypes)
    {
        this.itemId = itemId;
        this.title = title;
        this.type = itemTypes;
    }
}

public class Drinkable : Item
{
    public Drinkable(ItemIds itemId, string title) : base(itemId, title, ItemTypes.Consumables)
    {
        useName = "Drink";
    }
}

public class Edibles : Item
{
    public Edibles(ItemIds itemId, string title) : base(itemId, title, ItemTypes.Consumables)
    {
        useName = "Eat";
    }
}

public class Misc : Item
{
    public Misc(ItemIds itemId, string title) : base(itemId, title, ItemTypes.Misc)
    {
        useName = "Use";
    }
}

public class Weapon : Item, IHaveStatMods, IEquip
{
    public Weapon(ItemIds itemId, string title) : base(itemId, title, ItemTypes.Weapon)
    {
        useName = "Equip";
    }

    public List<AssingStatmod> StatMods { get; } = new List<AssingStatmod>();
    public List<EquipSlot> Slots { get; } = new List<EquipSlot>();
}

public class Armour : Item, IEquip
{
    public Armour(ItemIds itemId, string title) : base(itemId, title, ItemTypes.Armour)
    {
        useName = "Equip";
    }

    public List<EquipSlot> Slots { get; } = new List<EquipSlot>();
}

public static class ItemExtensions
{
    public static string FullDesc(this Item item)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(item.Desc);
        if (item is IHaveStatMods statMods)
        {
            foreach (AssingStatmod statmod in statMods.StatMods)
            {
                string text = $"\n{statmod.StatTypes.ToString()}: {statmod.StatMod.Value}";
                if (statmod.StatMod.ModType == ModTypes.Precent)
                {
                    text += "%";
                }
                stringBuilder.Append(text);
            }
        }
        return stringBuilder.ToString();
    }
}