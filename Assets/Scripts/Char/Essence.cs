using UnityEngine;

[System.Serializable]
public class Essence
{
    [SerializeField]
    protected float _amount;

    public float Amount => Mathf.Floor(_amount);
    public string StringAmount => _amount > 999 ? Mathf.Round(_amount / 1000) + "k" : _amount.ToString();

    public Essence() => _amount = 0;

    public Essence(float parAmount) => _amount = parAmount;

    public void Gain(float toGain)
    {
        _amount += Mathf.Max(0, toGain);
        EssenceSliderEvent?.Invoke();
    }

    public float Lose(float toLose)
    {
        float lose = Mathf.Min(_amount, toLose);
        _amount -= lose;
        EssenceSliderEvent?.Invoke();
        return lose;
    }

    public void ManualUpdate() => EssenceSliderEvent?.Invoke();

    public delegate void EssenceSlider();

    public event EssenceSlider EssenceSliderEvent;
}