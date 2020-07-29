using UnityEngine;

public class FluidSliderTypeHandler : MonoBehaviour
{
    [SerializeField] private GameObject spheres = null, sliders = null;

    private void OnEnable()
    {
        if (GameUISettings.FluidSliderType == GameUISettings.SliderType.Sphere)
        {
            spheres.SetActive(true);
            sliders.SetActive(false);
        }
        else
        {
            spheres.SetActive(false);
            sliders.SetActive(true);
        }
    }
}