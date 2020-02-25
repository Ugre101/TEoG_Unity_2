using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplayHome : MonoBehaviour
{
    [SerializeField] private Tilemap t1Home, t2home;
    [SerializeField] private Tilemap t1Dorm, t2Dorm;

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
        int tier = StartHomeStats.MainHouse.Level;
        if (tier < 3)
        {
            t1Home.gameObject.SetActive(true);
            t2home.gameObject.SetActive(false);
        }
        else
        {
            t1Home.gameObject.SetActive(false);
            t2home.gameObject.SetActive(true);
        }
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