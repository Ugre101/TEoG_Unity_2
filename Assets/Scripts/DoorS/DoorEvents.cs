using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorEvents : MonoBehaviour
{
    public Tilemap _currentMap;

    public delegate void ChangeMap();

    public static event ChangeMap changeMap;
    public void MapChange(Tilemap newMap)
    {
        _currentMap = newMap;
        changeMap();
    }
}