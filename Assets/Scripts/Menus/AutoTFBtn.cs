using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoTFBtn : MonoBehaviour
{
    public PlayerMain player;
    private Button btn;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(AutoTF);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Auto TF: {player.AutoEss}";
    }
    private void AutoTF()
    {
        player.ToggleAutoEssence();
        text.text = $"Auto TF: {player.AutoEss}";
    }
}
