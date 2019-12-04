using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceMenu : MonoBehaviour
{
    public PlayerMain _player;

    [SerializeField]
    private Button _autoEssBtn = null;

    [SerializeField]
    private TextMeshProUGUI autoEssText = null;

    // Start is called before the first frame update
    private void Start()
    {
        autoEssText.text = $"Auto essence: {_player.AutoEss}";
        _autoEssBtn.onClick.AddListener(AutoEssToggle);
    }

    public void AutoEssToggle()
    {
        _player.ToggleAutoEssence();
        autoEssText.text = $"Auto essence: {_player.AutoEss}";
    }
}