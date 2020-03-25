using TMPro;
using UnityEngine;

public class PerkTreeHoverText : MonoBehaviour
{
    public static PerkTreeHoverText GetPerkTreeHoverText { get; private set; } = null;

    private static void SetActive(bool isActive) => GetPerkTreeHoverText.gameObject.SetActive(isActive);

    private static TextMeshProUGUI staticTitleText, staticInfoText, staticEffectText, staticReqText;
    [SerializeField] private TextMeshProUGUI titleText = null, infoText = null, effectText = null, reqText = null;

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
        staticTitleText = titleText != null ? titleText : GetComponentsInChildren<TextMeshProUGUI>()[0];
        staticInfoText = infoText != null ? infoText : GetComponentsInChildren<TextMeshProUGUI>()[1];
        staticEffectText = effectText != null ? effectText : GetComponentsInChildren<TextMeshProUGUI>()[2];
        staticReqText = reqText != null ? reqText : GetComponentsInChildren<TextMeshProUGUI>()[3];
        gameObject.SetActive(false);
    }

    public static void Hovering(string title, string desc, string effects)
    {
        staticTitleText.text = title;
        staticInfoText.text = desc;
        staticEffectText.text = effects;
        staticReqText.text = string.Empty;
        SetActive(true);
    }

    public static void Hovering(string title, string infoText, string effects, string reqText)
    {
        staticTitleText.text = title;
        staticInfoText.text = infoText;
        staticEffectText.text = effects;
        staticReqText.text = reqText;
        SetActive(true);
    }

    public static void StopHovering() => SetActive(false);
}