using UnityEngine;

public class TelePortMenu : MonoBehaviour
{
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private TelePortButton prefabBtn = null;
    [SerializeField] private Transform btnConatiner = null;
    private void Start()
    {
        if (mapEvents == null)
        {
            enabled = false;
        }
    }

    private void ListTeleports()
    {
      //  Instantiate(prefabBtn,btnConatiner).Setup(mapEvents,WorldMaps.Home)
        // TODO make a playerflag for know maps, and make a way to get tilemaps in here
    }
}