using UnityEngine;

public class Buildings : MonoBehaviour
{
    public void EnterBuilding(Building building)
    {
        gameObject.SetActive(true);
        transform.SleepChildren(building.transform);
    }
}