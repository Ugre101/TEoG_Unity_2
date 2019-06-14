using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceSlider : MonoBehaviour
{
    public BasicChar basicChar;
    public TextMeshProUGUI essValue;
    public Image _image;

    private void Awake()
    {
        if (_image == null || basicChar == null || essValue == null)
        {
            this.enabled = false;
        }
    }
}