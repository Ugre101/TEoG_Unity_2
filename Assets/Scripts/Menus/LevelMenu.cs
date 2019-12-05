using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("Stat buttons")]
    public GameObject _str;

    public GameObject _charm, _end, _int, _will, _dex;

    [Header("Perk buttons")]
    public GameObject _fasterRest;

    public GameObject _moreEss, _giveEss;

    [Header("Player")]
    public PlayerMain _player;

    private Button _strBtn, _charmBtn, _endBtn, _intBtn, _willBtn, _dexBtn;
    private Button _fasterBtn, _moreBtn, _giveBtn;
    private TextMeshProUGUI _strText, _charmText, _endText, _intText, _willText, _dexText;
    private TextMeshProUGUI _fasterText, _moreText, _giveText;

    // Awake insted of start so it updates before OnEnable
    private void Awake()
    {
        // buttons stats
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
        // buttons perks
        _fasterBtn = _fasterRest.GetComponent<Button>();
        _fasterBtn.onClick.AddListener(FasterRest);
        _giveBtn = _giveEss.GetComponent<Button>();
        _giveBtn.onClick.AddListener(GiveEss);
        _moreBtn = _moreEss.GetComponent<Button>();
        _moreBtn.onClick.AddListener(GainEss);
        // Tmpro stats
        _strText = _str.GetComponentInChildren<TextMeshProUGUI>();
        _charmText = _charm.GetComponentInChildren<TextMeshProUGUI>();
        _endText = _end.GetComponentInChildren<TextMeshProUGUI>();
        _intText = _int.GetComponentInChildren<TextMeshProUGUI>();
        _willText = _will.GetComponentInChildren<TextMeshProUGUI>();
        _dexText = _dex.GetComponentInChildren<TextMeshProUGUI>();
        // perks
        _fasterText = _fasterRest.GetComponentInChildren<TextMeshProUGUI>();
        _giveText = _giveEss.GetComponentInChildren<TextMeshProUGUI>();
        _moreText = _moreEss.GetComponentInChildren<TextMeshProUGUI>();
    }

    // OnEnable so it reupdates every time you open
    private void OnEnable()
    {
        // stats
        _strText.text = $"Strength: {_player.Stats.Strength.baseValue}";
        _charmText.text = $"Charm: {_player.Stats.Charm.baseValue}";
        _endText.text = $"Endurance: {_player.Stats.Endurance.baseValue}";
        _intText.text = $"Int:";
        _willText.text = $"Willpower: ";
        _dexText.text = $"Dexterity {_player.Stats.Dexterity.baseValue}";
        // perks
        _fasterText.text = _player.Perks.DisplayPerk(PerksTypes.FasterRest);
        _giveText.text = _player.Perks.DisplayPerk(PerksTypes.GiveEss);
        _moreText.text = _player.Perks.DisplayPerk(PerksTypes.GainEss);
    }

    private void GainStr()
    {
        if (_player.ExpSystem.StatBool)
        {
            _player.Stats.Strength.baseValue++;
            _strText.text = $"Strength: {_player.Stats.Strength.baseValue}";
        }
    }

    private void GainCharm()
    {
        if (_player.ExpSystem.StatBool)
        {
            _player.Stats.Charm.baseValue++;
            _charmText.text = $"Charm: {_player.Stats.Charm.baseValue}";
        }
    }

    private void GainEnd()
    {
        if (_player.ExpSystem.StatBool)
        {
            _player.Stats.Endurance.baseValue++;
            _endText.text = $"Endurance: {_player.Stats.Endurance.baseValue}";
        }
    }

    private void GainDex()
    {
        if (_player.ExpSystem.StatBool)
        {
            _player.Stats.Dexterity.baseValue++;
            _dexText.text = $"Dexterity: {_player.Stats.Dexterity.baseValue}";
        }
    }

    // Perks
    private void FasterRest()
    {
        if (_player.ExpSystem.PerkBool())
        {
            _player.Perks.GainPerk(PerksTypes.FasterRest);
            _fasterText.text = _player.Perks.DisplayPerk(PerksTypes.FasterRest);
        }
    }

    private void GiveEss()
    {
        if (_player.ExpSystem.PerkBool())
        {
            _player.Perks.GainPerk(PerksTypes.GiveEss);
            _giveText.text = _player.Perks.DisplayPerk(PerksTypes.GiveEss);
        }
    }

    private void GainEss()
    {
        if (_player.ExpSystem.PerkBool())
        {
            _player.Perks.GainPerk(PerksTypes.GainEss);
            _moreText.text = _player.Perks.DisplayPerk(PerksTypes.GainEss);
        }
    }
}