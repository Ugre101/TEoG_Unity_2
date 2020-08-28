using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class EssenceSlider : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI essValue = null;
    [SerializeField] protected Image _image = null;
    protected BasicChar basicChar = null;
    protected abstract Essence Ess { get; }

    public virtual void Init(BasicChar who)
    {
        basicChar = who;
        enabled = true;
        Ess.ChangeEvent += ChangeEss;
        ChangeEss();
        started = true;
    }

    protected bool started = false;

    private void OnEnable()
    {
        if (started)
        {
            Ess.ChangeEvent += ChangeEss;
            ChangeEss();
        }
    }

    private void OnDisable() => Ess.ChangeEvent -= ChangeEss;

    protected abstract void ChangeEss();
}