using System.IO;
using System.Linq;
using UnityEngine;

namespace StartMenuStuff
{
    public class StartSaveSrollListControl : MonoBehaviour
    {
        public LoadButtonStart loadButton;
        public Transform container;
        public StartLoader loader;

        private DirectoryInfo _dirInfo;
        private FileInfo[] _fileInfo;
        private string SaveFolder => SaveSettings.SaveFolder;

        // Start is called before the first frame update
        private void Start()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
            _dirInfo = new DirectoryInfo(SaveFolder);
            RefreshSaveList();
        }

        public void RefreshSaveList()
        {
            _fileInfo = _dirInfo.GetFiles("*.json").
            OrderBy(f => f.LastWriteTime).ToArray();
            // Destroy buttons
            if (transform.childCount > 0)
            {
                transform.KillChildren(container);
            }
            // Add buttons
            foreach (FileInfo f in _fileInfo)
            {
                LoadButtonStart newButton = Instantiate(loadButton, container);
                newButton.Setup(f, this, loader);
                newButton.transform.SetAsFirstSibling();
            }
        }
    }
}