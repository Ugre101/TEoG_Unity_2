using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CumBar : FluidSliders
{
    private void OnEnable()
    {
        SexualFluid.FluidSlider += CumChange;
        if (player.Balls.Count > 0)
        {
           player.Balls[0].Fluid.ManualSlider();
        }
    }
   
    private void OnDisable()
    {
        SexualFluid.FluidSlider -= CumChange;
    }

    private void CumChange()
    {
        slider.value = player.CumSlider;
        if (statusText != null)
        {
            statusText.text = convert.LorGal(player.Balls.CumTotal()/1000);
        }
    }
}
