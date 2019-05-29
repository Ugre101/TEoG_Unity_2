using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EssenceSlider : MonoBehaviour
{
    public BasicChar _char;
    public enum EssenceType { Masc, Femi};
    public EssenceType _ess;
    public TextMeshProUGUI _essValue;
    public Image _image;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (_image == null)
        {
            this.enabled = false;
        }
        switch (_ess)
        {
            case EssenceType.Masc:
                Essence.essenceSlider += changeMasc;
                _char.Masc.ManualUpdate();
                break;
            case EssenceType.Femi:
                Essence.essenceSlider += changeFemi;
                _char.Femi.ManualUpdate();
                break;
        }
    }
    private void OnDisable()
    {
        switch (_ess)
        {
            case EssenceType.Masc:
                Essence.essenceSlider -= changeMasc;
                break;
            case EssenceType.Femi:
                Essence.essenceSlider -= changeFemi;
                break;
        }
    }
    private void changeMasc()
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        _essValue.text = _char.Masc.Amount.ToString();
    }
    private void changeFemi()
    {
        _essValue.text = _char.Femi.Amount.ToString();
    }
}
