using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChangeFontSize : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textLog = null;
    [SerializeField] protected Button upBtn = null, downBtn = null;
    protected abstract LogFontSize CurrFont { get; }

    protected void DownFunc() => textLog.fontSize = CurrFont.Down;

    protected void UpFunc() => textLog.fontSize = CurrFont.Up;

    protected virtual void Start()
    {
        upBtn.onClick.AddListener(UpFunc);
        downBtn.onClick.AddListener(DownFunc);
    }

    protected virtual void OnEnable() => textLog.fontSize = CurrFont.Size;
}