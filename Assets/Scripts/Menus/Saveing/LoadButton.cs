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
    private TickManager tickManager;
    public TextMeshProUGUI title;
    public Button load, del;

    private string _mainPath;

    // Start is called before the first frame update
    private void Start()
    {
        saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
        player = saveMananger.player;
        pos = saveMananger.playerSprite;
        gameUI = saveMananger.gameUI;
        dorm = saveMananger.dorm;
        mapEvents = saveMananger.mapEvents;
        tickManager = saveMananger.tickManager;
        _mainPath = Application.persistentDataPath + "/Game_Save/";
        if (!Directory.Exists(_mainPath))
        {
            Directory.CreateDirectory(_mainPath);
        }
        // Set buttons
        load.onClick.AddListener(LoadGame);
        del.onClick.AddListener(DeleteSave);
    }

    public void LoadGame()
    {
        Save save = new Save(player, pos, dorm, mapEvents, tickManager);
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
        string currPath = title.text;
        return _mainPath + currPath + ".json";
    }
}