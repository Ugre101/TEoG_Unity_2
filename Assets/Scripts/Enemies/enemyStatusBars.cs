using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class enemyStatusBars : MonoBehaviour
{
    public TextMeshProUGUI _statusText;

    private Slider slider;
    private BasicChar _char;

    public enum sliderType { hp, wp };
    public sliderType SliderType;
    // Start is called before the first frame update
    public void Init(BasicChar enemy)
    {
        _char = enemy;
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
