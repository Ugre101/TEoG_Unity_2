using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceSlider : MonoBehaviour
{
    public BasicChar basicChar;
    public TextMeshProUGUI essValue;
    public Image _image;

    public virtual void Init(BasicChar who)
    {
        basicChar = who;
        this.enabled = true;
    }
}