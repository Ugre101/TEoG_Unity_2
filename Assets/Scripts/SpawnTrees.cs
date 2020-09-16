using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTrees : MonoBehaviour
{
    [SerializeField] private List<Tile> spawnOn = new List<Tile>();
    [SerializeField] private Tilemap map = null;

    [Range(0, 10)] [SerializeField] private int distFromBorder = 1;

    [Range(0, 200)] [SerializeField] private int maxAmountTrees = 50;

    [Header("Percentage change of spawning a tree on empty spot")] [Range(0f, 0.1f)] [SerializeField]
    private float spawnChance = 0.1f;

    [Range(1f, 10f)] [SerializeField] private float distFromOtherTress = 3f;

    private readonly List<Vector3> spwanSpot = new List<Vector3>();
    private List<SmartTree> smartTrees = new List<SmartTree>();

    // Start is called before the first frame update
    private void Start()
    {
        map = map != null ? map : GetComponent<Tilemap>();
        GetSpawnTiles();
        Spawn();
        DateSystem.NewWeekEvent += ReCount;
        DateSystem.NewWeekEvent += Spawn;
    }

    private void ReCount() => smartTrees = GetComponentsInChildren<SmartTree>().ToList();

    private void GetSpawnTiles()
    {
        for (int n = map.cellBounds.xMin + distFromBorder; n < map.cellBounds.xMax - distFromBorder; n++)
        {
            for (int p = map.cellBounds.yMin + distFromBorder; p < map.cellBounds.yMax - distFromBorder; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int) map.transform.position.z);
                if (map.HasTile(localPlace))
                {
                    Tile tileBase = map.GetTile<Tile>(localPlace);
                    if (tileBase != null)
                    {
                        if (spawnOn.Any(tile => tileBase.sprite == tile.sprite))
                        {
                            spwanSpot.Add(localPlace);
                        }
                    }
                }
            }
        }
    }

    private readonly System.Random rnd = new System.Random();

    private void Spawn()
    {
        if (UnderMaxAmount())
        {
            foreach (Vector3 t in spwanSpot)
            {
                if (UnderMaxAmount())
                {
                    Vector3 spot = spwanSpot[rnd.Next(spwanSpot.Count)];
                    if (NotToCloseToOtherTrees(spot) && Random.value < spawnChance)
                    {
                        SmartTree tree = SmartTreeObjectPool.Instance.GetTree();
                        tree.gameObject.SetActive(true);
                        Transform treeTransform = tree.transform;
                        treeTransform.SetParent(transform);
                        treeTransform.position = spot;
                        smartTrees.Add(tree);
                    }
                }
                else
                    break;
            }
        }

        bool UnderMaxAmount() => smartTrees.Count < maxAmountTrees;
    }

    private bool NotToCloseToOtherTrees(Vector3 spot) 
        => !(from tree in smartTrees let treeSpot = map.LocalToWorld(spot) where Vector3.Distance(tree.transform.position, treeSpot) < distFromOtherTress select tree).Any();
}