using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicSlider : MonoBehaviour
{
    [SerializeField] protected BasicChar basicChar;

    [SerializeField] protected TextMeshProUGUI TextMesh;

    [SerializeField] protected Slider slider;

    [SerializeField] protected bool endSuffix = false;

    [SerializeField] protected string suffix = "";

    protected virtual void Start()
    {
        slider = slider != null ? slider : GetComponent<Slider>();
        TextMesh = TextMesh != null ? TextMesh : GetComponentInChildren<TextMeshProUGUI>();
        basicChar = basicChar != null ? basicChar : PlayerMain.GetPlayer;
    }

    public virtual void Setup(BasicChar who)
    {
        basicChar = who;
        enabled = true;
    }
}