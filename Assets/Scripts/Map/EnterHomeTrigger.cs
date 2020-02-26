using UnityEngine;
using UnityEngine.Tilemaps;

public class EnterHomeTrigger : MonoBehaviour
{
    [SerializeField] private CanvasMain GameUI = null;
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private HomeMapHandler homeMapHandler = null;
    private Tilemap toMap, landPlatform;
    private readonly WorldMaps worldMaps = WorldMaps.Home;

    private void Start()
    {
        GameUI = GameUI != null ? GameUI : CanvasMain.GetCanvasMain;
        mapEvents = mapEvents != null ? mapEvents : MapEvents.GetMapEvents;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerMain.GetPlayer.tag))
        {
            toMap = homeMapHandler.GetActiveLawn;
            GameUI.EnterHome();
            if (landPlatform == null)
            {
                mapEvents.Teleport(worldMaps, toMap);
            }
            else
            {
                mapEvents.Teleport(worldMaps, toMap, landPlatform);
            }
        }
    }
}