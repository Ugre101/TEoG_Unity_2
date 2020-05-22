using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour
{
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
    private void Start()
    {
        // PixelButton
        pixelToggle = UgreTools.GetPlayerPrefBool("pixelToggle");
        if (pixelCameraButton != null && pixelPerfectCamera != null)
        {
            pixelCameraButton.onClick.AddListener(TogglePixelCamera);
            pixelText = pixelText != null ? pixelText : pixelCameraButton.GetComponentInChildren<TextMeshProUGUI>();
            SetPixelText();
        }
        if (impButton != null)
        {
            impButton.onClick.AddListener(ToggleImp);
            impText = impText != null ? impText : impButton.GetComponentInChildren<TextMeshProUGUI>();
            SetImpText();
        }
        if (inchBtn != null)
        {
            inchBtn.onClick.AddListener(ToggleInch);
            inchText = inchText != null ? inchText : inchBtn.GetComponentInChildren<TextMeshProUGUI>();
            SetInchText();
        }
        if (poundBtn != null)
        {
            poundBtn.onClick.AddListener(TooglePound);
            poundText = poundText != null ? poundText : poundBtn.GetComponentInChildren<TextMeshProUGUI>();
            SetPoundText();
        }
        if (gallonBtn != null)
        {
            gallonBtn.onClick.AddListener(ToogleGallon);
            gallonText = gallonText != null ? gallonText : gallonBtn.GetComponentInChildren<TextMeshProUGUI>();
            SetGallonText();
        }
        if (currEventFontSize != null) { currEventFontSize.text = Settings.EventLogFontSize.ToString(); }
        if (eventFontUp != null && eventFontDown != null)
        {
            eventFontUp.onClick.AddListener(EventFontSizeUp);
            eventFontDown.onClick.AddListener(EventFontSizeDown);
        }
        setGenders.onClick.AddListener(OpenSetGenders);
        if (toggleSkippedMenu != null && skippedMenu != null)
        {
            toggleSkippedMenu.onClick.AddListener(() => skippedMenu.SetActive(!skippedMenu.activeSelf));
        }
    }
    private void OnEnable()
    {
        skippedMenu.SetActive(false);
        setGendersGameObj.SetActive(false);
    }
    private void SetPixelText() => pixelText.text = $"Pixelperfect: {pixelToggle}";

    private void SetImpText() => impText.text = $"Imperial: {Settings.Imperial}";

    private void SetInchText() => inchText.text = Settings.Inch ? "Inch" : "Metric";

    private void SetPoundText() => poundText.text = Settings.Pound ? "Pound" : "Kg";

    private void SetGallonText() => gallonText.text = Settings.Gallon ? "Gallon" : "Liter";

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
        Settings.ToogleInch();
        UgreTools.SetPlayerPrefBool("Inch", Settings.Inch);
        SetInchText();
        SetImpText();
    }

    private void TooglePound()
    {
        Settings.TooglePound();
        UgreTools.SetPlayerPrefBool("Pound", Settings.Pound);
        SetPoundText();
        SetImpText();
    }

    private void ToogleGallon()
    {
        Settings.ToogleGallon();
        UgreTools.SetPlayerPrefBool("Gallon", Settings.Gallon);
        SetGallonText();
        SetImpText();
    }

    #endregion Toggle Units

    private void EventFontSizeUp() => currEventFontSize.text = Settings.EventLogFontSizeUp.ToString();

    private void EventFontSizeDown() => currEventFontSize.text = Settings.EventLogFontSizeDown.ToString();

    private void OpenSetGenders()
    {
        setGendersGameObj.SetActive(true);
        GameManager.KeyBindsActive = false;
    }
}