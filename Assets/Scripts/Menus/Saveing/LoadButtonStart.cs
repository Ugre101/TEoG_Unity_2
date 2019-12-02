using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadButtonStart : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Button load, dal;

    private string _mainPath;

    // Start is called before the first frame update
    private void Start()
    {
        _mainPath = Application.persistentDataPath + "/Game_Save/";
        if (!Directory.Exists(_mainPath))
        {
            Directory.CreateDirectory(_mainPath);
        }

        load.onClick.AddListener(LoadGame);
        dal.onClick.AddListener(DeleteSave);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        string currPath = title.text;
        return _mainPath + currPath + ".json";
    }

    private PlayerMain player;
    private Transform pos;
    private Dorm dorm;
    private GameUI gameUI;
    private MapEvents mapEvents;
    private SaveMananger saveMananger;
    private TickManager tickManager;

    [SerializeField]
    private Home home = null;

    [SerializeField]
    private EventLog eventLog = null;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Running");
        saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
        player = saveMananger.player;
        pos = saveMananger.playerSprite;
        gameUI = saveMananger.gameUI;
        dorm = saveMananger.dorm;
        mapEvents = saveMananger.mapEvents;
        tickManager = saveMananger.tickManager;
        Save save = new Save(player, pos, dorm, mapEvents, tickManager, home, eventLog);
        string path = CurrentPath();
        Debug.Log(path);
        Debug.Log(File.Exists(path));
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save.LoadData(json);
        }
        gameUI.Resume();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}