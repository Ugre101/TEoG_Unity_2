public enum OrganType
{
    Dick,
    Balls,
    Vagina,
    Boobs
};
public class SexualOrgan
{

    public OrganType Type;
    private int _baseSize;
    private int _lastBase;
    private float _currSize;
    private bool _isDirty = true;

    public virtual float Size
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                // Calc
                _lastBase = _baseSize;
                _isDirty = false;
                _currSize = CalcSize();
            }
            return _currSize;
        }
    }
    public string Looks()
    {
        switch (Type)
        {
            case OrganType.Balls:
                string balls = $"a pair of {Size}cm wide balls";
                return balls;
            case OrganType.Dick:
                string dick = $"a {Size}cm long dick";
                return dick;
            case OrganType.Vagina:
                string vagina = "";
                return vagina;
            case OrganType.Boobs:
                string boobs = "";
                return boobs;
            default:
                return "error";
        }
    }
    private float CalcSize()
    {
        float FinalValue = _baseSize;
        return FinalValue;
    }
}