using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private TextMeshProUGUI soundPrecent = null;

    private static float GameVolume
    {
        get => AudioListener.volume;
        set => AudioListener.volume = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        slider = slider != null ? slider : GetComponent<Slider>();
        soundPrecent = soundPrecent != null ? soundPrecent : GetComponentInChildren<TextMeshProUGUI>();
        SetSoundPercent();
        slider.onValueChanged.AddListener(SetSound);
        slider.value = GameVolume;
    }

    private void OnEnable() => SetSoundPercent();

    private void SetSoundPercent()
    {
        if (soundPrecent != null) soundPrecent.text = $"{Mathf.FloorToInt(GameVolume * 100)}%";
    }

    private void SetSound(float val)
    {
        GameVolume = val;
        SetSoundPercent();
    }
}