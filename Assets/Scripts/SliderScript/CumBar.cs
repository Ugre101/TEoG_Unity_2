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
        if (player.SexualOrgans.Balls.Count > 0)
        {
           player.SexualOrgans.Balls[0].Fluid.ManualSlider();
        }
    }
   
    private void OnDisable()
    {
        SexualFluid.FluidSlider -= CumChange;
    }

    private void CumChange()
    {
        slider.value = player.SexualOrgans.CumSlider;
        if (statusText != null)
        {
            statusText.text = convert.LorGal(player.SexualOrgans.Balls.CumTotal()/1000);
        }
    }
}
