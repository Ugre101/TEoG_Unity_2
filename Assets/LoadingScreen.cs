using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI progresBar;
    public IntroScript intro;
    private void OnEnable()
    {
        StartCoroutine(StartGame());
    }
    public void PassOver()
    {
        playerMain player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        player.firstName = intro.FirstName;
        player.lastName = intro.LastName;
        player.Vore.Active = intro.Vore;
    }
    private IEnumerator StartGame()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync("MainGame");
        async.allowSceneActivation = false;
        Debug.Log("Pro: " + async.progress);
        while (!async.isDone)
        {
            progresBar.text = $"Loading progess: {async.progress * 100}%";
            if (async.progress >= 0.9f)
            {
                progresBar.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}