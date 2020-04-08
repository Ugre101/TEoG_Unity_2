using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreOptionBtnDigestion : MonoBehaviour
    {
        [SerializeField] private Button btn = null;
        [SerializeField] private TextMeshProUGUI title = null;

        private VoreBasic organ = null;

        private void SetText(bool boolVal) => title.text = $"Digestion: {boolVal}";

        public void Setup(VoreBasic organ)
        {
            btn = btn != null ? btn : GetComponent<Button>();
            title = title != null ? title : GetComponentInChildren<TextMeshProUGUI>();
            this.organ = organ;
            btn.onClick.AddListener(Toggle);
            SetText(organ.Digestion);
        }

        private void Toggle() => SetText(organ.ToggleDigestion);
    }
}