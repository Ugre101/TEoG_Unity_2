using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShowDorm : MonoBehaviour
{
    [SerializeField] private Dorm dorm = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private ShowServant ServantListPrefab = null;
    [SerializeField] private GameObject servantList = null;
    [SerializeField] private ShowOneServant aServant = null;
    [SerializeField] private GameObject ifEmpty = null;
    [SerializeField] private TMP_Dropdown raceDropdown = null, genderDropdown = null;

    private Races? chooseRace;
    private Genders? chooseGender;

    private void Start()
    {
        // Get arrays of enums to arrays
        Races[] races = (Races[])Enum.GetValues(typeof(Races));
        Genders[] genders = (Genders[])Enum.GetValues(typeof(Genders));
        // Convert the array to list for ease of use.
        List<Races> racesList = races.ToList();
        List<Genders> genderList = genders.ToList();
        // Convert enums to string and add them to option list
        TMP_Dropdown.OptionDataList optionDataList = new TMP_Dropdown.OptionDataList();
        racesList.ForEach(r => optionDataList.options.Add(new TMP_Dropdown.OptionData(r.ToString())));
        TMP_Dropdown.OptionDataList optionDataList1 = new TMP_Dropdown.OptionDataList();
        genderList.ForEach(g => optionDataList1.options.Add(new TMP_Dropdown.OptionData(g.ToString())));
        // Clear default options
        raceDropdown.ClearOptions();
        genderDropdown.ClearOptions();
        // Add my new ones + one extra for all
        raceDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        raceDropdown.AddOptions(optionDataList.options);
        genderDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        genderDropdown.AddOptions(optionDataList1.options);
        // Add listerners
        raceDropdown.onValueChanged.AddListener(delegate { SortRace(raceDropdown); });
        genderDropdown.onValueChanged.AddListener(delegate { SortGender(genderDropdown); });
    }

    public void OnEnable()
    {
        dorm = dorm != null ? dorm : Dorm.GetDrom;
        chooseRace = null;
        raceDropdown.value = 0;
        raceDropdown.RefreshShownValue();
        genderDropdown.value = 0;
        genderDropdown.RefreshShownValue();
        ListServants();
    }

    public void ListServants()
    {
        servantList.SetActive(true);
        aServant.gameObject.SetActive(false);

        bool hasSevants = dorm.Servants.Count > 0;
        ifEmpty.SetActive(!hasSevants);
        container.KillChildren();
        if (hasSevants)
        {
            if (chooseRace.HasValue || chooseGender.HasValue)
            {
                List<BasicChar> sorted = dorm.Servants;
                if (chooseRace.HasValue) { sorted = sorted.FindAll(bc => bc.RaceSystem.CurrentRace() == chooseRace.Value); }
                if (chooseGender.HasValue) { sorted = sorted.FindAll(bc => bc.Gender == chooseGender.Value); }
                sorted.ForEach(s =>
                {
                    ShowServant showServant = Instantiate(ServantListPrefab, container);
                    showServant.Init(s);
                    showServant.Btn.onClick.AddListener(() => ShowAServant(s));
                });
            }
            else
            {
                dorm.Servants.ForEach(s =>
                {
                    ShowServant showServant = Instantiate(ServantListPrefab, container);
                    showServant.Init(s);
                    showServant.Btn.onClick.AddListener(() => ShowAServant(s));
                });
            }
        }
    }

    private void ShowAServant(BasicChar basicChar)
    {
        servantList.SetActive(false);
        aServant.Setup(basicChar);
    }

    private void SortRace(TMP_Dropdown tMP_Dropdown)
    {
        chooseRace = Enum.TryParse(tMP_Dropdown.options[tMP_Dropdown.value].text, true, out Races tryFirst) ? (Races?)tryFirst : null;
        ListServants();
    }

    private void SortGender(TMP_Dropdown tMP_Dropdown)
    {
        chooseGender = Enum.TryParse(tMP_Dropdown.options[tMP_Dropdown.value].text, true, out Genders genders) ? (Genders?)genders : null;
        ListServants();
    }
}