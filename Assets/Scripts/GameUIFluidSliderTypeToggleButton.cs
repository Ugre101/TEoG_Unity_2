using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIFluidSliderTypeToggleButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        text = text != null ? text : GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Fluid slidertype: {GameUISettings.FluidSliderType}";
        btn.onClick.AddListener(Toggle);
    }

    private void Toggle() => text.text = $"Fluid slidertype: {GameUISettings.ToggleSliderType}";
}