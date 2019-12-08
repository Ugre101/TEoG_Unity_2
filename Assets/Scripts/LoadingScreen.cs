using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenuStuff
{
    public class LoadingScreen : MonoBehaviour
    {
        public TextMeshProUGUI progresBar;

        public void StartIntro()
        {
            gameObject.SetActive(true);
            StartCoroutine(StartGame());
            SceneManager.sceneLoaded += OnceStarted;
        }

        public void StartLoad()
        {
            gameObject.SetActive(true);
        }

        private void OnceStarted(Scene scene, LoadSceneMode mode)
        {
            GameUI gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
            gameUI.Intro();
            SceneManager.sceneLoaded -= OnceStarted;
        }

        private IEnumerator StartGame()
        {
            yield return null;
            AsyncOperation async = SceneManager.LoadSceneAsync("MainGame");
            while (!async.isDone)
            {
                progresBar.text = $"Loading progess: {async.progress * 100}%";

                yield return null;
            }
        }
    }
}