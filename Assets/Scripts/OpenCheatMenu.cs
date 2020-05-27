using UnityEngine;
using UnityEngine.UI;

public class OpenCheatMenu : MonoBehaviour
{
    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private Button btn = null;
    [SerializeField] private GameObject cheatMenu;
    private int timesClicked = 0;

    // Start is called before the first frame update
    private void Start()
    {
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }

    private void Click()
    {
        timesClicked++;
        if (timesClicked > 7 && cheatMenu != null)
        {
            canvasMain.ResumePause(cheatMenu);
        }
    }
}