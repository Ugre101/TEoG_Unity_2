using UnityEngine;

public class EquiptItems : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    public Items items;
    [field: SerializeField] public EquipmentSlot Head { get; private set; }
    [field: SerializeField] public EquipmentSlot Chest { get; private set; }
    [field: SerializeField] public EquipmentSlot LeftHand { get; private set; }
    [field: SerializeField] public EquipmentSlot RightHand { get; private set; }
    [field: SerializeField] public EquipmentSlot Pants { get; private set; }
    [field: SerializeField] public EquipmentSlot Boots { get; private set; }

    public Item EquipItem(Item toEquip)
    {
        IEquip equip = toEquip as IEquip;
        switch (equip.Slot)
        {
            case EquipSlot.Head:
                return Head.AddTo(toEquip);

            case EquipSlot.Chest:
                return Chest.AddTo(toEquip);

            case EquipSlot.LeftHand:
                return LeftHand.AddTo(toEquip);

            case EquipSlot.RightHand:
                return RightHand.AddTo(toEquip);

            case EquipSlot.Pants:
                return Pants.AddTo(toEquip);

            case EquipSlot.Boots:
                return Boots.AddTo(toEquip);

            default:
                return null;
        }
    }

    // Start is called before the first frame update
}