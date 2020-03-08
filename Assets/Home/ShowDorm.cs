using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShowDorm : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private Dorm dorm = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private ShowServant ServantListPrefab = null;
    [SerializeField] private GameObject servantList = null;
    [SerializeField] private ShowOneServant aServant = null;
    [SerializeField] private GameObject ifEmpty = null;
    [SerializeField] private TMP_Dropdown raceDropdown = null, genderDropdown = null, sortBy = null;

    private Races? chooseRace;
    private Genders? chooseGender;
    private SortByEnum? sortByStat;

    private enum SortByEnum
    {
        AffectionAcc,
        AffectionDec,
        ObedianceAcc,
        ObedianceDec,
    }

    private void Start()
    {
        // Get arrays of enums to arrays
        Races[] races = (Races[])Enum.GetValues(typeof(Races));
        Genders[] genders = (Genders[])Enum.GetValues(typeof(Genders));
        SortByEnum[] sort = (SortByEnum[])Enum.GetValues(typeof(SortByEnum));
        // Convert the array to list for ease of use.
        List<Races> racesList = races.ToList();
        List<Genders> genderList = genders.ToList();
        List<SortByEnum> sortByEnums = sort.ToList();
        // Convert enums to string and add them to option list
        TMP_Dropdown.OptionDataList optionDataList = new TMP_Dropdown.OptionDataList();
        racesList.ForEach(r => optionDataList.options.Add(new TMP_Dropdown.OptionData(r.ToString())));
        TMP_Dropdown.OptionDataList optionDataList1 = new TMP_Dropdown.OptionDataList();
        genderList.ForEach(g => optionDataList1.options.Add(new TMP_Dropdown.OptionData(g.ToString())));
        TMP_Dropdown.OptionDataList optionDataList2 = new TMP_Dropdown.OptionDataList();
        sortByEnums.ForEach(g => optionDataList2.options.Add(new TMP_Dropdown.OptionData(g.ToString())));
        // Clear default options
        raceDropdown.ClearOptions();
        genderDropdown.ClearOptions();
        sortBy.ClearOptions();
        // Add my new ones + one extra for all
        raceDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        raceDropdown.AddOptions(optionDataList.options);
        genderDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        genderDropdown.AddOptions(optionDataList1.options);
        sortBy.options.Add(new TMP_Dropdown.OptionData("Sort by"));
        sortBy.AddOptions(optionDataList2.options);
        // Add listerners
        raceDropdown.onValueChanged.AddListener(delegate { SortRace(raceDropdown); });
        genderDropdown.onValueChanged.AddListener(delegate { SortGender(genderDropdown); });
        sortBy.onValueChanged.AddListener(delegate { SortByStat(sortBy); });
    }

    public void OnEnable()
    {
        dorm = dorm != null ? dorm : Dorm.GetDrom;
        chooseRace = null;
        raceDropdown.value = 0;
        raceDropdown.RefreshShownValue();
        genderDropdown.value = 0;
        genderDropdown.RefreshShownValue();
        sortBy.value = 0;
        sortBy.RefreshShownValue();
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
            if (chooseRace.HasValue || chooseGender.HasValue || sortByStat.HasValue)
            {
                List<BasicChar> sorted = dorm.Servants;
                if (chooseRace.HasValue) { sorted = sorted.FindAll(bc => bc.RaceSystem.CurrentRace() == chooseRace.Value); }
                if (chooseGender.HasValue) { sorted = sorted.FindAll(bc => bc.Gender == chooseGender.Value); }
                if (sortByStat.HasValue) { sorted = SortSercantByRelationship(sorted); }
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

    private void SortByStat(TMP_Dropdown tMP_Dropdown)
    {
        sortByStat = Enum.TryParse(tMP_Dropdown.options[tMP_Dropdown.value].text, true, out SortByEnum sort) ? (SortByEnum?)sort : null;
        ListServants();
    }

    private List<BasicChar> SortSercantByRelationship(List<BasicChar> basicChars)
    {
        if (sortByStat.HasValue)
        {
            switch (sortByStat.Value)
            {
                // TODO make this working
                case SortByEnum.AffectionAcc:
                    basicChars.Sort(delegate (BasicChar a, BasicChar b)
                    {
                        return a.RelationshipTracker.GetReleationWith(player).AffectionValue.CompareTo(b.RelationshipTracker.GetReleationWith(player).AffectionValue);
                    });
                    basicChars.ForEach(b => Debug.Log(b.RelationshipTracker.GetReleationWith(player).AffectionValue));
                    break;

                case SortByEnum.AffectionDec:
                    basicChars.OrderByDescending(s => s.RelationshipTracker.GetReleationWith(player).AffectionValue);
                    break;

                case SortByEnum.ObedianceAcc:
                    basicChars.OrderBy(s => s.RelationshipTracker.GetReleationWith(player).ObedienceValue);
                    break;

                case SortByEnum.ObedianceDec:
                    basicChars.OrderByDescending(s => s.RelationshipTracker.GetReleationWith(player).AffectionValue);
                    break;
            }
        }
        return basicChars;
    }
}