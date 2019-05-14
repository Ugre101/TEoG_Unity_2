using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sliderStatusUpdater : MonoBehaviour
{
    // Public
    public playerMain _char;

    public TextMeshProUGUI _statusText;

    public enum sliderType { hp, wp };

    public sliderType SliderType;

    // private
    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            GetComponent<sliderStatusUpdater>().enabled = false;
        }
        switch (SliderType)
        {
            case sliderType.hp:
                BasicChar.updateSlider += changeHealth;
                break;

            case sliderType.wp:
                BasicChar.updateSlider += changeWill;
                break;
        }
        _char.manualSliderUpdate();
    }

    private void OnDisable()
    {
        switch (SliderType)
        {
            case sliderType.hp:
                BasicChar.updateSlider -= changeHealth;
                break;

            case sliderType.wp:
                BasicChar.updateSlider -= changeWill;
                break;
        }
    }

    // Update is called once per frame

    private void changeHealth()
    {
        slider.value = _char.hpSlider();
        if (_statusText != null)
        {
            _statusText.text = _char.hpStatus();
        }
    }

    private void changeWill()
    {
        slider.value = _char.wpSlider();
        if (_statusText != null)
        {
            _statusText.text = _char.wpStatus();
        }
    }
}