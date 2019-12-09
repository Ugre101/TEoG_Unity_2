using TMPro;
using UnityEngine;

public class ShowServant : MonoBehaviour
{
    [SerializeField]
    private ThePrey who;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;

    public void Init(ThePrey whom)
    {
        who = whom;
        Title.text = who.FullName;
        Desc.text = CharDesc();
    }

    private string CharDesc()
    {
        string desc = $"{who.Gender} {who.Race}";
        return desc;
    }
}