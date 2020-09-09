using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class FluidSliders : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI statusText = null;
    [SerializeField] protected Slider slider = null;
    protected static BasicChar Player => PlayerMain.Player;

    public abstract void Setup();
}