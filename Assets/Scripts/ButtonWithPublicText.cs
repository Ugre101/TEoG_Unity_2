using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithPublicText : Button
{
    [SerializeField] private TextMeshProUGUI text = null;

    public string BtnText
    {
        get
        {
            GetTextMeshProUGUI();
            return text.text;
        }
        set
        {
            GetTextMeshProUGUI();
            text.text = value;
        }
    }

    private void GetTextMeshProUGUI()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}