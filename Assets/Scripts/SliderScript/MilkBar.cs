using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MilkBar : FluidSliders
{

    private void OnEnable()
    {
        SexualFluid.FluidSlider += MilkChange;
        if (player.Lactating && player.Boobs.Count > 0)
        {
            player.Boobs[0].Fluid.ManualSlider();
        }
    }
    private void OnDisable()
    {
        SexualFluid.FluidSlider -= MilkChange;
    }
    private void MilkChange()
    {
        slider.value = player.MilkSlider();
        if (statusText != null)
        {
            statusText.text = convert.LorGal(player.Boobs.MilkTotal()/1000);
        }
    }
}
