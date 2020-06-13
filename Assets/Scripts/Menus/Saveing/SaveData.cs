using System.Collections.Generic;
using UnityEngine;

public class Save
{
    private readonly PlayerMain Player;
    private readonly Transform Pos;

    public Save(PlayerMain player, PlayerHolder playerHolder)
    {
        Player = player;
        Pos = playerHolder.transform;
    }

    public string SaveData()
    {
        PlayerSave playerSave = new PlayerSave(Player);
        List<DormSave> dormSaves = Dorm.Save();
        PosSave playerPos = new PosSave(Pos.position);
        HomeSave homeSave = StartHomeStats.Save();
        FullSave fullSave = new FullSave(playerSave, playerPos, dormSaves, homeSave, MapEvents.GetMapEvents.GetTeleportSaves());
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        Debug.Log(json);
        string errorMsg = string.Empty;
        // Singleton static
        // Reference
        try
        {
            PlayerHolder.GetPlayerHolder.Load(fullSave.PlayerPart.Who);
        }
        catch
        {
            errorMsg += "Player failed to load";
        }
        //  JsonUtility.FromJsonOverwrite(fullSave.PlayerPart.Who, Player);
        try
        {
            StartHomeStats.Load(fullSave.HomePart);
        }
        catch
        {
            errorMsg += "Home failed to load";
        }
        try
        {
            Dorm.Load(fullSave.DormPart);
        }
        catch
        {
            errorMsg += "Dorm failed to load";
        }

        DateSystem.Load(fullSave.DatePart);
        QuestsSystem.Load(fullSave.QuestSave);
        PlayerFlags.Load(fullSave.PlayerFlagsSave);
        MapEvents.GetMapEvents.Load(fullSave.PosPart, fullSave.TeleportSaves);
        GameManager.Load(fullSave.GameManagerSave);
        EventLog.ClearLog();
        LoadEvent?.Invoke();
        if (errorMsg != string.Empty)
        {
            PopupHandler.GetPopupHandler.DelayedSpawnTimedPopup(errorMsg, 6f);
        }
    }

    public delegate void DelegateLoad();

    public static event DelegateLoad LoadEvent;
}

[System.Serializable]
public class FullSave
{
    [SerializeField] private PlayerSave playerPart;
    [SerializeField] private PosSave posPart;
    [SerializeField] private List<DormSave> dormPart;
    [SerializeField] private DateSave datePart;
    [SerializeField] private HomeSave homePart;

    [SerializeField] private QuestSave questSave;

    [SerializeField] private PlayerFlagsSave playerFlags;
    [SerializeField] private List<TeleportSave> teleportSaves;
    [SerializeField] private GameManagerSaveState gameManager;
    public PlayerSave PlayerPart => playerPart;
    public PosSave PosPart => posPart;
    public List<DormSave> DormPart => dormPart;
    public DateSave DatePart => datePart;
    public HomeSave HomePart => homePart;

    public QuestSave QuestSave => questSave;

    public PlayerFlagsSave PlayerFlagsSave => playerFlags;
    public List<TeleportSave> TeleportSaves => teleportSaves;

    public GameManagerSaveState GameManagerSave => gameManager;

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, HomeSave parHome, List<TeleportSave> teleportSaves)
    {
        this.playerPart = player;
        this.posPart = pos;
        this.dormPart = dorm;
        this.datePart = DateSystem.Save;
        this.homePart = parHome;
        this.questSave = QuestsSystem.Save;
        this.playerFlags = PlayerFlags.Save();
        this.teleportSaves = teleportSaves;
        this.gameManager = GameManager.Save();
    }
}

[System.Serializable]
public struct PlayerSave
{
    [SerializeField] private string who;

    public PlayerSave(BasicChar whom) => who = JsonUtility.ToJson(whom);

    public string Who => who;
}

[System.Serializable]
public struct PosSave
{
    [SerializeField] private Vector3 pos;

    [SerializeField] private WorldMaps world;

    [SerializeField] private string map;

    public Vector3 Pos => pos;
    public WorldMaps World => world;
    public string Map => map;

    public PosSave(Vector3 vec3)
    {
        pos = vec3;
        world = MapEvents.ActiveMap;
        map = MapEvents.CurrentMap.transform.name;
    }
}