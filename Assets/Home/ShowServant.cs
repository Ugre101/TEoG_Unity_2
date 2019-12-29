using TMPro;
using UnityEngine;

public class ShowServant : MonoBehaviour
{
    [SerializeField]
    private BasicChar who;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;

    public void Init(BasicChar whom)
    {
        who = whom;
        Title.text = who.Identity.FullName;
        Desc.text = CharDesc();
    }

    private string CharDesc()
    {
        string desc = $"{who.Gender} {who.Race}";
        return desc;
    }
}