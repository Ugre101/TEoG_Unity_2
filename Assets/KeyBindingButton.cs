using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingButton : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI title = null;

    [SerializeField]
    private TextMeshProUGUI curKey = null;

    [SerializeField]
    private Button btn = null;

    public TextMeshProUGUI Title => title;
    public TextMeshProUGUI CurKey => curKey;
    public Button Button => btn;
}