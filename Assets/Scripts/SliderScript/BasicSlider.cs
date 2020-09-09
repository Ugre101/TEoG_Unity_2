using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasicSlider : MonoBehaviour
{
    protected BasicChar basicChar;

    [SerializeField] protected TextMeshProUGUI TextMesh;

    [SerializeField] protected Slider slider;

    [SerializeField] protected bool endSuffix = false;

    [SerializeField] protected string suffix = "";
    protected abstract Health Health { get; }

    protected virtual void Start()
    {
        slider = slider != null ? slider : GetComponent<Slider>();
        TextMesh = TextMesh != null ? TextMesh : GetComponentInChildren<TextMeshProUGUI>();
    }

    public virtual void Setup(BasicChar who) => basicChar = who;

    protected virtual void ChangeHealth()
    {
        slider.value = Health.SliderValue;
        TextMesh.text = Health.Status + (endSuffix ? suffix : "");
    }
}