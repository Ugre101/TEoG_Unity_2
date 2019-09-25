using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkTreeBasicBtn : MonoBehaviour
{
    public bool taken = false;
    public playerMain player;
    public TextMeshProUGUI amount;
    private Button btn;

    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Use);
    }

    public virtual void Use()
    {
    }
}