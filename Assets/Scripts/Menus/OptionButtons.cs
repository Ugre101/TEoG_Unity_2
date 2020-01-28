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

    [SerializeField] private TextMeshProUGUI pixelText;

    [Header("Imperial")]
    [SerializeField] private Button impButton = null;

    [SerializeField] private TextMeshProUGUI impText = null;

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
            pixelText = pixelText != null ? pixelText : pixelCameraButton.GetComponentInChildren<TextMeshProUGUI>();
            pixelText.text = $"Pixelperfect: {pixelToggle}";
        }
        if (impButton != null)
        {
            impButton.onClick.AddListener(ToggleImp);
            impText = impText != null ? impText : impButton.GetComponentInChildren<TextMeshProUGUI>();
            impText.text = $"Imperial: {Settings.Imperial}";
        }
    }

    private void TogglePixelCamera()
    {
        pixelToggle = pixelToggle ? false : true;
        PlayerPrefs.SetInt("pixelToggle", pixelToggle ? 1 : 0);
        pixelPerfectCamera.enabled = pixelToggle;
        pixelText.text = $"Pixelperfect: {pixelToggle}";
    }

    private void ToggleImp()
    {
        Settings.ToogleImp();
        PlayerPrefs.SetInt("Imperial", Settings.Imperial ? 1 : 0);
        impText.text = $"Imperial: {Settings.Imperial}";
    }
}