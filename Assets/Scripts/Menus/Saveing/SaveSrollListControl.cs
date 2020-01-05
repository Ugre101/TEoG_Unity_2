using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSrollListControl : MonoBehaviour
{
    [SerializeField]
    private LoadButton exload = null;

    [SerializeField]
    private GameObject container = null;

    [SerializeField]
    private TimedPopupText timedPopup = null;

    private DirectoryInfo _dirInfo;
    private FileInfo[] _fileInfo;
    private string Path => SaveSettings.SaveFolder;

    private void Start()
    {
        _dirInfo = new DirectoryInfo(Path);
        RefreshSaveList();
        SaveMananger.SavedEvent += RefreshSaveList;
        LoadButtonBase.SaveDeleted += RefreshSaveList;
        LoadButtonBase.FailEvent += Failed;
    }

    public void RefreshSaveList()
    {
        _fileInfo = _dirInfo.GetFiles("*.json").
            OrderByDescending(f => f.LastWriteTime).ToArray();
        // Destroy buttons
        if (transform.childCount > 0)
        {
            container.transform.KillChildren();
        }
        // Add buttons
        foreach (FileInfo f in _fileInfo)
        {
            LoadButton newButton = Instantiate(exload, container.transform);
            newButton.Setup(f);
        }
    }

    public void Failed()
    {
        TimedPopupText popupText = Instantiate(timedPopup, transform);
        popupText.Setup("Failed to load...");
    }
}