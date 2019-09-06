using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("Start area")]
    public List<GameObject> _startEnemies;
    [Header("Road to village")]
    public List<GameObject> _roadTovillage;
    [Space]
    public Tilemap _trees;
    public MapEvents mapEvents;

    // Private
    private List<Vector3> _empty = new List<Vector3>();

    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _CurrEnemies = new List<GameObject>();
    private Tilemap _currMap;
    private int _enemyToAdd;

    private void Start()
    {
        _enemyToAdd = 6;
        _currMap = mapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        _CurrEnemies.AddRange(_startEnemies);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_empty.Count < 1)
        {
            AvailblePos();
        }
        else if (_enemies.Count < _enemyToAdd && _CurrEnemies.Count > 0)
        {
            int index = Random.Range(0, _empty.Count);
            int enemyIndex = Random.Range(0, _CurrEnemies.Count - 1);
            GameObject enemu = Instantiate(_CurrEnemies[enemyIndex], _empty[index], Quaternion.identity, this.transform);
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
                if (_currMap.HasTile(localPlace) && !_trees.HasTile(localPlace))
                {
                    Vector3 place = _currMap.CellToWorld(localPlace);
                    _empty.Add(place);
                }
            }
        }
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
    }
}