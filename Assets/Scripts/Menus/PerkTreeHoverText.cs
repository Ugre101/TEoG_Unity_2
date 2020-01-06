using TMPro;
using UnityEngine;

public class PerkTreeHoverText : MonoBehaviour
{
    public static PerkTreeHoverText GetPerkTreeHoverText { get; private set; } = null;
    private static TextMeshProUGUI textBox = null;

    [SerializeField]
    private TextMeshProUGUI setTextBox = null;

    private void Start()
    {
        if (GetPerkTreeHoverText == null)
        {
            GetPerkTreeHoverText = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (textBox == null && setTextBox != null)
        {
            textBox = setTextBox;
        }
        else
        {
            textBox = GetComponentInChildren<TextMeshProUGUI>();
        }
        gameObject.SetActive(false);
    }

    public static void Hovering(string text)
    {
        GetPerkTreeHoverText.gameObject.SetActive(true);
        textBox.text = text;
    }

    public static void StopHovering() => GetPerkTreeHoverText.gameObject.SetActive(false);
}