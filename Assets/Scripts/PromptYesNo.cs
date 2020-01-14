using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromptYesNo : MonoBehaviour
{
    [SerializeField] private Button YesBtn = null, NoBtn = null;

    /// <summary> No = destroy itself </summary>
    public void Setup(UnityAction yesFunc)
    {
        BindYes(yesFunc);

        NoBtn.onClick.RemoveAllListeners();
        NoBtn.onClick.AddListener(SelfDestroy);
    }

    public void Setup(UnityAction yesFunc, UnityAction noFunc)
    {
        BindYes(yesFunc);

        NoBtn.onClick.RemoveAllListeners();
        NoBtn.onClick.AddListener(noFunc);
        NoBtn.onClick.AddListener(SelfDestroy);
    }

    public void SelfDestroy() => Destroy(gameObject);

    private void BindYes(UnityAction yesFunc)
    {
        YesBtn.onClick.RemoveAllListeners();
        YesBtn.onClick.AddListener(yesFunc);
        YesBtn.onClick.AddListener(SelfDestroy);
    }
}