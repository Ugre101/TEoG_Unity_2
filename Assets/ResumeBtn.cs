using UnityEngine;
using UnityEngine.UI;

public class ResumeBtn : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private CanvasMain canvasMain = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        btn.onClick.AddListener(canvasMain.Resume);
    }
}