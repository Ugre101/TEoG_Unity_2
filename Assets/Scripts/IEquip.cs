public enum EquipSlot
{
    Head,
    Chest,
    LeftHand,
    RightHand,
    Pants,
    Boots
}

public interface IEquip
{
    EquipSlot Slot { get; }
}