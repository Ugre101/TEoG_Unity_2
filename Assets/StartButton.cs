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
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}