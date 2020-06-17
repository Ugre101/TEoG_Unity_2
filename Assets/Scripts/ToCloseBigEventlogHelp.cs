using TMPro;
using UnityEngine;

public class ToCloseBigEventlogHelp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

    private void OnEnable()
    {
        text = text != null ? text : GetComponent<TextMeshProUGUI>();
        KeyBind eventKey = KeyBindings.EventKey;
        text.text = $"To close double click or press {eventKey.Key}";
        if (eventKey.AltKey != KeyCode.None)
        {
            text.text += $" / {eventKey.AltKey}";
        }
    }
}