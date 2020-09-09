using SaveStuff;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public static string SaveData()
    {
        PlayerSave playerSave = new PlayerSave(PlayerMain.Player);
        List<DormSave> dormSaves = Dorm.Save();
        PosSave playerPos = new PosSave(PlayerSprite.Instance.transform.position);
        HomeSave homeSave = DormUpgrades.Save();
        FullSave fullSave = new FullSave(playerSave, playerPos, dormSaves, homeSave, MapEvents.GetMapEvents.GetTeleportSaves());
        //    Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public static void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);

        if (Application.isEditor)
        {
            Debug.Log(json);
        }




        //  Debug.Log($"Length: {json.Length} Json string: {json}");
        string errorMsg = string.Empty;
        // Singleton static
        // Reference
        try
        {
            //   PlayerHolder.Player.Load(fullSave.PlayerPart);
            PlayerMain.Load(fullSave.PlayerSave);
        }
        catch
        {
            errorMsg += "Player failed to load";
        }
        //  JsonUtility.FromJsonOverwrite(fullSave.PlayerPart.Who, Player);
        try
        {
            DormUpgrades.Load(fullSave.HomeSave);
        }
        catch
        {
            errorMsg += "Home failed to load";
        }
        try
        {
            Dorm.Load(fullSave.DormSave);
        }
        catch
        {
            errorMsg += "Dorm failed to load";
        }

        DateSystem.Load(fullSave.DateSave);
        QuestsSystem.Load(fullSave.QuestSave);
        PlayerFlags.Load(fullSave.PlayerFlagsSave);
        MapEvents.GetMapEvents.Load(fullSave.PosistionSave, fullSave.TeleportSaves);
        GameManager.Load(fullSave.GameManagerSave);
        EventLog.ClearLog();
        LoadEvent?.Invoke();
        if (errorMsg == string.Empty) return;
        PopupHandler.GetPopupHandler.DelayedSpawnTimedPopup(errorMsg, 6f);
        Debug.LogWarning(errorMsg);
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
    public PlayerSave PlayerSave => playerPart;
    public PosSave PosistionSave => posPart;
    public List<DormSave> DormSave => dormPart;
    public DateSave DateSave => datePart;
    public HomeSave HomeSave => homePart;

    public QuestSave QuestSave => questSave;

    public PlayerFlagsSave PlayerFlagsSave => playerFlags;
    public List<TeleportSave> TeleportSaves => teleportSaves;

    public GameManagerSaveState GameManagerSave => gameManager;

    public FullSave(PlayerSave playerSave, PosSave posSave, List<DormSave> dormSave, HomeSave homeSave, List<TeleportSave> teleportSaves)
    {
        this.playerPart = playerSave;
        this.posPart = posSave;
        this.dormPart = dormSave;
        this.datePart = DateSystem.Save;
        this.homePart = homeSave;
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

[System.Serializable]
public struct BasicCharCustomSave
{
    [SerializeField] private IdentitySave identitySave;
    [SerializeField] private ReletionShipSave reletionShip;

    public BasicCharCustomSave(IdentitySave identitySave, ReletionShipSave reletionShip)
    {
        this.identitySave = identitySave;
        this.reletionShip = reletionShip;
    }

    public IdentitySave IdentitySave => identitySave;
    public ReletionShipSave? ReletionShip => reletionShip;
}