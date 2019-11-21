using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI progresBar;

    private void OnEnable()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync("MainGame");
        while (!async.isDone)
        {
            Debug.Log("Pro: " + async.progress);
            progresBar.text = $"Loading progess: {async.progress * 100}%";

            yield return null;
        }
    }
}