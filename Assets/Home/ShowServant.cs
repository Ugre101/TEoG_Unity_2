using TMPro;
using UnityEngine;

public class ShowServant : MonoBehaviour
{
    private BasicChar basicChar;

    [SerializeField] private TextMeshProUGUI title = null, desc = null;

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