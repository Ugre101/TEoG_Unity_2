using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Essence 
{
    [SerializeField]
    protected float _amount;
    public virtual float Amount { get { return Mathf.Floor(_amount); }}
    public Essence()
    {
        _amount = 0;
    }
    public void Gain(float toGain)
    {
        _amount += Mathf.Max(0,toGain);
        essenceSlider?.Invoke();
    }
    public float Lose(float toLose)
    {
        float lose = Mathf.Min(_amount, toLose);
        _amount -= lose;
        essenceSlider?.Invoke();
        return lose;
    }
    public void ManualUpdate()
    {
        essenceSlider?.Invoke();
    }
    public delegate void EssenceSlider();

    public static event EssenceSlider essenceSlider;
}
