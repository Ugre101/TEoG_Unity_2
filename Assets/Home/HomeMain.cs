using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HomeMain : MonoBehaviour
{
    private BasicChar Owner => PlayerHolder.Player;
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private GameObject dormGameobject = null, telePortMenu = null;
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private Button leaveBtn = null;
    [SerializeField] private Tilemap toMap = null, toPlatform = null;
    private readonly WorldMaps worldMaps = WorldMaps.StartMap;
    [SerializeField] private BuildButton buildButtonPrefab = null;
    [SerializeField] private Transform buildContainer = null;

    [Header("Build costs")]
    [SerializeField] private BuildCost mainHouse = new BuildCost();

    [SerializeField] private BuildCost dorm = new BuildCost(), kitchen = new BuildCost(), gym = new BuildCost();

    private void Start()
    {
        mapEvents = mapEvents != null ? mapEvents : MapEvents.GetMapEvents;
        leaveBtn.onClick.AddListener(LeaveHome);
        BuildButtons();
    }

    public void ToStart()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        transform.SleepChildren(transform.GetChild(0));
    }

    public void LeaveHome()
    {
        if (toPlatform == null)
        {
            mapEvents.Teleport(worldMaps, toMap);
        }
        else
        {
            mapEvents.Teleport(worldMaps, toMap, toPlatform);
        }
        GameManager.SetCurrentArea(GlobalArea.Map);
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
        Instantiate(buildButtonPrefab, buildContainer).Setup(buildCost, homeUpgrade, Owner, buildingName);
    }

    public void UpgradeMainHouse()
    {
    }

    // Enter & leave dorm a bit overkill for now but should make it easier in future
    public void EnterDorm()
    {
        dormGameobject.SetActive(true);
        canvasMain.HideGameUI();
        GameManager.SetCurState(GameState.InBuilding);
    }

    public void LeaveDorm()
    {
        dormGameobject.SetActive(false);
        GameManager.SetCurState(GameState.Free);
    }

    public void ToggleTeleportMenu()
    {
        telePortMenu.gameObject.SetActive(!telePortMenu.activeSelf);
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