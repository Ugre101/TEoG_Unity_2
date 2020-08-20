using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowServant : MonoBehaviour
{
    private DormMate dormMate;

    [SerializeField] private TextMeshProUGUI title = null, desc = null;
    [field: SerializeField] public Button Btn { get; private set; }

    private void Start() => Btn = Btn != null ? Btn : GetComponent<Button>();

    public Button Init(DormMate dormMate)
    {
        this.dormMate = dormMate;
        title.text = dormMate.BasicChar.Identity.FullName;
        desc.text = dormMate.BasicChar.Summary();
        return Btn;
    }
}