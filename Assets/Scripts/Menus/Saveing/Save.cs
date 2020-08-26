using SaveStuff;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public string SaveData()
    {
        PlayerSave playerSave = new PlayerSave(PlayerMain.Player);
        List<DormSave> dormSaves = Dorm.Save();
        PosSave playerPos = new PosSave(PlayerSprite.Instance.transform.position);
        HomeSave homeSave = DormUpgrades.Save();
        FullSave fullSave = new FullSave(playerSave, playerPos, dormSaves, homeSave, MapEvents.GetMapEvents.GetTeleportSaves());
        //    Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        //  Debug.Log($"Length: {json.Length} Json string: {json}");
        string errorMsg = string.Empty;
        // Singleton static
        // Reference
        try
        {
            //   PlayerHolder.Player.Load(fullSave.PlayerPart);
            PlayerMain.Load(fullSave.PlayerPart);
        }
        catch
        {
            errorMsg += "Player failed to load";
        }
        //  JsonUtility.FromJsonOverwrite(fullSave.PlayerPart.Who, Player);
        try
        {
            DormUpgrades.Load(fullSave.HomePart);
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
    [SerializeField] private PlayerSave playerSave;
    [SerializeField] private PosSave posSave;
    [SerializeField] private List<DormSave> dormSave;
    [SerializeField] private DateSave dateSave;
    [SerializeField] private HomeSave homeSave;

    [SerializeField] private QuestSave questSave;

    [SerializeField] private PlayerFlagsSave playerFlagsSave;
    [SerializeField] private List<TeleportSave> teleportSaves;
    [SerializeField] private GameManagerSaveState gameManagerSave;
    public PlayerSave PlayerPart => playerSave;
    public PosSave PosPart => posSave;
    public List<DormSave> DormPart => dormSave;
    public DateSave DatePart => dateSave;
    public HomeSave HomePart => homeSave;

    public QuestSave QuestSave => questSave;

    public PlayerFlagsSave PlayerFlagsSave => playerFlagsSave;
    public List<TeleportSave> TeleportSaves => teleportSaves;

    public GameManagerSaveState GameManagerSave => gameManagerSave;

    public FullSave(PlayerSave playerSave, PosSave posSave, List<DormSave> dormSave, HomeSave homeSave, List<TeleportSave> teleportSaves)
    {
        this.playerSave = playerSave;
        this.posSave = posSave;
        this.dormSave = dormSave;
        this.dateSave = DateSystem.Save;
        this.homeSave = homeSave;
        this.questSave = QuestsSystem.Save;
        this.playerFlagsSave = PlayerFlags.Save();
        this.teleportSaves = teleportSaves;
        this.gameManagerSave = GameManager.Save();
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