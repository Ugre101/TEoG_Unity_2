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

    private playerMain player;
    private Transform pos;
    private Dorm dorm;
    private GameUI gameUI;
    private MapEvents mapEvents;
    private SaveMananger saveMananger;
    private TickManager tickManager;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Running");
        saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
        player = saveMananger.player; //GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        pos = saveMananger.playerSprite; //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameUI = saveMananger.gameUI; //GameObject.Find("Canvas").GetComponent<GameUI>();
        dorm = saveMananger.dorm; //GameObject.Find("Dorm-servants").GetComponent<Dorm>();
        mapEvents = saveMananger.mapEvents;
        tickManager = saveMananger.tickManager;
        Save save = new Save(player, pos, dorm, mapEvents, tickManager);
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