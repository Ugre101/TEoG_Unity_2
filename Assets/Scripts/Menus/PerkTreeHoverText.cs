using TMPro;
using UnityEngine;

public class PerkTreeHoverText : MonoBehaviour
{
    public static PerkTreeHoverText GetPerkTreeHoverText { get; private set; } = null;

    private static void SetActive(bool isActive) => GetPerkTreeHoverText.gameObject.SetActive(isActive);

    private static TextMeshProUGUI staticInfoText, staticEffectText, staticReqText;
    [SerializeField] private TextMeshProUGUI infoText = null, effectText = null, reqText = null;

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
        staticEffectText = effectText != null ? effectText : GetComponentsInChildren<TextMeshProUGUI>()[1];
        staticReqText = reqText != null ? reqText : GetComponentsInChildren<TextMeshProUGUI>()[2];
        gameObject.SetActive(false);
    }

    public static void Hovering(string text, string effects)
    {
        staticInfoText.text = text;
        staticEffectText.text = effects;
        staticReqText.text = string.Empty;
        SetActive(true);
    }

    public static void Hovering(string infoText, string effects, string reqText)
    {
        staticInfoText.text = infoText;
        staticEffectText.text = effects;
        staticReqText.text = reqText;
        SetActive(true);
    }

    public static void StopHovering() => SetActive(false);
}