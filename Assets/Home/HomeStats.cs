using UnityEngine;

public class HomeStats
{
    public DormUpgrade Dorm { get; } = new DormUpgrade();
    public DormGymUpgrage DormGym { get; } = new DormGymUpgrage();
    public DormKitchenUpgrade DormKitchen { get; } = new DormKitchenUpgrade();

    public HomeSave Save()
    {
        return new HomeSave(Dorm.Level, DormGym.Level, DormKitchen.Level);
    }

    public void Load(HomeSave toLoad)
    {
        Dorm.Load(toLoad.DormLevel);
        DormGym.Load(toLoad.DormGymLevel);
        DormKitchen.Load(toLoad.DormKitchenLevel);
    }
}

public class HomeUpgrade
{
    protected int baseCost = 20;

    protected float costExpo = 1f;

    public int Level { get; protected set; } = 0;
    public int Cost => Mathf.CeilToInt(Mathf.Pow(baseCost + Level, costExpo));

    public bool CanAfford(BasicChar buyer) => buyer.Currency.Gold >= Cost;

    /// <summary> If you have the gold it will take the gold and upgrade building. </summary>
    /// <returns> If building level up was succesful</returns>
    public bool Upgrade(BasicChar buyer)
    {
        if (buyer.Currency.TryToBuy(Cost))
        {
            Level++;
            return true;
        }
        return false;
    }

    public void Load(int parLevel) => Level = parLevel;
}

public class DormUpgrade : HomeUpgrade
{
    public DormUpgrade()
    {
        baseCost = 50;
        costExpo = 1f;
        Level = 0;
    }
}

public class DormGymUpgrage : HomeUpgrade
{
    public DormGymUpgrage()
    {
        baseCost = 30;
        costExpo = 1f;
        Level = 1;
    }
}

public class DormKitchenUpgrade : HomeUpgrade
{
    public DormKitchenUpgrade()
    {
        baseCost = 30;
        costExpo = 1f;
        Level = 0;
    }
}