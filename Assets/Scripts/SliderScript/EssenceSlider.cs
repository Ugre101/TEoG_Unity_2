using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceSlider : MonoBehaviour
{
    public ThePrey basicChar;
    public TextMeshProUGUI essValue;
    public Image _image;

    public virtual void Init(ThePrey who)
    {
        basicChar = who;
        this.enabled = true;
    }
}