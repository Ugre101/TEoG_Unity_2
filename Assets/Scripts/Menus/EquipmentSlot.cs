﻿using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private EquipSlot slot;

    [SerializeField] private Item item;

    public Item AddTo(Item parItem)
    {
        Item toReturn = item;
        item = parItem;
        return toReturn;
    }

    public void DragItem(Item item)
    {
        PlayerMain.GetPlayer.ManualEquipItem(item, slot);
    }
}