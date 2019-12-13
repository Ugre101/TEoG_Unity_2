﻿using System;
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
    private readonly TickManager tickManager;
    private readonly Home home;
    private readonly VoreChar voreChar;
    private PlayerSave save;

    public Save(PlayerMain player, Transform pos, Dorm theDorm, MapEvents map, TickManager manager, Home parHome, VoreChar parVoreChar)
    {
        Player = player;
        Pos = pos.transform;
        dorm = theDorm;
        mapEvents = map;
        tickManager = manager;
        home = parHome;
        voreChar = parVoreChar;
    }

    public string SaveData()
    {
        save = new PlayerSave(Player);
        List<DormSave> temp = dorm.Save();
        PosSave pos = new PosSave(Pos.position, mapEvents.ActiveMap, mapEvents.CurrentMap.transform.name);
        DateSave date = DateSystem.Save;
        HomeSave homeSave = home.Stats.Save();
        VoreSaves voreSaves = voreChar.Save();
        FullSave fullSave = new FullSave(save, pos, temp, date, homeSave, voreSaves);
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        mapEvents.Load(fullSave.posPart);
        JsonUtility.FromJsonOverwrite(fullSave.playerPart.who, Player);
        home.Stats.Load(fullSave.homePart);
        dorm.Load(fullSave.dormPart);
        voreChar.Load(fullSave.voreSaves, Player);
        DateSystem.Load(fullSave.datePart);
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

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, DateSave date, HomeSave parHome, VoreSaves vore)
    {
        playerPart = player;
        posPart = pos;
        dormPart = dorm;
        datePart = date;
        homePart = parHome;
        voreSaves = vore;
    }
}

[Serializable]
public class PlayerSave
{
    //public Vector3 pos;
    public string who;

    public PlayerSave(BasicChar whom)
    {
        who = JsonUtility.ToJson(whom);
    }
}

[Serializable]
public class PosSave
{
    public Vector3 pos;
    public WorldMaps world;
    public string map;

    public PosSave(Vector3 vec3, WorldMaps currWorld, string currMap)
    {
        pos = vec3;
        world = currWorld;
        map = currMap;
    }
}

[Serializable]
public class DateSave
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
public class HomeSave
{
    public int dormLevel, dormGymLevel, dormKitchenLevel;

    public HomeSave(int parDormLevel, int parDormGymLevel, int parDormKitchenLevel)
    {
        dormLevel = parDormLevel;
        dormGymLevel = parDormGymLevel;
        dormKitchenLevel = parDormKitchenLevel;
    }
}