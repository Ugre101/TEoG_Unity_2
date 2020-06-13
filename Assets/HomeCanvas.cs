using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeCanvas : MonoBehaviour
{
    public static HomeCanvas GetHomeCanvas { get; private set; }
    [SerializeField] private HomeMain homeUI = null;

    private void Awake()
    {
        if (GetHomeCanvas == null)
        {
            GetHomeCanvas = this;
        }
        else
        {
            Debug.LogError("You have one or more homeCanvas");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        homeUI = homeUI != null ? homeUI : GetComponentInChildren<HomeMain>();
        GameManager.GloablAreaChange += EnableHomeUI;
        EnableHomeUI(GameManager.CurrentArea);
    }

    private bool homeUIisActive = true;

    private void EnableHomeUI(GlobalArea newArea)
    {
        if (newArea == GlobalArea.Home)
        {
            homeUI.gameObject.SetActive(true);
            homeUIisActive = true;
        }
        else if (homeUIisActive)
        {
            homeUI.gameObject.SetActive(false);
            homeUIisActive = false;
        }
    }

    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private HomeMapHandler homeMapHandler = null;
    [SerializeField] private Tilemap homeLandPlatform = null;

    public void EnterHome()
    {
        GameManager.SetCurState(GameState.Free);
        GameManager.SetCurrentArea(GlobalArea.Home);
        if (homeLandPlatform == null)
        {
            mapEvents.Teleport(WorldMaps.Home, homeMapHandler.GetActiveLawn);
        }
        else
        {
            mapEvents.Teleport(WorldMaps.Home, homeMapHandler.GetActiveLawn, homeLandPlatform);
        }
    }
}