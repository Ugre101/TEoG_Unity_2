using UnityEngine;
using UnityEngine.Tilemaps;

public class CanTelePortTo : MonoBehaviour
{
    [SerializeField] private Tilemap map, landPlatform;
    [SerializeField] private WorldMaps worldMaps;

    public bool Know { get; private set; } = true;
    public Tilemap Map { get => map; private set => map = value; }
    public Tilemap LandPlatform { get => landPlatform; private set => landPlatform = value; }
    public WorldMaps WorldMaps { get => worldMaps; private set => worldMaps = value; }

    public void Load(bool know) => Know = know;

    private void Start()
    {
        Map = GetComponentInParent<Map>().gameObject.GetComponent<Tilemap>();
        LandPlatform = GetComponent<Tilemap>();
        WorldMaps = GetComponentInParent<WorldMap>().World;
    }
}