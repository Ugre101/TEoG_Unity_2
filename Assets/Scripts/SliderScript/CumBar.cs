using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CumBar : MonoBehaviour
{
    public playerMain _player;
    public TextMeshProUGUI _statusText;
    public Settings _convert;
    private Slider _slider;
    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            GetComponent<CumBar>().enabled = false;
        }
        SexualFluid.fluidSlider += CumChange;
        if (_player.Balls.Count > 0)
        {
            _player.Balls[0].Fluid.ManualSlider();
        }
    }
    private void OnDisable()
    {
        SexualFluid.fluidSlider -= CumChange;
    }

    private void CumChange()
    {
        _slider.value = _player.CumSlider();
        if (_statusText != null)
        {
            _statusText.text = _convert.LorGal(_player.CumTotal()/1000);
        }
    }
}
