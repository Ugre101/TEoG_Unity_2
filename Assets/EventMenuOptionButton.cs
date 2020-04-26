using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventMenuOptionButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI text = null;

    public Button Setup(string title)
    {
        btn = btn != null ? btn : GetComponent<Button>();
        text = text != null ? text : GetComponentInChildren<TextMeshProUGUI>();
        text.text = title;
        return btn;
    }
}