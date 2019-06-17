using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
