using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Tilemap _fronTilemap = null, _toTilemap = null;

    private string playerTag;

    private void Start() => playerTag = PlayerMain.GetPlayer.tag;

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            bool direction = MapEvents.CurrentMap == _fronTilemap;
            MapEvents.MapChange(direction ? _toTilemap : _fronTilemap);
        }
    }
}