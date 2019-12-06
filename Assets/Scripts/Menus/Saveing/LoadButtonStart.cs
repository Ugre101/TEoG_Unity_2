using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StartMenuStuff
{
    public class LoadButtonStart : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title = null;

        [SerializeField]
        private Button load = null, del = null;

        private FileInfo file;
        private StartSaveSrollListControl saveList;
        public void Setup(FileInfo parFile,StartSaveSrollListControl parSaveList)
        {
            file = parFile;
            saveList = parSaveList;
            load.onClick.AddListener(LoadGame);
            del.onClick.AddListener(DeleteSave);
            SceneManager.sceneLoaded += OnSceneLoaded;
            string cleanedTitleText = file.Name.Substring(0, file.Name.LastIndexOf("."))
                .Replace("-", " ");
            title.text = cleanedTitleText;
        }

        public void LoadGame() => SceneManager.LoadScene("MainGame");

        public void DeleteSave()
        {
            string path = file.FullName;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            saveList.RefreshSaveList();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SaveMananger saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
            string path = file.FullName;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Save save = saveMananger.NewSave;
                save.LoadData(json);
                saveMananger.gameUI.Resume();
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
            else
            {
                Debug.LogError("Load failed...");
            }
        }
    }
}