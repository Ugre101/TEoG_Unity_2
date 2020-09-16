using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void StartGame()
    {

    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => SceneManager.sceneLoaded -= OnSceneLoaded;
}