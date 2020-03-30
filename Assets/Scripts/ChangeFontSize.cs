using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChangeFontSize : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textLog = null;
    [SerializeField] protected Button upBtn = null, downBtn = null;
    protected abstract float CurrSize { get; }
    protected abstract float DownSized { get; }

    protected void DownFunc() => textLog.fontSize = DownSized;

    protected abstract float UpSized { get; }

    protected void UpFunc() => textLog.fontSize = UpSized;

    protected virtual void Start()
    {
        upBtn.onClick.AddListener(UpFunc);
        downBtn.onClick.AddListener(DownFunc);
    }

    protected virtual void OnEnable()
    {
        textLog.fontSize = CurrSize;
    }
}
