using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FluidSliders : MonoBehaviour
{
    public PlayerMain player;
    public TextMeshProUGUI statusText;
    protected Slider slider;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
}
