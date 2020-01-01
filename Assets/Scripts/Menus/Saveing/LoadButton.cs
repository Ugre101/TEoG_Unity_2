using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private SaveMananger saveMananger = SaveMananger.Instance;
    private FileInfo file;

    // Short commands
    private CanvasMain GetGameUI => CanvasMain.GetCanvasMain;

    public TextMeshProUGUI title;
    public Button load, del;
    public SaveSrollListControl saveList;

    public void Setup(FileInfo parFile, SaveSrollListControl parSaveList)
    {
        file = parFile;
        saveList = parSaveList;
        string cleanedTitleText = file.Name.Substring(0, file.Name.LastIndexOf("."))
                .Replace("-", " ");
        title.text = cleanedTitleText;
        // Set buttons
        load.onClick.AddListener(LoadGame);
        del.onClick.AddListener(DeleteSave);
    }

    public void LoadGame()
    {
        string path = file.FullName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Save save = saveMananger.NewSave;
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
        string path = file.FullName;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        saveList.RefreshSaveList();
    }
}