using System.IO;
using System.Linq;
using UnityEngine;

namespace StartMenuStuff
{
    public class StartSaveSrollListControl : MonoBehaviour
    {
        [SerializeField] private LoadButtonStart loadButton = null;
        [SerializeField] private Transform container = null;
        [SerializeField] private StartLoader loader = null;
        [SerializeField] private TimedPopupText timedPopup = null;

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
            LoadButtonBase.SaveDeleted += RefreshSaveList;
            LoadButtonBase.FailEvent += Failed;
        }

        public void RefreshSaveList()
        {
            _fileInfo = _dirInfo.GetFiles("*.json").
            OrderBy(f => f.LastWriteTime).ToArray();
            // Destroy buttons
            if (transform.childCount > 0)
            {
                container.transform.KillChildren();
            }
            // Add buttons
            foreach (FileInfo f in _fileInfo)
            {
                LoadButtonStart newButton = Instantiate(loadButton, container);
                newButton.Setup(f, loader);
                newButton.transform.SetAsFirstSibling();
            }
        }

        public void Failed()
        {
            TimedPopupText popupText = Instantiate(timedPopup, transform);
            popupText.Setup("Failed to load...");
        }
    }
}