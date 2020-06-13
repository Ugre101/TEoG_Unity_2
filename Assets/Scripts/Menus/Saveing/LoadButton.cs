using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LoadButtonBase : MonoBehaviour
{
    protected FileInfo file;

    // Short commands
    [SerializeField] protected TextMeshProUGUI title = null;

    [SerializeField] protected Button load = null, del = null;

    public virtual void Setup(FileInfo fileInfo)
    {
    }

    public void DeleteSave()
    {
        string path = file.FullName;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        SaveDeleted?.Invoke();
    }

    public delegate void DeletedSave();

    public static event DeletedSave SaveDeleted;

    public delegate void Failed();

    public static event Failed FailEvent;

    public void SaveFailed() => FailEvent?.Invoke();
}

public class LoadButton : LoadButtonBase
{
    public override void Setup(FileInfo parFile)
    {
        file = parFile;
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
            SaveMananger.Instance.NewSave.LoadData(json);
            CanvasMain.GetCanvasMain.Resume();
        }
        else
        {
            
            Debug.Log("Error load failed...");
            SaveFailed();
        }
    }
}