using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HomeMain : MonoBehaviour
{
    [SerializeField] private BasicChar owner = null;
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private GameObject dormGameobject = null;
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private Button leaveBtn = null;
    [SerializeField] private Tilemap toMap = null;
    [SerializeField] private Tilemap toPlatform = null;
    private readonly WorldMaps worldMaps = WorldMaps.StartMap;
    [SerializeField] private BuildButton buildButtonPrefab = null;
    [SerializeField] private Transform buildContainer = null;

    [Header("Build costs")]
    [SerializeField] private BuildCost mainHouse = new BuildCost();

    [SerializeField] private BuildCost dorm = new BuildCost(), kitchen = new BuildCost(), gym = new BuildCost();

    private void Start()
    {
        mapEvents = mapEvents != null ? mapEvents : MapEvents.GetMapEvents;
        owner = owner != null ? owner : PlayerMain.GetPlayer;
        leaveBtn.onClick.AddListener(LeaveHome);
        BuildButtons();
    }

    public void ToStart()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        transform.SleepChildren(transform.GetChild(0));
    }

    private void LeaveHome()
    {
        if (toPlatform == null)
        {
            mapEvents.Teleport(worldMaps, toMap);
        }
        else
        {
            mapEvents.Teleport(worldMaps, toMap, toPlatform);
        }
        canvasMain.Resume();
    }

    private void BuildButtons()
    {
        buildContainer.KillChildren();
        FirstPart(mainHouse, StartHomeStats.MainHouse, "Main house");
        FirstPart(dorm, StartHomeStats.Dorm, "Dorm");
        FirstPart(kitchen, StartHomeStats.Kitchen, "Kitchen");
        FirstPart(gym, StartHomeStats.Gym, "Gym");
    }

    private void FirstPart(BuildCost buildCost, HomeUpgrade homeUpgrade, string buildingName)
    {
        Instantiate(buildButtonPrefab, buildContainer).Setup(buildCost, homeUpgrade, owner, buildingName);
    }

    public void UpgradeMainHouse()
    {
    }

    // Enter & leave dorm a bit overkill for now but should make it easier in future
    public void EnterDorm()
    {
        dormGameobject.SetActive(true);
        GameManager.CurState = GameState.InBuilding;
    }

    public void LeaveDorm()
    {
        dormGameobject.SetActive(false);
        GameManager.CurState = GameState.Free;
    }
}

[System.Serializable]
public class BuildCost
{
    [SerializeField] private int baseCost = 30;
    [SerializeField] private float exponent = 1.2f;

    public int GetCost(int curLevel)
    {
        float cost = baseCost;
        for (int i = 0; i < curLevel; i++)
        {
            cost = Mathf.Pow(cost, exponent);
        }
        return Mathf.FloorToInt(cost);
    }
}