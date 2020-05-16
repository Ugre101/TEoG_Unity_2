using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlayerMain Player;

    [SerializeField] private List<Tilemap> dontSpawnOn = new List<Tilemap>();

    private MapEvents MapEvents => MapEvents.GetMapEvents;

    [Header("Settings")]
    [Range(0, 50)]
    [SerializeField] private int distFromPlayer = 5, distFromBorder = 2, distFromBoss = 10;

    // Private
    private int enemyToAdd = 6;

    private Tilemap _currMap;

    private readonly List<Vector3> _empty = new List<Vector3>();
    private readonly List<EnemyPrefab> currEnemies = new List<EnemyPrefab>();
    private readonly List<Boss> currBosses = new List<Boss>();
    private readonly List<Boss> addedBosses = new List<Boss>();
    private readonly System.Random rnd = new System.Random();

    private void Start()
    {
        Player = Player != null ? Player : PlayerMain.GetPlayer;
        MapEvents.TileMapChange += DoorChanged;
        Movement.TriggerEnemy += RePosistion;
        DoorChanged(MapEvents.CurrentMap);
    }

    private void AvailblePos()
    {
        _empty.Clear();
        for (int n = _currMap.cellBounds.xMin + distFromBorder; n < _currMap.cellBounds.xMax - distFromBorder; n++)
        {
            for (int p = _currMap.cellBounds.yMin + distFromBorder; p < _currMap.cellBounds.yMax - distFromBorder; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)_currMap.transform.position.z);
                if (_currMap.HasTile(localPlace) && !dontSpawnOn.Exists(t => t.HasTile(localPlace)))
                {
                    Vector3 place = _currMap.CellToWorld(localPlace);
                    _empty.Add(place);
                }
            }
        }
    }

    public bool AroundPlayer(Vector3 vector3) => Vector3.Distance(Player.transform.position, vector3) < distFromPlayer;

    public bool AroundBoss(Vector3 vector3)
    {
        if (addedBosses.Count < 1) { return false; }
        foreach (Boss b in addedBosses)
        {
            if (Vector3.Distance(b.transform.position, vector3) < distFromBoss)
            {
                return true;
            }
        }
        return false;
    }

    public void RePosistion(BasicChar toRePos)
    {
        if (_empty.Count < 1)
        {
            AvailblePos();
        }
        int index = rnd.Next(_empty.Count);
        int tries = 0;
        while (AroundPlayer(_empty[index]) || AroundBoss(_empty[index]))
        {
            index = rnd.Next(_empty.Count);
            tries++;
            if (tries > 100)
            {
                break;
                // Give up and just let is spawn whereever
            }
        }
        toRePos.transform.position = _empty[index];
        _empty.RemoveAt(index);
    }

    private void DoorChanged(Tilemap _currMap)
    {
        this._currMap = _currMap;
        AvailblePos();
        CurrentEnemies();
        SetupEnemies();
    }

    private void CurrentEnemies()
    {
        currEnemies.Clear();
        currBosses.Clear();
        addedBosses.Clear();
        if (MapEvents.CurMapScript != null)
        {
            if (MapEvents.CurMapScript.Enemies.Count > 0)
            {
                currEnemies.AddRange(MapEvents.CurMapScript.Enemies);
                enemyToAdd = MapEvents.CurMapScript.EnemyCount;
            }
            if (MapEvents.CurMapScript.Bosses.Count > 0)
            {
                currBosses.AddRange(MapEvents.CurMapScript.Bosses);
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

    private void SetupEnemies()
    {
        transform.KillChildren();
        SpawnBosses();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (currEnemies.Count > 0)
        {
            for (int i = 0; i < enemyToAdd; i++)
            {
                EnemyPrefab prefab = currEnemies[rnd.Next(currEnemies.Count)];
                if (prefab != null)
                {
                    Instantiate(prefab, transform, true).name = prefab.name;
                    RePosistion(prefab);
                }else
                {
                    Debug.LogWarning(MapEvents.CurrentMap + " is missing enemies references");
                }
            }
        }
    }

    private void SpawnBosses()
    {
        if (currBosses.Count > 0)
        {
            foreach (Boss b in currBosses)
            {
                if (b != null)
                {
                    if (b.LockedPosistion)
                    {
                        Boss boss = Instantiate(b, b.Pos, Quaternion.identity, transform);
                        NameAndADDBoss(b, boss);
                    }
                    else
                    {
                        Boss boss = Instantiate(b, transform, true);
                        NameAndADDBoss(b, boss);
                        RePosistion(boss);
                    }
                }
            }
        }
    }

    private void NameAndADDBoss(Boss b, Boss boss)
    {
        boss.name = b.name;
        addedBosses.Add(boss);
    }
}