using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrownHomeProperty : MonoBehaviour
{
    [SerializeField] private Tilemap lawn = null;
    [SerializeField] private Tile grassTile = null;
    public List<Vector3Int> AddedTiles { get; private set; } = new List<Vector3Int>();

    // Start is called before the first frame update
    private void Start()
    {
        lawn = lawn != null ? lawn : GetComponent<Tilemap>();

        for (int i = 0; i < 10; i++)
        {
            GrowLawn();
        }
        ClearLawn();
    }

    private void OnEnable()
    {
    }

    public void GrowLawn()
    {
        int ymax = lawn.cellBounds.yMax;
        for (int x = lawn.cellBounds.xMin; x < lawn.cellBounds.xMax; x++)
        {
            Vector3Int localPlace = new Vector3Int(x, ymax, (int)lawn.transform.position.z);
            lawn.SetTile(localPlace, grassTile);
            AddedTiles.Add(localPlace);
        }
        int xMin = lawn.cellBounds.xMin - 1;
        for (int y = lawn.cellBounds.yMin; y < lawn.cellBounds.yMax; y++)
        {
            Vector3Int localPlace = new Vector3Int(xMin, y, (int)lawn.transform.position.z);
            lawn.SetTile(localPlace, grassTile);
            AddedTiles.Add(localPlace);
        }
    }

    public void ClearLawn()
    {
        foreach (Vector3Int tile in AddedTiles)
        {
            lawn.SetTile(tile, null);
        }
        lawn.CompressBounds();
        AddedTiles.Clear();
    }
}