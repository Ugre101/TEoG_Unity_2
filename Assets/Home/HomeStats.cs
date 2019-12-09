using System;
using UnityEngine;

[Serializable]
public class HomeStats
{
    [Header("Dorm")]
    [SerializeField]
    private HomeUpgrade dorm = new HomeUpgrade();
    [SerializeField]
    private HomeUpgrade dormGym = new HomeUpgrade();
    [SerializeField]
    private HomeUpgrade dormKitchen = new HomeUpgrade();
    public HomeUpgrade Dorm => dorm;
    public HomeUpgrade DormGym => dormGym;
    public HomeUpgrade DormKitchen => dormKitchen;

    public HomeSave Save()
    {
        return new HomeSave(Dorm.Level, DormGym.Level, DormKitchen.Level);
    }
    public void Load(HomeSave toLoad)
    {
        dorm.Load(toLoad.dormLevel);
        dormGym.Load(toLoad.dormGymLevel);
        dormKitchen.Load(toLoad.dormKitchenLevel);
    }

}

[Serializable]
public class HomeUpgrade
{
    public HomeUpgrade()
    {
    }

    public HomeUpgrade(int startLevel) => level = startLevel;

    [SerializeField]
    private int level = 0;

    [SerializeField]
    private int baseCost = 0;

    [SerializeField]
    private float costExpo = 0f;

    public int Level => level;
    public int Cost => Mathf.CeilToInt(Mathf.Pow(baseCost, costExpo));

    public bool CanAfford(ThePrey buyer)
    {
        return buyer.Gold >= Cost;
    }

    /// <summary>
    /// If you have the gold it will take the gold and upgrade building.
    /// </summary>
    /// <param name="buyer"></param>
    /// <returns>If building level up was succesful</returns>
    public bool Upgrade(ThePrey buyer)
    {
        bool canAfford = CanAfford(buyer);
        if (canAfford)
        {
            buyer.Gold -= Cost;
            level++;
        }
        return canAfford;
    }
    public void Load(int parLevel)
    {
        level = parLevel;
    }
}