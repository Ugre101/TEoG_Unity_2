using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _startEnemy;
    public Tilemap _trees;
    public DoorEvents _doorEvent;

    // Private
    private List<Vector3> _empty = new List<Vector3>();

    private List<GameObject> _enemies = new List<GameObject>();
    private Tilemap _currMap;
    private Transform[] _spawnPoints;

    private void Start()
    {
        _currMap = _doorEvent._currentMap;
        DoorEvents.changeMap += DoorChanged;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_empty.Count < 1)
        {
            AvailblePos();
        }
        else if (_enemies.Count < 6)
        {
            int index = Random.Range(0, _empty.Count);
            GameObject enemu = Instantiate(_startEnemy, _empty[index], Quaternion.identity);
            enemu.transform.parent = gameObject.transform;
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
        _currMap = _doorEvent._currentMap;
        foreach (GameObject e in _enemies)
        {
            Destroy(e);
        }
        _empty.Clear();
        _enemies.Clear();
    }
}