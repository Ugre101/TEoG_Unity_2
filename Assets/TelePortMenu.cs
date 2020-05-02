using System.Collections.Generic;
using UnityEngine;

public class TelePortMenu : MonoBehaviour
{
    [SerializeField] private HomeMain home = null;
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private TelePortButton prefabBtn = null;
    [SerializeField] private Transform btnConatiner = null, sortBtnContainer = null;

    private void Start()
    {
        if (mapEvents == null)
        {
            enabled = false;
        }
    }

    private void OnEnable()
    {
        ListAllTeleports();
    }

    private void ListAllTeleports()
    {
        btnConatiner.KillChildren();
        mapEvents.TelePortLocations.FindAll(tl => tl.CanTelePortTo.Know).ForEach(t => Instantiate(prefabBtn, btnConatiner).Setup(t, home));
        //  Instantiate(prefabBtn,btnConatiner).Setup(mapEvents,WorldMaps.Home)
        // TODO make a playerflag for know maps, and make a way to get tilemaps in here
    }

    private void ListSortedByWorldTeleports(WorldMaps world)
    {
        btnConatiner.KillChildren();
        List<TelePortLocation> firstSort = mapEvents.TelePortLocations.FindAll(tl => tl.CanTelePortTo.Know);
        firstSort.FindAll(tl => tl.World == world).ForEach(t => Instantiate(prefabBtn, btnConatiner).Setup(t, home));
    }
}