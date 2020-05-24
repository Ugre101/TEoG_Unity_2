using UnityEngine;

public class BuildingsMenu : MonoBehaviour
{
    public void EnterBuilding(Building building)
    {
        gameObject.SetActive(true);
        transform.SleepChildren(building.transform);
    }
}