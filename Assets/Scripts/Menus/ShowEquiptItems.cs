using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquiptItems
{
    [SerializeField]
    private EquiptItem head = new EquiptItem(EquipSlot.Head), chest = new EquiptItem(EquipSlot.Chest), leftHand = new EquiptItem(EquipSlot.LeftHand), rightHand = new EquiptItem(EquipSlot.RightHand), pants = new EquiptItem(EquipSlot.Pants),
        boots = new EquiptItem(EquipSlot.Boots);

    public EquiptItem Head => head;
    public EquiptItem Chest => chest;
    public EquiptItem LeftHand => leftHand;
    public EquiptItem RightHand => rightHand;
    public EquiptItem Pants => pants;
    public EquiptItem Boots => boots;

    public EquiptItem GetSlot(EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.Head: return Head;
            case EquipSlot.Chest: return Chest;
            case EquipSlot.LeftHand: return LeftHand;
            case EquipSlot.RightHand: return RightHand;
            case EquipSlot.Pants: return Pants;
            case EquipSlot.Boots: return Boots;
            default:
                return null;
        }
    }

    public List<EquiptItem> GetAll => new List<EquiptItem>() { Head, Chest, LeftHand, RightHand, Pants, Boots };
}

[System.Serializable]
public class EquiptItem
{
    public EquiptItem(EquipSlot slot) => this.slot = slot;

    [SerializeField] private ItemIds item;
    [SerializeField] private EquipSlot slot;
    [SerializeField] private bool hasItem = false;
    public ItemIds Item => item;
    public EquipSlot Slot => slot;
    public bool HasItem => hasItem;

    public void AddItem(ItemIds itemId)
    {
        item = itemId;
        hasItem = true;
        GotItem?.Invoke();
    }

    public delegate void AddedItem();

    public event AddedItem GotItem;
}

public static class EquiptItemsExtensions
{
    public static void AutoEquipItem(this BasicChar basicChar, Item item)
    {
        if (item is IEquip equip)
        {
            List<EquiptItem> equiptItems = new List<EquiptItem>();
            equip.Slots.ForEach(es =>
            {
                equiptItems.Add(basicChar.EquiptItems.GetSlot(es));
            });
            if (equiptItems.Exists(ei => !ei.HasItem))
            {
                HandleItem(basicChar, item, equiptItems.Find(ei => !ei.HasItem));
            }
            else
            {
                EquiptItem equipTo = equiptItems[0]; // for now just take first slot
                if (basicChar.Inventory.HasSpace)
                {
                    basicChar.Inventory.AddItem(equipTo.Item);
                    CleanModsFromItem(basicChar, equipTo);
                    HandleItem(basicChar, item, equipTo);
                }
                else
                {
                    // TODO add warning no inv space
                }
            }
        }
    }

    public static void ManualEquipItem(this BasicChar basicChar, Item item, EquipSlot slot)
    {
        if (item is IEquip equip)
        {
            EquiptItem equiptItem = basicChar.EquiptItems.GetSlot(slot);
            if (equip.Slots.Exists(s => s == slot))
            {
                if (equiptItem.HasItem)
                {
                    basicChar.Inventory.AddItem(equiptItem.Item);
                    CleanModsFromItem(basicChar, equiptItem);
                    HandleItem(basicChar, item, equiptItem);
                }
                else
                {
                    HandleItem(basicChar, item, equiptItem);
                }
            }
        }
    }

    private static void HandleItem(BasicChar basicChar, Item item, EquiptItem equipTo)
    {
        equipTo.AddItem(item.ItemId);
        HandleStatMods(basicChar, item, equipTo);
        HandleHealthMods(basicChar, item, equipTo);
        HandleFertViri(basicChar, item, equipTo);
    }

    private static void CleanModsFromItem(BasicChar basicChar, EquiptItem equipt)
    {
        basicChar.Stats.GetAll.ForEach(s => s.RemoveFromSource(equipt.Slot.ToString()));
        basicChar.WP.RemoveFromSource(equipt.Slot.ToString());
        basicChar.HP.RemoveFromSource(equipt.Slot.ToString());
        basicChar.WP.Recovery.RemoveFromSource(equipt.Slot.ToString());
        basicChar.HP.Recovery.RemoveFromSource(equipt.Slot.ToString());
        basicChar.PregnancySystem.Fertility.RemoveFromSource(equipt.Slot.ToString());
        basicChar.PregnancySystem.Virility.RemoveFromSource(equipt.Slot.ToString());
    }

    private static void HandleStatMods(BasicChar basicChar, Item item, EquiptItem equipTo)
    {
        if (item is IHaveStatMods mods)
        {
            foreach (AssingStatmod sm in mods.StatMods)
            {
                StatMod newMod = new StatMod(sm.StatMod.Value, equipTo.Slot.ToString(), sm.StatMod.ModType);
                basicChar.Stats.GetStat(sm.StatTypes).AddMods(newMod);
            }
        }
    }

    private static void HandleHealthMods(BasicChar basicChar, Item item, EquiptItem equipt)
    {
        if (item is IHaveHealthMods mods)
        {
            foreach (HealthMod mod in mods.HealthMods)
            {
                HealthMod newMod = new HealthMod(mod.Value, mod.ModType, equipt.Slot.ToString(), mod.HealthType);
                if (newMod.HealthType == HealthTypes.Health)
                {
                    basicChar.HP.AddMods(newMod);
                }
                else
                {
                    basicChar.WP.AddMods(newMod);
                }
            }
        }
        if (item is IHaveRecoveryMods recoveryMods)
        {
            foreach (HealthMod mod in recoveryMods.RecoveryMods)
            {
                HealthMod newMod = new HealthMod(mod.Value, mod.ModType, equipt.Slot.ToString(), mod.HealthType);
                if (newMod.HealthType == HealthTypes.Health)
                {
                    basicChar.HP.Recovery.AddMods(mod);
                }
                else
                {
                    basicChar.WP.Recovery.AddMods(mod);
                }
            }
        }
    }

    private static void HandleFertViri(BasicChar basicChar, Item item, EquiptItem equipt)
    {
        if (item is IHaveFertilityMods fertilityMods)
        {
            foreach (StatMod m in fertilityMods.FertMods)
            {
                basicChar.PregnancySystem.Fertility.AddMods(m);
            }
        }
        if (item is IHaveVirilityMods virilityMods)
        {
            foreach (StatMod m in virilityMods.ViriMods)
            {
                basicChar.PregnancySystem.Virility.AddMods(m);
            }
        }
    }
}