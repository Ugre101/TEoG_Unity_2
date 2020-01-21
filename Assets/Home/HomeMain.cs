using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HomeMain : MonoBehaviour
{
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private GameObject HouseStart = null;
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private Button leaveBtn = null;
    [SerializeField] private Tilemap toMap = null;
    [SerializeField] private Tilemap toPlatform = null;
    private readonly WorldMaps worldMaps = WorldMaps.StartMap;

    private void Start()
    {
        mapEvents = mapEvents != null ? mapEvents : MapEvents.GetMapEvents;
        leaveBtn.onClick.AddListener(LeaveHome);
    }

    public void ToStart()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        transform.SleepChildren(HouseStart.transform);
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
}