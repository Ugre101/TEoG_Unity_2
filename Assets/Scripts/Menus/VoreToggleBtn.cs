using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoreToggleBtn : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    private void BtnText(bool active) => text.text = $"Vore: {active}";

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(ToggleVore);
        text = text != null ? text : GetComponentInChildren<TextMeshProUGUI>();
        BtnText(Settings.Vore);
    }

    private void ToggleVore() => BtnText(Settings.ToogleVore());
}