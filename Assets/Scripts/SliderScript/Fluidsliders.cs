using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FluidSliders : MonoBehaviour
{
    [SerializeField]
    protected PlayerMain player = null;

    [SerializeField]
    protected TextMeshProUGUI statusText = null;

    [SerializeField]
    protected Slider slider = null;

    protected void Start()
    {
        slider = slider != null ? slider : GetComponent<Slider>();
        player = player != null ? player : PlayerMain.GetPlayer;
        Setup();
    }

    public virtual void Setup()
    {
    }
}