using UnityEngine;

namespace StartMenuStuff
{
    public class StartMenuCanvas : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.SleepChildren(transform.GetChild(0));
            ScreenSetting.Load();
        }

        public void QuitGame()
        {
            Debug.Log("Quit game");
            Application.Quit();
        }
    }
}