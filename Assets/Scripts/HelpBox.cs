using UnityEngine;
using UnityEngine.UI;

public abstract class HelpBox : MonoBehaviour
{
    [SerializeField] protected Button okeyBtn = null;
    [SerializeField] protected Toggle toggle = null;

    protected abstract string SaveName { get; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        okeyBtn = okeyBtn != null ? okeyBtn : GetComponentInChildren<Button>();
        toggle = toggle != null ? toggle : GetComponentInChildren<Toggle>();
        okeyBtn.onClick.AddListener(Okey);
        toggle.onValueChanged.AddListener(OnToggle);
        if (PlayerPrefs.HasKey(SaveName))
        {
            if (PlayerPrefs.GetInt(SaveName) == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void OnToggle(bool val) => PlayerPrefs.SetInt(SaveName, val ? 1 : 0);

    protected void Okey() => Destroy(gameObject);
}