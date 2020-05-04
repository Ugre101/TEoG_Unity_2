using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelePortButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private TelePortLocation canTele;

    public void Setup(TelePortLocation canTele)
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btnText = btnText != null ? btnText : GetComponentInChildren<TextMeshProUGUI>();
        this.canTele = canTele;
        btn.onClick.AddListener(TeleportTo);
        btnText.text = $"World: {canTele.CanTelePortTo.WorldMaps}\nMap: {canTele.CanTelePortTo.Map.name}";
    }

    private void TeleportTo()
    {
        canTele.TelePortTo();
        GameManager.CurrentArea = GlobalArea.Map;
        CanvasMain.GetCanvasMain.Resume();
    }


}