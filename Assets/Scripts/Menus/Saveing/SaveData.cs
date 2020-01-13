using System;
using System.Collections.Generic;
using UnityEngine;
using Vore;

[Serializable]
public class Save
{
    private readonly PlayerMain Player;
    private readonly Transform Pos;
    private readonly Dorm dorm;
    private readonly VoreChar voreChar;

    public Save(PlayerMain player, Dorm theDorm)
    {
        Player = player;
        Pos = player.transform;
        dorm = theDorm;
        voreChar = player.VoreChar;
    }

    public string SaveData()
    {
        PlayerSave playerSave = new PlayerSave(Player);
        List<DormSave> dormSaves = dorm.Save();
        PosSave playerPos = new PosSave(Pos.position, MapEvents.ActiveMap, MapEvents.CurrentMap.transform.name);
        HomeSave homeSave = Home.Stats.Save();
        VoreSaves voreSaves = voreChar.Save();
        FullSave fullSave = new FullSave(playerSave, playerPos, dormSaves, homeSave, voreSaves);
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        // Singleton static
        MapEvents.GetMapEvents.Load(fullSave.PosPart);
        // Reference
        JsonUtility.FromJsonOverwrite(fullSave.PlayerPart.Who, Player);
        Home.Stats.Load(fullSave.HomePart);
        dorm.Load(fullSave.DormPart);
        voreChar.Load(fullSave.VoreSaves, Player);
        // Pure static
        DateSystem.Load(fullSave.DatePart);
        QuestsSystem.Load(fullSave.QuestSave);
        PlayerFlags.Load(fullSave.PlayerFlagsSave);
        EventLog.ClearLog();
    }
}

[Serializable]
public class FullSave
{
    [SerializeField] private PlayerSave playerPart;
    [SerializeField] private PosSave posPart;
    [SerializeField] private List<DormSave> dormPart;
    [SerializeField] private DateSave datePart;
    [SerializeField] private HomeSave homePart;
    [SerializeField] private VoreSaves voreSaves;
    [SerializeField] private QuestSave questSave;
    [SerializeField] private PlayerFlagsSave playerFlags;
    public PlayerSave PlayerPart => playerPart;
    public PosSave PosPart => posPart;
    public List<DormSave> DormPart => dormPart;
    public DateSave DatePart => datePart;
    public HomeSave HomePart => homePart;
    public VoreSaves VoreSaves => voreSaves;
    public QuestSave QuestSave => questSave;
    public PlayerFlagsSave PlayerFlagsSave => playerFlags;

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, HomeSave parHome, VoreSaves vore)
    {
        this.playerPart = player;
        this.posPart = pos;
        this.dormPart = dorm;
        this.datePart = DateSystem.Save;
        this.homePart = parHome;
        this.voreSaves = vore;
        this.questSave = QuestsSystem.Save();
        this.playerFlags = PlayerFlags.Save();
    }
}

[Serializable]
public struct PlayerSave
{
    [SerializeField] private string who;

    public PlayerSave(BasicChar whom) => who = JsonUtility.ToJson(whom);

    public string Who => who;
}

[Serializable]
public struct PosSave
{
    [SerializeField] private Vector3 pos;

    [SerializeField] private WorldMaps world;

    [SerializeField] private string map;

    public Vector3 Pos => pos;
    public WorldMaps World => world;
    public string Map => map;

    public PosSave(Vector3 vec3, WorldMaps currWorld, string currMap)
    {
        pos = vec3;
        world = currWorld;
        map = currMap;
    }
}

[Serializable]
public struct DateSave
{
    [SerializeField] private int hour;
    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;

    public int Year => year;
    public int Month => month;
    public int Day => day;
    public int Hour => hour;

    public DateSave(int Year, int Month, int Day, int Hour)
    {
        this.year = Year;
        this.month = Month;
        this.day = Day;
        this.hour = Hour;
    }
}

[Serializable]
public struct HomeSave
{
    [SerializeField] private int dormKitchenLevel;
    [SerializeField] private int dormLevel;
    [SerializeField] private int dormGymLevel;

    public int DormLevel => dormLevel;
    public int DormGymLevel => dormGymLevel;
    public int DormKitchenLevel => dormKitchenLevel;

    public HomeSave(int parDormLevel, int parDormGymLevel, int parDormKitchenLevel)
    {
        this.dormLevel = parDormLevel;
        this.dormGymLevel = parDormGymLevel;
        this.dormKitchenLevel = parDormKitchenLevel;
    }
}