using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadButtonStart : MonoBehaviour
{
    private TextMeshProUGUI[] _textList;
    private TextMeshProUGUI _text;
    private Button[] _buttons;
    private Button _load, _del;

    private string _mainPath;

    // Start is called before the first frame update
    private void Start()
    {
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
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
    private playerMain player;
    private Transform pos;
    private Dorm dorm;
    private GameUI gameUI;
    private MapEvents mapEvents;
    private SaveMananger saveMananger;
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
        player = saveMananger.player; //GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        pos = saveMananger.playerSprite; //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameUI = saveMananger.gameUI; //GameObject.Find("Canvas").GetComponent<GameUI>();
        dorm = saveMananger.dorm; //GameObject.Find("Dorm-servants").GetComponent<Dorm>();
        mapEvents = saveMananger.mapEvents;
        Save save = new Save(player, pos, dorm,mapEvents);
        string path = CurrentPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save.LoadData(json);
        }
        gameUI.Resume();
    }
}
