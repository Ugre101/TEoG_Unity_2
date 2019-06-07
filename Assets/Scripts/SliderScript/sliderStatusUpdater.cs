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
                Health.updateSlider += changeHealth;
                _char.HP.manualSliderUpdate();
                break;

            case sliderType.wp:
                Health.updateSlider += changeWill;
                _char.WP.manualSliderUpdate();
                break;
        }
    }

    private void OnDisable()
    {
        switch (SliderType)
        {
            case sliderType.hp:
                Health.updateSlider -= changeHealth;
                break;

            case sliderType.wp:
                Health.updateSlider -= changeWill;
                break;
        }
    }

    // Update is called once per frame

    private void changeHealth()
    {
        slider.value = _char.HP.Slider();
        if (_statusText != null)
        {
            _statusText.text = _char.HP.Status();
        }
    }

    private void changeWill()
    {
        slider.value = _char.WP.Slider();
        if (_statusText != null)
        {
            _statusText.text = _char.WP.Status();
        }
    }
}