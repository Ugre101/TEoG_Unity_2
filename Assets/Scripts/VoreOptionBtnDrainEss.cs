using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreOptionBtnDrainEss : MonoBehaviour
    {
        [SerializeField] private Button btn = null;
        [SerializeField] private TextMeshProUGUI title = null;

        private void SetText(ChooseEssence value) => title.text = $"Drain: {value}";

        public void Setup()
        {
            btn = btn != null ? btn : GetComponent<Button>();
            title = title != null ? title : GetComponentInChildren<TextMeshProUGUI>();
            btn.onClick.AddListener(Toggle);
            SetText(VoreSettings.DrainEss);
        }

        private void Toggle() => SetText(VoreSettings.ToggleDrainEss());
    }
}