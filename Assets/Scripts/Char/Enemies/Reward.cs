using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    [Range(0, 1000)]
    [SerializeField] private int expReward = 0;

    public int ExpReward => Mathf.FloorToInt(expReward * Random.Range(1 - rng, 1 + rng));

    [Range(0, 1000)]
    [SerializeField] private int goldReward = 0;

    public int GoldReward => Mathf.FloorToInt(goldReward * Random.Range(1 - rng, 1 + rng));

    [Range(0, 0.9f)]
    [SerializeField] private float rng = 0.3f;

    [SerializeField] private List<ItemDrop> drops = new List<ItemDrop>();

    public void HandleDrops(BasicChar basicChar)
    {
        drops.ForEach(d =>
        {
            if (d.DropChance < Random.Range(0, 1f))
            {
                basicChar.Inventory.AddItem(d.Item);
            }
        });
    }
}

[System.Serializable]
public class ItemDrop
{
    [SerializeField] private ItemId item;
    [SerializeField] private float dropChance;

    public ItemDrop(ItemId item, float dropChance = 0.3f)
    {
        this.item = item;
        this.dropChance = dropChance;
    }

    public ItemId Item => item;
    public float DropChance => dropChance;
}