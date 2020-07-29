using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SortButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    public Button Btn
    {
        get
        {
            if (btn == null)
            {
                btn = GetComponent<Button>();
            }
            return btn;
        }
    }

    public TextMeshProUGUI Text
    {
        get
        {
            if (text == null)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
            }
            return text;
        }
    }

    public string StringText { get => Text.text; set => Text.text = value; }

    public void Setup(string btnText, UnityAction onClickFunc)
    {
        StringText = btnText;
        Btn.onClick.AddListener(onClickFunc);
    }
}