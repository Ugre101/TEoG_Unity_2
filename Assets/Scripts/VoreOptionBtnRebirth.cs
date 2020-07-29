using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreOptionBtnRebirth : MonoBehaviour
    {
        [SerializeField] private Button btn = null;
        [SerializeField] private TextMeshProUGUI title = null;
        private VoreVagina voreVagina = null;

        public void Setup(VoreVagina voreVagina)
        {
            this.voreVagina = voreVagina;
            btn = btn != null ? btn : GetComponent<Button>();
            title = title != null ? title : GetComponentInChildren<TextMeshProUGUI>();
            btn.onClick.AddListener(Toggle);
            SetText(voreVagina.ChildTf);
        }

        private void SetText(bool val) => title.text = $"Child tf: {val}";

        private void Toggle() => SetText(voreVagina.ToggleChildTf());
    }
}