using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceMenu : MonoBehaviour
{
    [SerializeField] private PlayerMain _player;

    [SerializeField] private Button _autoEssBtn = null;

    [SerializeField] private TextMeshProUGUI autoEssText = null;

    // Start is called before the first frame update
    private void Start()
    {
        _player = _player != null ? _player : PlayerMain.GetPlayer;
        EssenceText();
        _autoEssBtn.onClick.AddListener(AutoEssToggle);
    }

    public void AutoEssToggle()
    {
        _player.ToggleAutoEssence();
        EssenceText();
    }

    private void EssenceText()
    {
        autoEssText.text = $"Auto essence: {_player.AutoEss}";
    }
}