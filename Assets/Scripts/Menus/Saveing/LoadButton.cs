using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private SaveMananger saveMananger;

    // Short commands
    private PlayerMain Player => saveMananger.player;

    private Transform GetPos => saveMananger.playerSprite;
    private Dorm GetDorm => saveMananger.dorm;
    private GameUI GetGameUI => saveMananger.gameUI;
    private MapEvents GetMapEvents => saveMananger.mapEvents;
    private TickManager GetTickManager => saveMananger.tickManager;

    [SerializeField]
    private Home home = null;

    [SerializeField]
    private EventLog eventLog = null;

    public TextMeshProUGUI title;
    public Button load, del;

    private string _mainPath;

    public void Setup(SaveMananger parSaveMananger, string parTitle)
    {
        saveMananger = parSaveMananger;
        title.text = parTitle;
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
        string path = CurrentPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Save save = new Save(Player, GetPos, GetDorm, GetMapEvents, GetTickManager, home, eventLog);
            save.LoadData(json);
        }
        else
        {
            Debug.Log("Error load failed...");
        }
        GetGameUI.Resume();
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