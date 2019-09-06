using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private SaveMananger saveMananger;
    private playerMain player;
    private Transform pos;
    private Dorm dorm;
    private GameUI gameUI;
    private MapEvents mapEvents;
    private TextMeshProUGUI[] _textList;
    private TextMeshProUGUI _text;
    private Button[] _buttons;
    private Button _load, _del;

    private string _mainPath;

    // Start is called before the first frame update
    private void Start()
    {
        saveMananger = GetComponentInParent<SaveMananger>();
        player = saveMananger.player;
        pos = saveMananger.playerSprite;
        gameUI = saveMananger.gameUI;
        dorm = saveMananger.dorm;
        mapEvents = saveMananger.mapEvents;
        _mainPath = Application.persistentDataPath + "/Game_Save/";
        if (!Directory.Exists(_mainPath))
        {
            Directory.CreateDirectory(_mainPath);
        }
        // Set buttons
        _buttons = GetComponentsInChildren<Button>();
        _load = _buttons[0];
        _del = _buttons[1];

        _textList = GetComponentsInChildren<TextMeshProUGUI>();
        _text = _textList[0];
        _load.onClick.AddListener(LoadGame);
        _del.onClick.AddListener(DeleteSave);
    }

    public void LoadGame()
    {
        Save save = new Save(player, pos, dorm, mapEvents);
        string path = CurrentPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save.LoadData(json);
        }
        gameUI.Resume();
    }

    public void DeleteSave()
    {
        string path = CurrentPath();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private string CurrentPath()
    {
        string currPath = _text.text;
        return _mainPath + currPath + ".json";
    }
}