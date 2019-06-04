using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour
{
    [Header("Pixel perfect")]
    public PixelPerfectCamera pixelPerfectCamera;

    public Button pixelCameraButton;
    private bool pixelToggle = false;
    private TextMeshProUGUI pixelText;

    [Header("Vore")]
    public Button voreButton;

    private bool voreToggle;
    private TextMeshProUGUI voreText;

    [Header("Imperial")]
    public Button impButton;

    public Settings settings;
    private TextMeshProUGUI impText;
    private bool impToggle;

    // Start is called before the first frame update
    private void Start()
    {
        // PixelButton
        if (PlayerPrefs.HasKey("pixelToggle"))
        {
            pixelToggle = PlayerPrefs.GetInt("pixelToggle") == 1 ? true : false;
        }
        if (pixelCameraButton != null && pixelPerfectCamera != null)
        {
            pixelCameraButton.onClick.AddListener(TogglePixelCamera);
            pixelText = pixelCameraButton.GetComponentInChildren<TextMeshProUGUI>();
            if (pixelText != null)
            {
                pixelText.text = $"Pixelperfect: {pixelToggle}";
            }
        }
        // VoreButton
        if (PlayerPrefs.HasKey("voreToggle"))
        {
            voreToggle = PlayerPrefs.GetInt("voreToggle") == 1 ? true : false;
        }
        if (voreButton != null)
        {
            voreButton.onClick.AddListener(ToggleVore);
            voreText = voreButton.GetComponentInChildren<TextMeshProUGUI>();
            if (voreText != null)
            {
                voreText.text = $"Vore: {voreToggle}";
            }
        }
        // Imperial units
        if (PlayerPrefs.HasKey("Imperial"))
        {
            impToggle = PlayerPrefs.GetInt("Imperial") == 1 ? true : false;
        }
        if (impButton != null)
        {
            impButton.onClick.AddListener(ToggleImp);
            impText = impButton.GetComponentInChildren<TextMeshProUGUI>();
            if (impText != null)
            {
                impText.text = $"Imperial: {impToggle}";
            }
        }
    }

    private void TogglePixelCamera()
    {
        pixelToggle = pixelToggle ? false : true;
        PlayerPrefs.SetInt("pixelToggle", pixelToggle ? 1 : 0);
        pixelPerfectCamera.enabled = pixelToggle;
        if (pixelText != null)
        {
            pixelText.text = $"Pixelperfect: {pixelToggle}";
        }
    }

    private void ToggleVore()
    {
        voreToggle = voreToggle ? false : true;
        PlayerPrefs.SetInt("voreToggle", voreToggle ? 1 : 0);
        if (voreText != null)
        {
            voreText.text = $"Vore: {voreToggle}";
        }
    }

    private void ToggleImp()
    {
        impToggle = settings.ToogleImp();
        PlayerPrefs.SetInt("Imperial", impToggle ? 1 : 0);
        if (impText != null)
        {
            impText.text = $"Imperial: {impToggle}";
        }
    }
}