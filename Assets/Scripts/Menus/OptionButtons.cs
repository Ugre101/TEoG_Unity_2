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

    [SerializeField] private Button inchBtn = null, poundBtn = null;

    [SerializeField] private TextMeshProUGUI impText = null, inchText = null, poundText = null;

    [Header("Gender")]
    [SerializeField] private Button setGenders = null;

    [SerializeField] private GameObject setGendersGameObj = null;

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
        setGenders.onClick.AddListener(OpenSetGenders);
    }

    private void SetPixelText() => pixelText.text = $"Pixelperfect: {pixelToggle}";

    private void SetImpText() => impText.text = $"Inch & pound: {Settings.Imperial}";

    private void SetInchText() => inchText.text = Settings.Inch ? "Inch" : "Metric";

    private void SetPoundText() => poundText.text = Settings.Pound ? "Pound" : "Kg";

    private void TogglePixelCamera()
    {
        pixelToggle = !pixelToggle;
        UgreTools.SetPlayerPrefBool("pixelToggle", pixelToggle);
        pixelPerfectCamera.enabled = pixelToggle;
        SetPixelText();
    }

    private void ToggleImp()
    {
        Settings.ToogleImperial();
        UgreTools.SetPlayerPrefBool("Imperial", Settings.Imperial);
        SetImpText();
        SetInchText();
        SetPoundText();
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

    private void OpenSetGenders()
    {
        setGendersGameObj.SetActive(true);
        GameManager.KeyBindsActive = false;
    }
}