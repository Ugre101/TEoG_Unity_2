using TMPro;
using UnityEngine;

public class TextLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI log = null;

    // Start is called before the first frame update
    private void Start() => log = log != null ? log : GetComponent<TextMeshProUGUI>();

    public void SetText(string text) => log.text = text;

    public void AddText(string text) => log.text += text;
}