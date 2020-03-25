using UnityEngine;
using UnityEngine.UI;

public class HelpBox : MonoBehaviour
{
    [SerializeField] private Button okeyBtn = null;
    [SerializeField] private Toggle toggle = null;

    private const string saveName = "GameUIHelp";

    // Start is called before the first frame update
    private void Start()
    {
        okeyBtn = okeyBtn != null ? okeyBtn : GetComponentInChildren<Button>();
        toggle = toggle != null ? toggle : GetComponentInChildren<Toggle>();
        okeyBtn.onClick.AddListener(Okey);
        toggle.onValueChanged.AddListener(OnToggle);
        if (PlayerPrefs.HasKey(saveName))
        {
            if (PlayerPrefs.GetInt(saveName) == 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnToggle(bool val) => PlayerPrefs.SetInt(saveName, val ? 1 : 0);

    private void Okey() => Destroy(this.gameObject);
}