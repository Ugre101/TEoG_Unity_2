using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoader : MonoBehaviour
{
    private FileInfo file;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartLoading(FileInfo parFile)
    {
        file = parFile;
        StartCoroutine(AsyncLoadGame(parFile));
        // SceneManager.LoadScene("MainGame");
        // SceneManager.sceneLoaded += OnSceneLoaded;
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

    public IEnumerator AsyncLoadGame(FileInfo file)
    {
        string path = file.FullName;
        if (File.Exists(path))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");
            string json = File.ReadAllText(path);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            // wait so everything is loaded
            yield return new WaitForEndOfFrame();
            SaveMananger saveMananger = GameObject.FindGameObjectWithTag("SaveMenu").GetComponent<SaveMananger>();
            Save save = saveMananger.NewSave;
            save.LoadData(json);
            Debug.Log(json);
            saveMananger.gameUI.Resume();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Load failed...");
        }
    }
}