using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceSlider : MonoBehaviour
{
    [SerializeField]
    protected BasicChar basicChar = null;

    [SerializeField]
    protected TextMeshProUGUI essValue = null;

    [SerializeField]
    protected Image _image = null;

    public virtual void Init(BasicChar who)
    {
        basicChar = who;
        this.enabled = true;
    }
}