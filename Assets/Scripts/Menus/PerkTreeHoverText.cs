using TMPro;
using UnityEngine;

public class PerkTreeHoverText : MonoBehaviour
{
    public static PerkTreeHoverText GetPerkTreeHoverText { get; private set; } = null;

    private static void SetActive(bool isActive) => GetPerkTreeHoverText.gameObject.SetActive(isActive);

    private static TextMeshProUGUI staticInfoText = null, staticReqText = null;
    [SerializeField] private TextMeshProUGUI infoText = null, reqText = null;

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
        staticInfoText = infoText != null ? infoText : GetComponentsInChildren<TextMeshProUGUI>()[0];
        staticReqText = reqText != null ? reqText : GetComponentsInChildren<TextMeshProUGUI>()[1];
        gameObject.SetActive(false);
    }

    public static void Hovering(string text)
    {
        staticInfoText.text = text;
        staticReqText.text = string.Empty;
        SetActive(true);
    }

    public static void Hovering(string infoText, string reqText)
    {
        staticInfoText.text = infoText;
        staticReqText.text = reqText;
        SetActive(true);
    }

    public static void StopHovering() => SetActive(false);
}