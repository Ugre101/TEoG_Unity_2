using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicSlider : MonoBehaviour
{
    public BasicChar basicChar;
    public TextMeshProUGUI TextMesh;
    protected Slider slider;
    [SerializeField]
    protected bool endSuffix = false;
    [SerializeField]
    protected string suffix = "";
    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (slider == null || basicChar == null)
        {
            enabled = false;
        }
    }
}