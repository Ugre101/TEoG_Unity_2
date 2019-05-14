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
