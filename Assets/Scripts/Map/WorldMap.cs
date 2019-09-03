using UnityEngine;
public enum WorldMaps
{
    StartMap,
    SecondMap,
    Home
}
public class WorldMap : MonoBehaviour
{
    public WorldMaps map;
    public void Current()
    {
        gameObject.SetActive(true);
    }
}
