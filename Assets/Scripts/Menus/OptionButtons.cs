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

    [Header("Imperial")]
    public Button impButton;

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

    private void ToggleImp()
    {
        impToggle = Settings.ToogleImp();
        PlayerPrefs.SetInt("Imperial", impToggle ? 1 : 0);
        if (impText != null)
        {
            impText.text = $"Imperial: {impToggle}";
        }
    }
}