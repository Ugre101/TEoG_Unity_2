using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenuStuff
{
    public class StartLoader : MonoBehaviour
    {
        private FileInfo file;

        [SerializeField] private GameObject canvas = null;

        [SerializeField] private LoadingScreen loadingScreen = null;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartLoading(FileInfo parFile)
        {
            file = parFile;
            StartCoroutine(AsyncLoadGame());
        }

        public IEnumerator AsyncLoadGame()
        {
            string path = file.FullName;

            canvas.transform.SleepChildren();
            loadingScreen.StartLoad();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");
            string json = File.ReadAllText(path);
            while (!asyncLoad.isDone)
            {
                loadingScreen.progresBar.text = $"Loading progess: {asyncLoad.progress * 100}%";
                yield return null;
            }
            // wait so everything is loaded
            yield return new WaitForEndOfFrame();
            SaveMananger saveMananger = SaveMananger.Instance;
            Save save = saveMananger.NewSave;
            save.LoadData(json);
            Debug.Log(json);
            CanvasMain.GetCanvasMain.Resume();
            Destroy(gameObject);
        }
    }
}