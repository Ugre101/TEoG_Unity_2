using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenuStuff
{
    public class LoadButtonStart : LoadButtonBase
    {
        private StartLoader loader;

        public void Setup(FileInfo parFile, StartLoader parLoader)
        {
            file = parFile;
            loader = parLoader;
            load.onClick.AddListener(Load);
            del.onClick.AddListener(DeleteSave);

            //SceneManager.sceneLoaded += OnSceneLoaded;
            string cleanedTitleText = file.Name.Substring(0, file.Name.LastIndexOf(".")).Replace("-", " ");
            title.text = cleanedTitleText;
        }

        private void Load()
        {
            if (File.Exists(file.FullName))
            {
                loader.StartLoading(file);
            }
            else
            {
                SaveFailed();
            }
        }

        // public void LoadGame() => SceneManager.LoadScene("MainGame");

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            string path = file.FullName;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Save.LoadData(json);
                GameManager.SetCurState(GameState.Free);
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
            else
            {
                Debug.LogError("Load failed...");
            }
        }
    }
}