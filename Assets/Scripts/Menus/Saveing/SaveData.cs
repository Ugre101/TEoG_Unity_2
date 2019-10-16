using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    private readonly playerMain Player;
    private readonly Transform Pos;
    private readonly Dorm dorm;
    private readonly MapEvents mapEvents;
    private readonly TickManager tickManager;
    private PlayerSave save;

    public Save(playerMain player, Transform pos, Dorm theDorm, MapEvents map, TickManager manager)
    {
        Player = player;
        Pos = pos.transform;
        dorm = theDorm;
        mapEvents = map;
        tickManager = manager;
    }

    public string SaveData()
    {
        save = new PlayerSave(Player);
        List<DormSave> temp = new List<DormSave>();
        foreach (Transform child in dorm.transform)
        {
            DormSave tempDorm = new DormSave(child.name, child.GetComponent<BasicChar>());
            temp.Add(tempDorm);
        }
        PosSave pos = new PosSave(Pos.position, mapEvents.ActiveMap, mapEvents.CurrentMap.transform.name);
        DateSave date = tickManager.Save();
        FullSave fullSave = new FullSave(save, pos, temp, date);
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        mapEvents.Load(fullSave.posPart);
        // Pos.position = fullSave.posPart.pos;
        JsonUtility.FromJsonOverwrite(fullSave.playerPart.who, Player);
        foreach (Transform child in dorm.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (DormSave dormSave in fullSave.dormPart)
        {
            dorm.Load(dormSave);
        }
    }
}

[Serializable]
public class FullSave
{
    public PlayerSave playerPart;
    public PosSave posPart;
    public List<DormSave> dormPart;
    public DateSave datePart;

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, DateSave date)
    {
        playerPart = player;
        posPart = pos;
        dormPart = dorm;
        datePart = date;
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
public class DormSave
{
    public string name;
    public string who;

    public DormSave(string Name, BasicChar Who)
    {
        name = Name;
        who = JsonUtility.ToJson(Who);
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