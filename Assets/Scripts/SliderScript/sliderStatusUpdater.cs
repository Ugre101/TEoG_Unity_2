using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicSlider : MonoBehaviour
{
    [SerializeField]
    protected BasicChar basicChar;

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

    public virtual void Setup(BasicChar who)
    {
        enabled = true;
        basicChar = who;
    }
}