using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AfterBattleButtonBase : MonoBehaviour
{
    [SerializeField] protected Button btn = null;

    [SerializeField] protected TextMeshProUGUI title = null;

    public void BaseSetup()
    {
        btn = btn != null ? btn : GetComponent<Button>();
    }
    protected abstract void Func();
    public void HideBtn() => gameObject.SetActive(false);
}
