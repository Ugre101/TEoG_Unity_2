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
        if (player.SexualOrgans.Lactating && player.SexualOrgans.Boobs.Count > 0)
        {
            player.SexualOrgans.Boobs[0].Fluid.ManualSlider();
        }
    }
    private void OnDisable()
    {
        SexualFluid.FluidSlider -= MilkChange;
    }
    private void MilkChange()
    {
        slider.value = player.SexualOrgans.MilkSlider;
        if (statusText != null)
        {
            statusText.text = convert.LorGal(player.SexualOrgans.Boobs.MilkTotal()/1000);
        }
    }
}
