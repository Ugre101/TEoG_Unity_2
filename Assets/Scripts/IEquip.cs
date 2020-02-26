using System.Collections.Generic;

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
    List<EquipSlot> Slots { get; }
}