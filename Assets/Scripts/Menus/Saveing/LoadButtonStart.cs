using System.Collections;
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

        private static FileInfo file;
        private StartSaveSrollListControl saveList;
        private StartLoader loader;

        public void Setup(FileInfo parFile, StartSaveSrollListControl parSaveList, StartLoader parLoader)
        {
            file = parFile;
            saveList = parSaveList;
            loader = parLoader;
            load.onClick.AddListener(() => loader.StartLoading(file));
            del.onClick.AddListener(DeleteSave);

            //SceneManager.sceneLoaded += OnSceneLoaded;
            string cleanedTitleText = file.Name.Substring(0, file.Name.LastIndexOf("."))
                .Replace("-", " ");
            title.text = cleanedTitleText;
        }

        // public void LoadGame() => SceneManager.LoadScene("MainGame");

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
            SaveMananger saveMananger = SaveMananger.Instance;
            string path = file.FullName;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Save save = saveMananger.NewSave;
                save.LoadData(json);
                CanvasMain.GetCanvasMain.Resume();
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
            else
            {
                Debug.LogError("Load failed...");
            }
        }

    }
}