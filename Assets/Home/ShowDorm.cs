using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShowDorm : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
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
        // Clear default options
        raceDropdown.ClearOptions();
        genderDropdown.ClearOptions();
        sortBy.ClearOptions();
        // Add my new ones + one extra for all
        raceDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        raceDropdown.AddOptions(UgreTools.EnumToOptionDataList<Races>());
        genderDropdown.options.Add(new TMP_Dropdown.OptionData("All"));
        genderDropdown.AddOptions(UgreTools.EnumToOptionDataList<Genders>());
        sortBy.options.Add(new TMP_Dropdown.OptionData("Sort by"));
        sortBy.AddOptions(UgreTools.EnumToOptionDataList<SortByEnum>());
        // Add listerners
        raceDropdown.onValueChanged.AddListener(delegate { SortRace(raceDropdown); });
        genderDropdown.onValueChanged.AddListener(delegate { SortGender(genderDropdown); });
        sortBy.onValueChanged.AddListener(delegate { SortByStat(sortBy); });
    }

    public void OnEnable()
    {
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

        bool hasSevants = Dorm.Followers.Count > 0;
        ifEmpty.SetActive(!hasSevants);
        container.KillChildren();
        if (hasSevants)
        {
            if (chooseRace.HasValue || chooseGender.HasValue || sortByStat.HasValue)
            {
                List<BasicChar> sorted = Dorm.Followers;
                if (chooseRace.HasValue) { sorted = sorted.FindAll(bc => bc.RaceSystem.CurrentRace() == chooseRace.Value); }
                if (chooseGender.HasValue) { sorted = sorted.FindAll(bc => GenderExtensions.Gender(bc) == chooseGender.Value); }
                if (sortByStat.HasValue) { sorted = SortSercantByRelationship(sorted); }
                sorted.ForEach(s => Instantiate(ServantListPrefab, container).Init(s).onClick.AddListener(() => ShowAServant(s)));
            }
            else
            {
                Dorm.Followers.ForEach(s => Instantiate(ServantListPrefab, container).Init(s).onClick.AddListener(() => ShowAServant(s)));
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
                    basicChars.Sort((BasicChar a, BasicChar b)
                        => a.RelationshipTracker.GetReleationWith(player).AffectionValue.CompareTo(b.RelationshipTracker.GetReleationWith(player).AffectionValue));
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