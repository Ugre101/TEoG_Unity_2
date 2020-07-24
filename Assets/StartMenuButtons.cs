using UnityEngine;
using UnityEngine.UI;

namespace StartMenuStuff
{
    public class StartMenuButtons : MonoBehaviour
    {
        [SerializeField] private Button startBtn = null, loadBtn = null, optionsBtn = null, quitBtn = null;
        [SerializeField] private StartLoader loader = null;
        // Start is called before the first frame update
        private void Start()
        {
            startBtn.onClick.AddListener(StartMenuMananger.StartNewGame);
            quitBtn.onClick.AddListener(Application.Quit);
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}