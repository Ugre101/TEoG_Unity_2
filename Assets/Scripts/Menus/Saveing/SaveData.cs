using System.Collections.Generic;
using UnityEngine;
using Vore;

[System.Serializable]
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
        HomeSave homeSave = StartHomeStats.Save();
        VoreSaves voreSaves = voreChar.Save;
        FullSave fullSave = new FullSave(playerSave, playerPos, dormSaves, homeSave, voreSaves, MapEvents.GetMapEvents.GetTeleportSaves());
        Debug.Log(JsonUtility.ToJson(fullSave));

        return JsonUtility.ToJson(fullSave);
    }

    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        // Singleton static
        // Reference
        JsonUtility.FromJsonOverwrite(fullSave.PlayerPart.Who, Player);
        StartHomeStats.Load(fullSave.HomePart);
        dorm.Load(fullSave.DormPart);
        voreChar.Load(fullSave.VoreSaves, Player);
        // Pure static
        DateSystem.Load(fullSave.DatePart);
        QuestsSystem.Load(fullSave.QuestSave);
        PlayerFlags.Load(fullSave.PlayerFlagsSave);
        MapEvents.GetMapEvents.Load(fullSave.PosPart, fullSave.TeleportSaves);
        GameManager.Load(fullSave.GameManagerSave);
        EventLog.ClearLog();
        LoadEvent?.Invoke();
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
    [SerializeField] private VoreSaves voreSaves;
    [SerializeField] private QuestSave questSave;
    [SerializeField] private PlayerFlagsSave playerFlags;
    [SerializeField] private List<TeleportSave> teleportSaves;
    [SerializeField] private GameManagerSaveState gameManager;
    public PlayerSave PlayerPart => playerPart;
    public PosSave PosPart => posPart;
    public List<DormSave> DormPart => dormPart;
    public DateSave DatePart => datePart;
    public HomeSave HomePart => homePart;
    public VoreSaves VoreSaves => voreSaves;
    public QuestSave QuestSave => questSave;
    public PlayerFlagsSave PlayerFlagsSave => playerFlags;
    public List<TeleportSave> TeleportSaves => teleportSaves;

    public GameManagerSaveState GameManagerSave => gameManager;

    public FullSave(PlayerSave player, PosSave pos, List<DormSave> dorm, HomeSave parHome, VoreSaves vore, List<TeleportSave> teleportSaves)
    {
        this.playerPart = player;
        this.posPart = pos;
        this.dormPart = dorm;
        this.datePart = DateSystem.Save;
        this.homePart = parHome;
        this.voreSaves = vore;
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

    public PosSave(Vector3 vec3, WorldMaps currWorld, string currMap)
    {
        pos = vec3;
        world = currWorld;
        map = currMap;
    }
}