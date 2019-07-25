using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    public DoorEvents _doorEvent;
    public Tilemap _fronTilemap, _toTilemap;

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_doorEvent._currentMap == _fronTilemap)
            {
                _doorEvent.MapChange(_toTilemap);
            } else
            {
                _doorEvent.MapChange(_fronTilemap);
            }
        }
    }
} 
