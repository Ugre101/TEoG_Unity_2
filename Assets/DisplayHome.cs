using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplayHome : MonoBehaviour
{
    [SerializeField] private Tilemap t1Home, t2home;

    private void OnEnable()
    {
        StartHomeStats.MainHouse.Refresh += DisplayMainHouse;
        DisplayMainHouse();
    }

    private void OnDisable()
    {
        StartHomeStats.MainHouse.Refresh -= DisplayMainHouse;
    }

    private void DisplayMainHouse()
    {
        int tier = StartHomeStats.MainHouse.Level;
        if (tier == 0)
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
}