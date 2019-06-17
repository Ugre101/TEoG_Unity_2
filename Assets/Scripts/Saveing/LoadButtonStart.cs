using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadButtonStart : MonoBehaviour
{
    private playerMain player;
    private Transform pos;
    private GameUI gameUI;
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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameUI = GameObject.Find("Canvas").GetComponent<GameUI>();
        Save save = new Save(player, pos);
        string path = CurrentPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save.LoadData(json);
        }
        gameUI.Resume();
    }
}
