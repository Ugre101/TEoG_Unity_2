using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField]
    protected float _current;

    protected int _max;

    public Health(int parMax)
    {
        _max = parMax;
        _current = parMax;
    }

    public void RaiseMax(int toRaise) => _max += Mathf.Abs(toRaise);

    public void LowerMax(int toLower) => _max -= Mathf.Abs(toLower);

    public float Current => Mathf.Round(_current);

    public bool TakeDmg(float dmg)
    {
        _current = Mathf.Max(0, _current - dmg);
        UpdateSliderEvent?.Invoke();
        if (_current <= 0)
        {
            DeadEvent?.Invoke();
            return true;
        }
        return false;
    }

    public void Gain(float gain)
    {
        _current += Mathf.Clamp(gain, 0, _max - _current);
        UpdateSliderEvent?.Invoke();
    }

    public void FullGain() => _current = _max;

    public float SliderValue => _current / _max;

    public string Status => $"{_current} / {_max}";

    public delegate void UpdateSlider();

    public event UpdateSlider UpdateSliderEvent;

    public delegate void Dead();

    public event Dead DeadEvent;

    public void ManualSliderUpdate() => UpdateSliderEvent?.Invoke();
}