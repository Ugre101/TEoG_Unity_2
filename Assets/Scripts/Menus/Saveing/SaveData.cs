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
    private readonly MapEvents mapEvents;
    private readonly Home home;
    private readonly VoreChar voreChar;
    private PlayerSave save;

    public Save(PlayerMain player, Dorm theDorm, MapEvents map, Home parHome)
    {
        Player = player;
        Pos = player.transform;
        dorm = theDorm;
        mapEvents = map;
        home = parHome;
        voreChar = player.VoreChar;
    }

    public string SaveData()
    {
        save = new PlayerSave(Player);
        List<DormSave> temp = dorm.Save();
        PosSave pos = new PosSave(Pos.position, mapEvents.ActiveMap, mapEvents.CurrentMap.transform.name);
        HomeSave homeSave = home.Stats.Save();
        VoreSaves voreSaves = voreChar.Save();
        FullSave fullSave = new FullSave(save, pos, temp, homeSave, voreSaves);
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        mapEvents.Load(fullSave.posPart);
        JsonUtility.FromJsonOverwrite(fullSave.playerPart.Who, Player);
        home.Stats.Load(fullSave.homePart);
        dorm.Load(fullSave.dormPart);
        voreChar.Load(fullSave.voreSaves, Player);
        DateSystem.Load(fullSave.datePart);
        QuestsSystem.Load(fullSave.questSave);
        EventLog.ClearLog();
    }
}

[Serializable]
public class FullSave
{
    public PlayerSave playerPart;
    public PosSave posPart;
    public List<DormSave> dormPart;
    public DateSave datePart;
    public HomeSave homePart;
    public VoreSaves voreSaves;
    public QuestSave questSave;

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, HomeSave parHome, VoreSaves vore)
    {
        playerPart = player;
        posPart = pos;
        dormPart = dorm;
        datePart = DateSystem.Save;
        homePart = parHome;
        voreSaves = vore;
        questSave = QuestsSystem.Save();
    }
}

[Serializable]
public struct PlayerSave
{
    [SerializeField]
    private string who;

    public PlayerSave(BasicChar whom) => who = JsonUtility.ToJson(whom);

    public string Who => who;
}

[Serializable]
public struct PosSave
{
    [SerializeField]
    private Vector3 pos;

    [SerializeField]
    private WorldMaps world;

    [SerializeField]
    private string map;

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
    public int year, month, day, hour;

    public DateSave(int Year, int Month, int Day, int Hour)
    {
        year = Year;
        month = Month;
        day = Day;
        hour = Hour;
    }
}

[Serializable]
public struct HomeSave
{
    public int dormLevel, dormGymLevel, dormKitchenLevel;

    public HomeSave(int parDormLevel, int parDormGymLevel, int parDormKitchenLevel)
    {
        dormLevel = parDormLevel;
        dormGymLevel = parDormGymLevel;
        dormKitchenLevel = parDormKitchenLevel;
    }
}