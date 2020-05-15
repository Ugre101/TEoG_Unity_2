using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTrees : MonoBehaviour
{
    [SerializeField] private SmartTree smartTree = null;
    [SerializeField] private List<Tile> spawnOn = new List<Tile>();
    [SerializeField] private Tilemap map = null;
    [SerializeField] private int distFromBorder = 1;

    [Header("Precentage change of spawning a tree on empty spot")]
    [Range(0f, 0.1f)]
    [SerializeField] private float spawnChance = 0.1f;

    [Range(1f, 10f)]
    [SerializeField] private float distFromOtherTress = 3f;

    private List<Vector3> emptySpots = new List<Vector3>();
    private List<SmartTree> smartTrees = new List<SmartTree>();

    // Start is called before the first frame update
    private void Start()
    {
        map = map != null ? map : GetComponent<Tilemap>();
        GetSpawnTiles();
        Spawn();
        DateSystem.NewWeekEvent += Spawn;
    }

    private void GetSpawnTiles()
    {
        for (int n = map.cellBounds.xMin + distFromBorder; n < map.cellBounds.xMax - distFromBorder; n++)
        {
            for (int p = map.cellBounds.yMin + distFromBorder; p < map.cellBounds.yMax - distFromBorder; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)map.transform.position.z);
                if (map.HasTile(localPlace))
                {
                    Tile tileBase = map.GetTile<Tile>(localPlace);
                    if (tileBase != null)
                    {
                        foreach (Tile tile in spawnOn)
                        {
                            if (tileBase.sprite == tile.sprite)
                            {
                                emptySpots.Add(localPlace);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void Spawn()
    {
        foreach (Vector3 spot in emptySpots)
        {
            if (NotToCloseToOtherTrees(spot))
            {
                Debug.Log(Random.value < spawnChance);
                if (Random.value < spawnChance)
                {
                    smartTrees.Add(Instantiate(smartTree, spot, Quaternion.identity, transform));
                }
            }
        }
    }

    private bool NotToCloseToOtherTrees(Vector3 spot)
    {
        foreach (SmartTree tree in smartTrees)
        {
            Vector3 treeSpot = map.LocalToWorld(spot);
            if (Vector3.Distance(tree.transform.position, treeSpot) < distFromOtherTress)
            {
                return false;
            }
        }
        return true;
    }
}