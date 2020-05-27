using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkippedEvent : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null, btnText = null;
    private string title;
    private SkipEvent skipEvent;

    public void Setup(string title, SkipEvent skipEvent)
    {
        btn = btn != null ? btn : GetComponentInChildren<Button>();
        btnText = btnText != null ? btnText : btn.GetComponentInChildren<TextMeshProUGUI>();
        text = text != null ? text : GetComponentInChildren<TextMeshProUGUI>();
        this.title = title;
        this.skipEvent = skipEvent;
        SetText(skipEvent.Skip);
        btn.onClick.AddListener(Click);
    }

    private void SetText(bool boolVal)
    {
        text.text = $"{title} is skipped:";
        btnText.text = skipEvent.Skip.ToString();
    }

    private void Click() => SetText(skipEvent.ToggleSkip);
}