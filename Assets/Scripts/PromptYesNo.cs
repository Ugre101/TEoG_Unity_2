using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromptYesNo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private Button yesBtn = null, noBtn = null;

    /// <summary> No = destroy itself </summary>
    public void Setup(UnityAction yesFunc)
    {
        BindYes(yesFunc);
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(SelfDestroy);
    }

    public void Setup(UnityAction yesFunc, string title)
    {
        Setup(yesFunc);
        this.title.text = title;
    }

    private void Setup(UnityAction yesFunc, UnityAction noFunc)
    {
        Setup(yesFunc);
        noBtn.onClick.AddListener(noFunc);
    }

    public void Setup(UnityAction yesFunc, UnityAction noFunc, string title)
    {
        Setup(yesFunc, noFunc);
        this.title.text = title;
    }

    private void SelfDestroy() => Destroy(gameObject);

    private void BindYes(UnityAction yesFunc)
    {
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(yesFunc);
        yesBtn.onClick.AddListener(SelfDestroy);
    }
}