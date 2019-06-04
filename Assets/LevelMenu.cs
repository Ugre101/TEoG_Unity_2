using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("Stat buttons")]
    public GameObject _str;

    public GameObject _charm, _end, _int, _will, _dex;

    [Header("Player")]
    public playerMain _player;

    private Button _strBtn, _charmBtn, _endBtn, _intBtn, _willBtn, _dexBtn;
    private TextMeshProUGUI _strText, _charmText, _endText, _intText, _willText, _dexText;

    // Awake insted of start so it updates before OnEnable
    private void Awake()
    {
        // buttons
        _strBtn = _str.GetComponent<Button>();
        _strBtn.onClick.AddListener(GainStr);
        _charmBtn = _charm.GetComponent<Button>();
        _charmBtn.onClick.AddListener(GainCharm);
        _endBtn = _end.GetComponent<Button>();
        _endBtn.onClick.AddListener(GainEnd);
        _intBtn = _int.GetComponent<Button>();
        _willBtn = _will.GetComponent<Button>();
        _dexBtn = _dex.GetComponent<Button>();
        _dexBtn.onClick.AddListener(GainDex);
        // Tmpro
        _strText = _str.GetComponentInChildren<TextMeshProUGUI>();
        _charmText = _charm.GetComponentInChildren<TextMeshProUGUI>();
        _endText = _end.GetComponentInChildren<TextMeshProUGUI>();
        _intText = _int.GetComponentInChildren<TextMeshProUGUI>();
        _willText = _will.GetComponentInChildren<TextMeshProUGUI>();
        _dexText = _dex.GetComponentInChildren<TextMeshProUGUI>();
    }
    // OnEnable so it reupdates every time you open
    private void OnEnable()
    {
        _strText.text = $"Strength: {_player.Str}";
        _charmText.text = $"Charm: {_player.Charm}";
        _endText.text = $"Endurance: {_player.End}";
        _intText.text = $"Int:";
        _willText.text = $"Willpower: ";
        _dexText.text = $"Dexterity {_player.Dex}";
    }

    private void GainStr()
    {
        _player.Str++;
        _strText.text = $"Strength: {_player.Str}";
    }

    private void GainCharm()
    {
        _player.Charm++;
        _charmText.text = $"Charm: {_player.Charm}";
    }

    private void GainEnd()
    {
        _player.End++;
        _endText.text = $"Endurance: {_player.End}";
    }
    private void GainDex()
    {
        _player.Dex++;
        _dexText.text = $"Dexterity: {_player.Dex}";
    }
}