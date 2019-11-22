using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private SaveMananger saveMananger;
    // Short commands
    private playerMain player => saveMananger.player;
    private Transform pos => saveMananger.playerSprite;
    private Dorm dorm => saveMananger.dorm;
    private GameUI gameUI => saveMananger.gameUI;
    private MapEvents mapEvents => saveMananger.mapEvents;
    private TickManager tickManager => saveMananger.tickManager;
    [SerializeField]
    private Home home;
    public TextMeshProUGUI title;
    public Button load, del;

    private string _mainPath;

    public void Setup(SaveMananger parSaveMananger,string parTitle)
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
            Save save = new Save(player, pos, dorm, mapEvents, tickManager, home);
            save.LoadData(json);
        }else
        {
            Debug.Log("Error load failed...");
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