using UnityEngine;

public class MapDisabler : MonoBehaviour
{
    public GameObject tilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tilemap.SetActive(tilemap.activeSelf ? false : true);
    }
}
