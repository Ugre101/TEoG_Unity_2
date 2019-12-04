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

    [SerializeField]
    private TextMeshProUGUI pixelText;

    [Header("Imperial")]
    public Button impButton;

    private TextMeshProUGUI impText;

    // Start is called before the first frame update
    private void Start()
    {
        // PixelButton
        if (PlayerPrefs.HasKey("pixelToggle"))
        {
            pixelToggle = PlayerPrefs.GetInt("pixelToggle") == 1;
        }
        if (pixelCameraButton != null && pixelPerfectCamera != null)
        {
            pixelCameraButton.onClick.AddListener(TogglePixelCamera);
            if (pixelText == null)
            {
                pixelText = pixelCameraButton.GetComponentInChildren<TextMeshProUGUI>();
            }
            pixelText.text = $"Pixelperfect: {pixelToggle}";
        }
        if (impButton != null)
        {
            impButton.onClick.AddListener(ToggleImp);
            impText = impButton.GetComponentInChildren<TextMeshProUGUI>();
            if (impText != null)
            {
                impText.text = $"Imperial: {Settings.Imperial}";
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
        Settings.ToogleImp();
        PlayerPrefs.SetInt("Imperial", Settings.Imperial ? 1 : 0);
        if (impText != null)
        {
            impText.text = $"Imperial: {Settings.Imperial}";
        }
    }
}