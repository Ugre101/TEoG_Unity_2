using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MilkBar : MonoBehaviour
{
    public playerMain _player;
    public TextMeshProUGUI _text;
    public Settings _convert;
    private Slider _slider;
    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            GetComponent<MilkBar>().enabled = false;
        }
        SexualFluid.fluidSlider += MilkChange;
        if (_player.Lactating && _player.Boobs.Count > 0)
        {
            _player.Boobs[0].Fluid.ManualSlider();
        }
    }
    private void OnDisable()
    {
        SexualFluid.fluidSlider -= MilkChange;
    }
    private void MilkChange()
    {
        _slider.value = _player.MilkSlider();
        if (_text != null)
        {
            _text.text = _convert.LorGal(_player.MilkTotal()/1000);
        }
    }
}
