using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FluidSliders : MonoBehaviour
{
    public playerMain player;
    public TextMeshProUGUI statusText;
    public Settings convert;
    protected Slider slider;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            GetComponent<FluidSliders>().enabled = false;
        }
    }
}
