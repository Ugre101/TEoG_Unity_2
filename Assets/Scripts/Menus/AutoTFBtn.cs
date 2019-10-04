using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoTFBtn : MonoBehaviour
{
    public bool Status = true;
    private Button btn;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(AutoTF);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Auto TF: {Status}";
    }
    private void AutoTF()
    {
        Status = !Status;
        text.text = $"Auto TF: {Status}";
    }
}
