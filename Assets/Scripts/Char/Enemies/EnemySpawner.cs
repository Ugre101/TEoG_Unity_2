using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public List<Tilemap> dontSpawnOn;
    public MapEvents mapEvents;

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
    private List<Vector3> _empty = new List<Vector3>();

    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _CurrEnemies = new List<GameObject>();
    private Tilemap _currMap;

    private void Start()
    {
        _currMap = mapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        CurrentEnemies();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_empty.Count < 1)
        {
            AvailblePos();
        }
        else if (_enemies.Count < enemyToAdd && _CurrEnemies.Count > 0)
        {
            int index = Random.Range(0, _empty.Count);
            int enemyIndex = Random.Range(0, _CurrEnemies.Count - 1);
            GameObject enemu = Instantiate(_CurrEnemies[enemyIndex], _empty[index], Quaternion.identity, transform);
            enemu.name = _CurrEnemies[enemyIndex].name;
            _enemies.Add(enemu);
            _empty.RemoveAt(index);
        }
    }

    private void AvailblePos()
    {
        for (int n = _currMap.cellBounds.xMin + 2; n < _currMap.cellBounds.xMax - 2; n++)
        {
            for (int p = _currMap.cellBounds.yMin + 2; p < _currMap.cellBounds.yMax - 2; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)_currMap.transform.position.y);
                if (_currMap.HasTile(localPlace) && !dontSpawnOn.Exists(t => t.HasTile(localPlace)) && !AroundPlayer().Contains(localPlace))
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
        Vector3 playPos = player.transform.position;
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
        _currMap = mapEvents.CurrentMap;
        AvailblePos();
        CurrentEnemies();
        foreach (GameObject e in _enemies)
        {
            Destroy(e);
        }
        _empty.Clear();
        _enemies.Clear();
    }

    private void CurrentEnemies()
    {
        _CurrEnemies.Clear();
        _CurrEnemies.AddRange(mapEvents.CurMapScript.Enemies);
        enemyToAdd = mapEvents.CurMapScript.EnemyCount;

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