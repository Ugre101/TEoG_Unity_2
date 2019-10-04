using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VoreToggleBtn : MonoBehaviour
{
    public bool Status = false;
    private Button btn;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleVore);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Vore: {Status}";
    }
    private void ToggleVore()
    {
        Status = !Status;
        text.text = $"Vore: {Status}";
    }
}
