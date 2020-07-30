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
        [SerializeField] private StartLoadProgress loadProgress = null;

        private void Start() => DontDestroyOnLoad(gameObject);
        public void StartNewGame()
        {
            StartCoroutine(StartGame());
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
                loadProgress.SetProgress(asyncLoad.progress);
                yield return null;
            }
            // wait so everything is loaded
            yield return new WaitForEndOfFrame();
            SaveMananger saveMananger = SaveMananger.Instance;
            Save save = saveMananger.NewSave;
            save.LoadData(json);
            Debug.Log(json);
            GameManager.SetCurState(GameState.Free);
            Destroy(gameObject);
        }

        private IEnumerator StartGame()
        {
            loadingScreen.StartLoad();

            AsyncOperation async = SceneManager.LoadSceneAsync("MainGame");
            while (!async.isDone)
            {
                loadProgress.SetProgress(async.progress);

                yield return null;
            }
            StartCoroutine(UgreTools.WaitAFrame());
            GameManager.SetCurState(GameState.Intro);
        }

    }
}