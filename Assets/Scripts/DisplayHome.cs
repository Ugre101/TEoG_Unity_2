using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplayHome : MonoBehaviour
{
    [SerializeField] private Tilemap t1Home = null, t2home = null;
    [SerializeField] private List<Tilemap> homes = new List<Tilemap>();
    [SerializeField] private Tilemap t1Dorm = null, t2Dorm = null;

    private void OnEnable()
    {
        StartHomeStats.MainHouse.Refresh += DisplayMainHouse;
        StartHomeStats.Dorm.Refresh += DisplayDorm;
        DisplayMainHouse();
        DisplayDorm();
    }

    private void OnDisable()
    {
        StartHomeStats.MainHouse.Refresh -= DisplayMainHouse;
        StartHomeStats.Dorm.Refresh -= DisplayDorm;
    }

    private void DisplayMainHouse()
    {
        homes.ForEach(m => m.gameObject.SetActive(false));
        int tier = Mathf.Min(homes.Count - 1, Mathf.FloorToInt(StartHomeStats.MainHouse.Level / 3));
        homes[tier].gameObject.SetActive(true);
    }

    private void DisplayDorm()
    {
        int tier = StartHomeStats.Dorm.Level;
        if (tier == 0)
        {
            t1Dorm.gameObject.SetActive(false);
            t2Dorm.gameObject.SetActive(false);
        }
        else if (tier < 4)
        {
            t1Dorm.gameObject.SetActive(true);
            t2Dorm.gameObject.SetActive(false);
        }
        else
        {
            t1Dorm.gameObject.SetActive(false);
            t2Dorm.gameObject.SetActive(true);
        }
    }
}