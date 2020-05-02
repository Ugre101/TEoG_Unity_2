using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelePortButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private TelePortLocation canTele;
    private HomeMain home;

    public void Setup(TelePortLocation canTele, HomeMain home)
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btnText = btnText != null ? btnText : GetComponentInChildren<TextMeshProUGUI>();
        this.canTele = canTele;
        this.home = home;
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