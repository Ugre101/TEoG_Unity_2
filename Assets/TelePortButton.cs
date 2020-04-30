using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TelePortButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private MapEvents mapEvents;
    private WorldMaps world;
    private Tilemap map, landPlatform = null;

    public void Setup(MapEvents mapEvents, WorldMaps world, Tilemap map)
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btnText = btnText != null ? btnText : GetComponentInChildren<TextMeshProUGUI>();
        this.mapEvents = mapEvents;
        this.world = world;
        this.map = map;
        btn.onClick.AddListener(TeleportTo);
        btnText.text = $"World: {world}\nMap: {map}";
    }

    public void Setup(MapEvents mapEvents, WorldMaps world, Tilemap map, Tilemap landPlatform)
    {
        Setup(mapEvents, world, map);
        this.landPlatform = landPlatform;
    }

    private void TeleportTo()
    {
        if (landPlatform == null)
        {
            mapEvents.Teleport(world, map);
        }
        else
        {
            mapEvents.Teleport(world, map, landPlatform);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}