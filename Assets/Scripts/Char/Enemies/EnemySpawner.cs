using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerMain Player => PlayerMain.GetPlayer;

    [SerializeField]
    private List<Tilemap> dontSpawnOn = new List<Tilemap>();

    [SerializeField]
    private MapEvents MapEvents => MapEvents.GetMapEvents;

    [Header("Settings")]
    [SerializeField]
    private int enemyToAdd = 6;

    [Range(1, 15)]
    [SerializeField]
    private int distFromPlayer = 5;

    [Range(0, 10)]
    [SerializeField]
    private int distFromBorder = 2;

    // Private
    private readonly List<Vector3> _empty = new List<Vector3>();

    private readonly List<EnemyPrefab> _CurrEnemies = new List<EnemyPrefab>();
    private Tilemap _currMap;

    private void Start()
    {
        _currMap = MapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        CurrentEnemies();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_empty.Count < 1) { AvailblePos(); }
        else if (transform.childCount < enemyToAdd && _CurrEnemies.Count > 0)
        {
            int index = Random.Range(0, _empty.Count);
            int enemyIndex = Random.Range(0, _CurrEnemies.Count);
            EnemyPrefab enemu = Instantiate(_CurrEnemies[enemyIndex], _empty[index], Quaternion.identity, transform);
            enemu.name = _CurrEnemies[enemyIndex].name;
            _empty.RemoveAt(index);
        }
    }

    private void AvailblePos()
    {
        List<Vector3> aroundPlayer = AroundPlayer();
        for (int n = _currMap.cellBounds.xMin + distFromBorder; n < _currMap.cellBounds.xMax - distFromBorder; n++)
        {
            for (int p = _currMap.cellBounds.yMin + distFromBorder; p < _currMap.cellBounds.yMax - distFromBorder; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)_currMap.transform.position.z);
                if (_currMap.HasTile(localPlace) && !dontSpawnOn.Exists(t => t.HasTile(localPlace)) && !aroundPlayer.Contains(localPlace))
                {
                    Vector3 place = _currMap.CellToWorld(localPlace);
                    _empty.Add(place);
                }
            }
        }
    }

    public List<Vector3> AroundPlayer()
    {
        List<Vector3> around = new List<Vector3>();
        Vector3 playPos = Player.transform.position;
        for (int x = -distFromPlayer; x < distFromPlayer; x++)
        {
            for (int y = -distFromPlayer; y < distFromPlayer; y++)
            {
                around.Add(new Vector3(playPos.x + x, playPos.y + y, playPos.z));
            }
        }
        return around;
    }

    public void RePosistion(GameObject toRePos)
    {
        int index = Random.Range(0, _empty.Count);
        toRePos.transform.position = _empty[index];
        _empty.RemoveAt(index);
    }

    private void DoorChanged()
    {
        _currMap = MapEvents.CurrentMap;
        AvailblePos();
        CurrentEnemies();
        foreach (Transform e in transform)
        {
            Destroy(e.gameObject);
        }
        _empty.Clear();
    }

    private void CurrentEnemies()
    {
        _CurrEnemies.Clear();
        if (MapEvents.CurMapScript != null)
        {
            if (MapEvents.CurMapScript.Enemies.Count > 0)
            {
                _CurrEnemies.AddRange(MapEvents.CurMapScript.Enemies);
                enemyToAdd = MapEvents.CurMapScript.EnemyCount;
            }
        }

        #region old code

        /*
        switch (mapEvents.CurrentMap.name)
        {
            case "Ground-Room-Start":
                _CurrEnemies.AddRange(_startEnemies);
                break;

            case "Ground-Room-ToVillage":
                _CurrEnemies.AddRange(_roadTovillage);
                break;

            default:
                break;
        }
        */

        #endregion old code
    }
}