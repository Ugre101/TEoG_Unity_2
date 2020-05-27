using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FertilityShrineStuff
{
    public class FertilityShrine : Building
    {
        private List<PregnancyBlessingsIds> blessingsIds = new List<PregnancyBlessingsIds>();
        [SerializeField] private GameObject introPart = null, blessingsPart = null;
        [SerializeField] private Transform container = null;
        [SerializeField] private FertilityShrineBlessingBtn btnPrefab = null;
        [SerializeField] private TextMeshProUGUI fertPoints = null, toggleBlessingBtnText = null;
        [SerializeField] private Button toggleBlessingsBtn = null;

        private void Start()
        {
            blessingsIds = UgreTools.EnumToList<PregnancyBlessingsIds>();
            AddBlessBtns();
            toggleBlessingsBtn.onClick.AddListener(ToggleBlessing);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            fertPoints.text = "Shrine points: " + player.PregnancySystem.FertilityShrinePoints.ToString();
            TogglePart(true);
        }

        private void ToggleBlessing() => TogglePart(!introPart.activeSelf);

        private void TogglePart(bool intro)
        {
            introPart.SetActive(intro);
            blessingsPart.SetActive(!intro);
            toggleBlessingBtnText.text = intro ? "Blessings" : "Back";
        }

        private void AddBlessBtns()
        {
            container.KillChildren();
            foreach (PregnancyBlessingsIds id in blessingsIds)
            {
                Instantiate(btnPrefab, container).Setup(player, id);
            }
        }
    }
}