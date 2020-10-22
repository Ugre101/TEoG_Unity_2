using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SortButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    public Button Btn => btn;

    public TextMeshProUGUI Text => text;

    public string StringText { get => Text.text; set => Text.text = value; }

    public void Setup(UnityAction onClickFunc) => Btn.onClick.AddListener(onClickFunc);
    public void Setup(string btnText, UnityAction onClickFunc)
    {
        StringText = btnText;
        Setup(onClickFunc);
    }
}