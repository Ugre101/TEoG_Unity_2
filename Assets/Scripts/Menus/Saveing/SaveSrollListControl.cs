using System.IO;
using UnityEngine;

public class SaveSrollListControl : MonoBehaviour
{
    public SaveMananger saveMananger;
    public LoadButton exload;
    public GameObject container;

    private DirectoryInfo _dirInfo;
    private FileInfo[] _fileInfo, _fileInfoOld;
    private string _path;

    // Start is called before the first frame update
    private void Start()
    {
        _path = Application.persistentDataPath + "/Game_Save/";
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        _dirInfo = new DirectoryInfo(_path);
        _fileInfo = _dirInfo.GetFiles("*.json");
        _fileInfoOld = _fileInfo;
        RefreshSaveList();
    }

    // Update is called once per frame
    private void Update()
    {
        _fileInfo = _dirInfo.GetFiles("*.json");
        if (_fileInfo.Length != _fileInfoOld.Length)
        {
            _fileInfoOld = _fileInfo;
            RefreshSaveList();
        }
        // Refresh maybe?
    }

    public void RefreshSaveList()
    {
        // Destroy buttons
        if (transform.childCount > 0)
        {
            transform.KillChildren(container.transform);
        }
        // Add buttons
        foreach (FileInfo f in _fileInfo)
        {
            LoadButton newButton = Instantiate(exload, container.transform);
            newButton.Setup(saveMananger, f.Name.Split('.')[0].Trim());
            newButton.transform.SetAsFirstSibling();
        }
    }
}