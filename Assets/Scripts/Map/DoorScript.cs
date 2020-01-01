using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private Tilemap _fronTilemap = null, _toTilemap = null;

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (MapEvents.CurrentMap == _fronTilemap)
            {
                MapEvents.MapChange(_toTilemap);
            }
            else
            {
                MapEvents.MapChange(_fronTilemap);
            }
        }
    }
}