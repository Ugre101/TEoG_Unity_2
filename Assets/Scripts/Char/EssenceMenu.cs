using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceMenu : MonoBehaviour
{
    public PlayerMain _player;
    public GameObject _autoEss;

    private Button _autoEssBtn;

    // Start is called before the first frame update
    private void Start()
    {
        if (_autoEss != null)
        {
            TextMeshProUGUI text = _autoEss.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"Auto essence: {_player.AutoEss}";
            _autoEssBtn = _autoEss.GetComponent<Button>();
            _autoEssBtn.onClick.AddListener(AutoEssToggle);
        }
    }

    private void AutoEssToggle()
    {
        _player.ToggleAutoEssence();
        TextMeshProUGUI text = _autoEss.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Auto essence: {_player.AutoEss}";
    }

    // Update is called once per frame
    private void Update()
    {
    }
}