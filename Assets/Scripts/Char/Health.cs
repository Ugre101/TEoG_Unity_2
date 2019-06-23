using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField]
    protected float _current, _max;
    public Health(float max)
    {
        _max = max;
        _current = _max;
    }
    public float Current
    {
        get { return Mathf.Round(_current); }
        //set { _current += Mathf.Clamp(value,-_current,_max -_current); }
    }
    public bool TakeDmg(float dmg)
    {
        _current -= Mathf.Clamp(dmg, 0, _current);
        updateSlider?.Invoke();
        return _current <= 0 ? true : false;
    }
    public void Gain(float gain)
    {
        _current += Mathf.Clamp(gain, 0, _max - _current);
        updateSlider?.Invoke();
    }
    public void FullGain()
    {
        _current = _max;
    }
    public float Slider()
    {
        return _current / _max;
    }
    public string Status()
    {
        return $"{_current}/{_max}";
    }

    public delegate void UpdateSlider();

    public static event UpdateSlider updateSlider;

    public void manualSliderUpdate()
    {
        updateSlider();
    }

}