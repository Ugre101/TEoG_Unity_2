using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FertilityShrineStuff
{
    public class FertilityShrine : Building
    {
        [SerializeField] private Transform container = null;
        [SerializeField] private List<PregnancyBlessingsIds> blessingsIds;
        [SerializeField] private FertilityShrineBlessingBtn btnPrefab = null;
        [SerializeField] private TextMeshProUGUI fertPoints = null;

        private void Start()
        {
            blessingsIds = UgreTools.EnumToList<PregnancyBlessingsIds>();
            AddBlessBtns();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            fertPoints.text = "Shrine points: " + player.PregnancySystem.FertilityShrinePoints.ToString();
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

    public class FertilityShrineBlessingBtn : MonoBehaviour
    {
        [SerializeField] private Button btn = null;
        [SerializeField] private TextMeshProUGUI btnText = null;
        [SerializeField] private Color canAffordColor = new Color(), cantAffordColor = new Color();
        [SerializeField] private Image frame = null;
        private BasicChar toBless;
        private PregnancyBlessingsIds blessingsId;

        private int GetCost()
        {
            int level = toBless.PregnancySystem.PregnancyBlessings.GetBlessingValue(blessingsId), baseCost = 1;
            float power = 1.5f;
            switch (blessingsId)
            {
                case PregnancyBlessingsIds.Incubator:
                    break;

                case PregnancyBlessingsIds.BroadMother:
                    break;

                case PregnancyBlessingsIds.VirileLoad:
                    break;

                case PregnancyBlessingsIds.SpermFactory:
                    break;

                case PregnancyBlessingsIds.PrenancyFreak:
                    baseCost = 10;
                    break;

                default: break;
            }
            return level > 0 ? Mathf.CeilToInt(Mathf.Pow(level * baseCost, power)) : baseCost;
        }

        private bool CanAfford => toBless.PregnancySystem.FertilityShrinePoints > GetCost();

        private void SetFrameColor() => frame.color = CanAfford ? canAffordColor : cantAffordColor;

        private void SetBtnText() => btnText.text = $"{blessingsId.ToString()}: {GetCost()}p";

        public void Setup(BasicChar toBless, PregnancyBlessingsIds blessingsId)
        {
            this.toBless = toBless;
            this.blessingsId = blessingsId;
            btn.onClick.AddListener(Bless);
            SetFrameColor();
            SetBtnText();
        }

        private void Bless()
        {
            if (CanAfford)
            {
                toBless.PregnancySystem.PregnancyBlessings.AddBlessing(blessingsId);
                SetFrameColor();
                SetBtnText();
            }
            else
            {
                SetFrameColor();
                SetBtnText();
            }
        }

        private void OnEnable()
        {
            if (toBless != null)
            {
                SetFrameColor();
                SetBtnText();
            }
        }
    }
}