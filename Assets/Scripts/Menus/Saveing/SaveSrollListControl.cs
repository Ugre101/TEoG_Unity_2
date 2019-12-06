using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSrollListControl : MonoBehaviour
{
    public SaveMananger saveMananger;
    public LoadButton exload;
    public GameObject container;

    private DirectoryInfo _dirInfo;
    private FileInfo[] _fileInfo;
    private string Path => SaveSettings.SaveFolder;

    private void Start()
    {
        _dirInfo = new DirectoryInfo(Path);
        RefreshSaveList();
    }

    public void RefreshSaveList()
    {
        _fileInfo = _dirInfo.GetFiles("*.json").
            OrderByDescending(f => f.LastWriteTime).ToArray();
        // Destroy buttons
        if (transform.childCount > 0)
        {
            transform.KillChildren(container.transform);
        }
        // Add buttons
        foreach (FileInfo f in _fileInfo)
        {
            LoadButton newButton = Instantiate(exload, container.transform);
            newButton.Setup(saveMananger, f,this);
        }
    }
}