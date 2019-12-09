using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicSlider : MonoBehaviour
{
    [SerializeField]
    protected ThePrey basicChar;

    [SerializeField]
    protected TextMeshProUGUI TextMesh;

    [SerializeField]
    protected Slider slider;

    [SerializeField]
    protected bool endSuffix = false;

    [SerializeField]
    protected string suffix = "";

    private void Awake()
    {
        if (slider == null) { slider = GetComponent<Slider>(); }
        if (TextMesh == null) { TextMesh = GetComponentInChildren<TextMeshProUGUI>(); }
        if (basicChar == null) { enabled = false; }
    }

    public virtual void Setup(ThePrey who)
    {
        enabled = true;
        basicChar = who;
    }
}