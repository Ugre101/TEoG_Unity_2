using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingButton : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI curKey;

    [SerializeField]
    private Button btn;

    public TextMeshProUGUI Title => title;
    public TextMeshProUGUI CurKey => curKey;
    public Button Button => btn;
}