using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private GameObject fetishesMenu = null, gameUIMenu = null, dormTitlesMenu = null;

    [SerializeField] private Button fetishBtn = null, gameUIBtn = null, dormTitlesBtn = null;

    private void Start()
    {
        // Got rid of null checks as I probably want it to break if something is missing.
        // PixelButton
        pixelToggle = UgreTools.GetPlayerPrefBool("pixelToggle");

        // Set toggle buttons
        SetToogleBtn(pixelCameraButton, TogglePixelCamera, ref pixelText, SetPixelText);
        SetToogleBtn(impButton, ToggleImp, ref impText, SetImpText);
        SetToogleBtn(inchBtn, ToggleInch, ref inchText, SetInchText);
        SetToogleBtn(poundBtn, TooglePound, ref poundText, SetPoundText);
        SetToogleBtn(gallonBtn, ToogleGallon, ref gallonText, SetGallonText);

        currEventFontSize.text = Settings.EventLogFont.Size.ToString();

        eventFontUp.onClick.AddListener(EventFontSizeUp);
        eventFontDown.onClick.AddListener(EventFontSizeDown);

        setGenders.onClick.AddListener(OpenCloseSetGenders);

        toggleSkippedMenu.onClick.AddListener(() => ToggleSubMenu(skippedMenu));

        fetishBtn.onClick.AddListener(() => ToggleSubMenu(fetishesMenu));

        gameUIBtn.onClick.AddListener(() => ToggleSubMenu(gameUIMenu));

        dormTitlesBtn.onClick.AddListener(() => ToggleSubMenu(dormTitlesMenu));
    }

    private static void SetToogleBtn(Button btn, UnityAction func, ref TextMeshProUGUI btnText, UnityAction setTextFunc)
    {
        btn.onClick.AddListener(func);
        btnText = btnText != null ? btnText : btn.GetComponentInChildren<TextMeshProUGUI>();
        setTextFunc?.Invoke();
    }

    private void OnEnable() => subOptionsContainer.SleepChildren();

    private void SetPixelText() => pixelText.text = $"Pixelperfect: {pixelToggle}";

    private void SetImpText() => impText.text = $"Imperial: {Measurements.Imperial}";

    private void SetInchText() => inchText.text = Measurements.Inch.Imperial ? "Inch" : "Metric";

    private void SetPoundText() => poundText.text = Measurements.Pound.Imperial ? "Pound" : "Kg";

    private void SetGallonText() => gallonText.text = Measurements.Gallon.Imperial ? "Gallon" : "Liter";

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
        Measurements.ToogleImperial();
        UgreTools.SetPlayerPrefBool("Imperial", Measurements.Imperial);
        SetImpText();
        SetInchText();
        SetPoundText();
        SetGallonText();
    }

    private void ToggleInch()
    {
        UgreTools.SetPlayerPrefBool("Inch", Measurements.Inch.Toggle);
        SetInchText();
        SetImpText();
    }

    private void TooglePound()
    {
        UgreTools.SetPlayerPrefBool("Pound", Measurements.Pound.Toggle);
        SetPoundText();
        SetImpText();
    }

    private void ToogleGallon()
    {
        UgreTools.SetPlayerPrefBool("Gallon", Measurements.Gallon.Toggle);
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
            toToggle.SetActive(false);
        else
            subOptionsContainer.SleepChildren(toToggle.transform);
    }
}