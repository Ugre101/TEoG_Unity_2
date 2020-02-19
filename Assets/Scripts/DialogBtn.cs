using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBtn : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    public Button Button => btn;
    [SerializeField] private TextMeshProUGUI btnText = null;
    public TextMeshProUGUI ButtonText => btnText;

    public Button Setup(string btnTitle)
    {
        btnText.text = btnTitle;
        return Button;
    }
}