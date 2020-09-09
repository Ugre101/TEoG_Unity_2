using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool autuAssingIfEmpty = true;
    [SerializeField] private Tilemap _fronTilemap = null, _toTilemap = null;

    private string playerTag;

    public Tilemap FromTilemap { get => _fronTilemap; set => _fronTilemap = value; }
    public Tilemap ToTilemap { get => _toTilemap; set => _toTilemap = value; }
    public bool AutuAssingIfEmpty => autuAssingIfEmpty;

    private void Start() => playerTag = PlayerSprite.Tag;

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;
        
        bool direction = MapEvents.CurrentMap == FromTilemap;
        MapEvents.MapChange(direction ? ToTilemap : FromTilemap);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }
}