using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TelePortMenu : MonoBehaviour
{
    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private TelePortButton prefabBtn = null;
    [SerializeField] private TelePortHomeButton homePrefabBtn = null;
    [SerializeField] private Transform btnConatiner = null;
    [SerializeField] private TMP_Dropdown sortDropDown = null;
    private const string sortAllName = "All";// Just to make it easier, when sorting

    private void Start()
    {
        if (mapEvents == null)
        {
            enabled = false;
        }
        if (sortDropDown != null)
        {
            List<WorldMaps> worldMaps = ((WorldMaps[])System.Enum.GetValues(typeof(WorldMaps))).ToList();
            TMP_Dropdown.OptionDataList optionDataList = new TMP_Dropdown.OptionDataList();
            worldMaps.ForEach(w => optionDataList.options.Add(new TMP_Dropdown.OptionData(w.ToString())));
            sortDropDown.ClearOptions();
            sortDropDown.options.Add(new TMP_Dropdown.OptionData(sortAllName));
            sortDropDown.AddOptions(optionDataList.options);
            sortDropDown.onValueChanged.AddListener(delegate { ListSortedByWorldTeleports(sortDropDown); });
        }
    }

    private void OnEnable()
    {
        ListAllTeleports();
    }

    private void ListAllTeleports()
    {
        btnConatiner.KillChildren();
        Instantiate(homePrefabBtn, btnConatiner);
        mapEvents.TelePortLocations.FindAll(tl => tl.CanTelePortTo.Know).ForEach(t => Instantiate(prefabBtn, btnConatiner).Setup(t));
        //  Instantiate(prefabBtn,btnConatiner).Setup(mapEvents,WorldMaps.Home)
        // TODO custom teleport for home
    }

    private void ListSortedByWorldTeleports(TMP_Dropdown tMP_Dropdown)
    {
        btnConatiner.KillChildren();
        List<TelePortLocation> firstSort = mapEvents.TelePortLocations.FindAll(tl => tl.CanTelePortTo.Know);
        WorldMaps? world = Enum.TryParse(tMP_Dropdown.options[tMP_Dropdown.value].text, true, out WorldMaps tryFirst) ? (WorldMaps?)tryFirst : null;
        if (world.HasValue)
        {
            firstSort.FindAll(tl => tl.World == world).ForEach(t => Instantiate(prefabBtn, btnConatiner).Setup(t));
        }
        else if (tMP_Dropdown.options[tMP_Dropdown.value].text == sortAllName)
        {
            ListAllTeleports();
        }
    }
}

public class TelePortMenuHome : TelePortMenu
{
}