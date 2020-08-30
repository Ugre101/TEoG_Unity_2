using UnityEngine;

public static class DormUpgrades
{
    public static HomeUpgrade MainHouse { get; } = new HomeUpgrade();
    public static HomeUpgrade Dorm { get; } = new HomeUpgrade();
    public static HomeUpgrade Gym { get; } = new HomeUpgrade();
    public static HomeUpgrade Kitchen { get; } = new HomeUpgrade();

    public static HomeSave Save()
    {
        return new HomeSave(MainHouse.Level, Dorm.Level, Gym.Level, Kitchen.Level);
    }

    public static void Load(HomeSave toLoad)
    {
        MainHouse.Load(toLoad.HouseLevel);
        Dorm.Load(toLoad.DormLevel);
        Gym.Load(toLoad.GymLevel);
        Kitchen.Load(toLoad.KitchenLevel);
    }
}

public class HomeUpgrade
{
    private int level = 0;

    public int Level
    {
        get => level; set
        {
            level = value;
            Refresh?.Invoke();
        }
    }

    public void Upgrade() => Level++;

    public void DownGrade() => Level = Mathf.Max(0, Level - 1);

    public void Destroy() => Level = 0;

    public void Load(int parLevel) => Level = parLevel;

    public delegate void UpgradedHome();

    public event UpgradedHome Refresh;
}

[System.Serializable]
public struct HomeSave
{
    [SerializeField] private int houseLevel;
    [SerializeField] private int dormKitchenLevel;
    [SerializeField] private int dormLevel;
    [SerializeField] private int dormGymLevel;
    public int HouseLevel => houseLevel;
    public int DormLevel => dormLevel;
    public int GymLevel => dormGymLevel;
    public int KitchenLevel => dormKitchenLevel;

    public HomeSave(int houseLevel, int parDormLevel, int parDormGymLevel, int parDormKitchenLevel)
    {
        this.houseLevel = houseLevel;
        this.dormLevel = parDormLevel;
        this.dormGymLevel = parDormGymLevel;
        this.dormKitchenLevel = parDormKitchenLevel;
    }
}