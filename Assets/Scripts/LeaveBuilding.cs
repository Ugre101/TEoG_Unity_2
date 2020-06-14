using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaveBuilding : MonoBehaviour
{
    [SerializeField] private KeyCode hotKey = KeyCode.Escape;
    [SerializeField] private Button btn = null;

    [SerializeField] private CanvasMain canvasMain = null;
    [SerializeField] private TextMeshProUGUI altHotKeyText = null;
    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
        btn.onClick.AddListener(canvasMain.Resume);
        if (altHotKeyText != null)
        {
            altHotKeyText.text = hotKey.ToString();
        }
    }
    private void Update()
    {
       if(Input.GetKeyDown(hotKey))
        {
            btn.onClick?.Invoke();
        }
    }
}