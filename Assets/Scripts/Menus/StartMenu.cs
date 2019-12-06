using UnityEngine;

namespace StartMenuStuff
{
    public class StartMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.SleepChildren(transform.GetChild(0));
        }

        public void QuitGame()
        {
            Debug.Log("Quit game");
            Application.Quit();
        }
    }
}