using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowServant : MonoBehaviour
{
    private BasicChar basicChar;

    [SerializeField] private TextMeshProUGUI title = null, desc = null;
    [field: SerializeField] public Button Btn { get; private set; }

    private void Start() => Btn = Btn != null ? Btn : GetComponent<Button>();

    public void Init(BasicChar basicChar)
    {
        this.basicChar = basicChar;
        title.text = basicChar.Identity.FullName;
        desc.text = basicChar.Summary();
    }

    private string CharDesc()
    {
        string desc = $"{basicChar.Gender} {basicChar.Race}";
        return desc;
    }
}