using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour
{
    [SerializeField] private Transform subOptionsContainer = null;

    [Header("Pixel perfect")]
    [SerializeField] private PixelPerfectCamera pixelPerfectCamera = null;

    [SerializeField] private Button pixelCameraButton = null;
    private bool pixelToggle = false;

    [SerializeField] private TextMeshProUGUI pixelText = null;

    [Header("Imperial")]
    [SerializeField] private Button impButton = null;

    [SerializeField] private Button inchBtn = null, poundBtn = null, gallonBtn = null;

    [SerializeField] private TextMeshProUGUI impText = null, inchText = null, poundText = null, gallonText = null;

    [Header("Gender")]
    [SerializeField] private Button setGenders = null;

    [SerializeField] private GameObject setGendersGameObj = null;

    [Header("FontSizes")]
    [SerializeField] private Button eventFontUp = null;

    [SerializeField] private Button eventFontDown = null;

    [SerializeField] private TextMeshProUGUI currEventFontSize = null;

    [Header("Skipped events")]
    [SerializeField] private Button toggleSkippedMenu = null;

    [SerializeField] private GameObject skippedMenu = null;

    // Start is called before the first frame update
    [Header("Toggle extra menus")]
    [SerializeField] private GameObject fetishesMenu = null, gameUIMenu = null;

    [SerializeField] private Button fetishBtn = null, gameUIBtn = null;

    private void Start()
    {
        // Got rid of null checks as I probably want it to break if something is missing.
        // PixelButton
        pixelToggle = UgreTools.GetPlayerPrefBool("pixelToggle");

        pixelCameraButton.onClick.AddListener(TogglePixelCamera);
        pixelText = pixelText != null ? pixelText : pixelCameraButton.GetComponentInChildren<TextMeshProUGUI>();
        SetPixelText();

        impButton.onClick.AddListener(ToggleImp);
        impText = impText != null ? impText : impButton.GetComponentInChildren<TextMeshProUGUI>();
        SetImpText();

        inchBtn.onClick.AddListener(ToggleInch);
        inchText = inchText != null ? inchText : inchBtn.GetComponentInChildren<TextMeshProUGUI>();
        SetInchText();

        poundBtn.onClick.AddListener(TooglePound);
        poundText = poundText != null ? poundText : poundBtn.GetComponentInChildren<TextMeshProUGUI>();
        SetPoundText();

        gallonBtn.onClick.AddListener(ToogleGallon);
        gallonText = gallonText != null ? gallonText : gallonBtn.GetComponentInChildren<TextMeshProUGUI>();
        SetGallonText();

        currEventFontSize.text = Settings.EventLogFont.Size.ToString();

        eventFontUp.onClick.AddListener(EventFontSizeUp);
        eventFontDown.onClick.AddListener(EventFontSizeDown);

        setGenders.onClick.AddListener(OpenCloseSetGenders);

        toggleSkippedMenu.onClick.AddListener(() => ToggleSubMenu(skippedMenu));

        fetishBtn.onClick.AddListener(() => ToggleSubMenu(fetishesMenu));

        gameUIBtn.onClick.AddListener(() => ToggleSubMenu(gameUIMenu));
    }

    private void OnEnable() => subOptionsContainer.SleepChildren();

    private void SetPixelText() => pixelText.text = $"Pixelperfect: {pixelToggle}";

    private void SetImpText() => impText.text = $"Imperial: {Settings.Imperial}";

    private void SetInchText() => inchText.text = Settings.Inch.Imperial ? "Inch" : "Metric";

    private void SetPoundText() => poundText.text = Settings.Pound.Imperial ? "Pound" : "Kg";

    private void SetGallonText() => gallonText.text = Settings.Gallon.Imperial ? "Gallon" : "Liter";

    private void TogglePixelCamera()
    {
        pixelToggle = !pixelToggle;
        UgreTools.SetPlayerPrefBool("pixelToggle", pixelToggle);
        pixelPerfectCamera.enabled = pixelToggle;
        SetPixelText();
    }

    #region Toggle Units

    private void ToggleImp()
    {
        Settings.ToogleImperial();
        UgreTools.SetPlayerPrefBool("Imperial", Settings.Imperial);
        SetImpText();
        SetInchText();
        SetPoundText();
        SetGallonText();
    }

    private void ToggleInch()
    {
        UgreTools.SetPlayerPrefBool("Inch", Settings.Inch.Toggle);
        SetInchText();
        SetImpText();
    }

    private void TooglePound()
    {
        UgreTools.SetPlayerPrefBool("Pound", Settings.Pound.Toggle);
        SetPoundText();
        SetImpText();
    }

    private void ToogleGallon()
    {
        UgreTools.SetPlayerPrefBool("Gallon", Settings.Gallon.Toggle);
        SetGallonText();
        SetImpText();
    }

    #endregion Toggle Units

    private void EventFontSizeUp() => currEventFontSize.text = Settings.EventLogFont.Up.ToString();

    private void EventFontSizeDown() => currEventFontSize.text = Settings.EventLogFont.Down.ToString();

    private void OpenCloseSetGenders()
    {
        ToggleSubMenu(setGendersGameObj);
        GameManager.KeyBindsActive = !setGendersGameObj.activeSelf;
    }

    private void ToggleSubMenu(GameObject toToggle)
    {
        if (toToggle.activeSelf)
        {
            toToggle.SetActive(false);
        }
        else
        {
            subOptionsContainer.SleepChildren(toToggle.transform);
        }
    }
}