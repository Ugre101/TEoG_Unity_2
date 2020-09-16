using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(DoorScript))]
public class BetterDoorEditor : Editor
{
    private DoorScript door;

    private void OnEnable()
    {
        door = (DoorScript)target;
        if (door.AutuAssingIfEmpty)
        {
            if (door.transform.parent == null)
            {
                Debug.Log("No parent");
            }
            else if (door.GetComponentInParent<WorldMap>() is WorldMap world)
            {
                int doorIndex = door.transform.GetSiblingIndex();
                if (door.FromTilemap == null)
                {
                    if (world.transform.GetChild(doorIndex - 1).gameObject.GetComponent<Map>() is Map map && map.GetComponent<Tilemap>() is Tilemap tilemap)
                    {
                        door.FromTilemap = tilemap;
                        EditorUtility.SetDirty(door);
                        AssetDatabase.SaveAssets();
                    }
                }
                if (door.ToTilemap == null)
                {
                    if (world.transform.GetChild(doorIndex + 1).GetComponent<Map>() is Map map && map.GetComponent<Tilemap>() is Tilemap tilemap)
                    {
                        door.ToTilemap = tilemap;
                        EditorUtility.SetDirty(door);
                        AssetDatabase.SaveAssets();
                    }
                }
            }
        }
    }

    private void AutoPos()
    {
        Vector3 fromCenterPos = door.FromTilemap.cellBounds.center, toCenterPos = door.ToTilemap.cellBounds.center;
        Vector3Int fromInt = Vector3Int.FloorToInt(fromCenterPos), toInt = Vector3Int.FloorToInt(toCenterPos);
        Debug.Log(door.FromTilemap.CellToWorld(fromInt) - door.ToTilemap.CellToWorld(toInt));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}