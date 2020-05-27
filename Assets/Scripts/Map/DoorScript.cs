using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool autuAssingIfEmpty = true;
    [SerializeField] private Tilemap _fronTilemap = null, _toTilemap = null;

    private string playerTag;

    public Tilemap FronTilemap { get => _fronTilemap; set => _fronTilemap = value; }
    public Tilemap ToTilemap { get => _toTilemap; set => _toTilemap = value; }
    public bool AutuAssingIfEmpty => autuAssingIfEmpty;

    private void Start() => playerTag = PlayerMain.GetPlayer.tag;

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            bool direction = MapEvents.CurrentMap == FronTilemap;
            MapEvents.MapChange(direction ? ToTilemap : FronTilemap);
        }
    }
}