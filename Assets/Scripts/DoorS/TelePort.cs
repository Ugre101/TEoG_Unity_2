using UnityEngine;
using UnityEngine.Tilemaps;
public class TelePort : MonoBehaviour
{
    public GameObject fromWorld, toWorld;
    public Tilemap toMap;
    public MapEvents mapEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mapEvents.Teleport(fromWorld, toWorld, toMap);
    }
}
