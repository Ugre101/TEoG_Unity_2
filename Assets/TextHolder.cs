using TMPro;
using UnityEngine;

public class TextHolder : MonoBehaviour
{
    [SerializeField] private Transform container = null;
    [SerializeField] private TextMeshProUGUI boldText = null;
    [SerializeField] private TextMeshProUGUI normalText = null;

    public void AddBoldText(string text) => Instantiate(boldText, container).text = text;

    public void AddText(string text)
    {
        TextMeshProUGUI newText = Instantiate(normalText, container);
        newText.text = text;
    }
}