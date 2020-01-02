using UnityEngine;
using UnityEngine.UI;

public class DevResume : MonoBehaviour
{
    [SerializeField]
    private CanvasMain canvasMain = null;

    [SerializeField]
    private Button btn = null;

    // Start is called before the first frame update
    private void Start()
    {
        if (Debug.isDebugBuild)
        {
            canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
            btn = btn != null ? btn : GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(canvasMain.Resume);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}