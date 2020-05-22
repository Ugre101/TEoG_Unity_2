using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScatToggleBtn : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    private void BtnText(bool active) => text.text = $"Scat: {active}";

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(ToggleScat);
        text = text != null ? text : GetComponentInChildren<TextMeshProUGUI>();
        BtnText(Settings.Scat);
    }

    private void ToggleScat() => BtnText(Settings.ToogleScat());
}