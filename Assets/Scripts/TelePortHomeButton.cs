using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelePortHomeButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;

    public void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btnText = btnText != null ? btnText : GetComponentInChildren<TextMeshProUGUI>();
        btn.onClick.AddListener(TeleportTo);
        btnText.text = $"World: {WorldMaps.Home}\nMap: House";
    }

    private void TeleportTo() => HomeCanvas.GetHomeCanvas.EnterHome();
}