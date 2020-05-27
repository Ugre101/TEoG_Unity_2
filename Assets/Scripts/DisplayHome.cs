using System.Collections.Generic;
using UnityEngine;

public class DisplayHome : MonoBehaviour
{
    [SerializeField] private SpriteRenderer home = null, dorm = null;
    [SerializeField] private List<Sprite> homeSprites = new List<Sprite>(), dormSpites = new List<Sprite>();

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
        int tier = Mathf.Min(homeSprites.Count - 1, Mathf.FloorToInt(StartHomeStats.MainHouse.Level / 3));
        home.sprite = homeSprites[tier];
    }

    private void DisplayDorm()
    {
        int tier = Mathf.Min(dormSpites.Count, StartHomeStats.Dorm.Level);
        if (tier == 0)
        {
            dorm.gameObject.SetActive(false);
        }
        else
        {
            dorm.gameObject.SetActive(true);
            dorm.sprite = dormSpites[tier - 1];
        }
    }
}