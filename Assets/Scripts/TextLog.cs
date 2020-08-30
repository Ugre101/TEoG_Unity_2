using TMPro;
using UnityEngine;

public class TextLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI log = null, credits = null;

    // Start is called before the first frame update
    private void Start() => log = log != null ? log : GetComponentInChildren<TextMeshProUGUI>();

    private void OnEnable() => credits.text = string.Empty;

    public void SetText(string text) => log.text = text;

    public void AddText(string text) => log.text += text;

    public void SetCredits(string text) => credits.text = text;
}