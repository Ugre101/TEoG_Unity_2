public class SexStats
{
    private readonly float maxArousal = 100f;
    private int currOrgasm = 0;
    public float Arousal { get; private set; } = 0;
    public int Orgasms { get; private set; } = 0;
    public int SessionOrgasm { get; private set; } = 0;

    public bool CanDrain => currOrgasm > 0;
    public void Drained () { currOrgasm--; }
    public bool GainArousal(float gain)
    {
        Arousal += gain;
        bool org = Arousal > maxArousal;
        if (org)
        {
            Orgasms++;
            SessionOrgasm++;
            currOrgasm++;
            Arousal -= maxArousal;
            orgasmed?.Invoke();
        }
        arousalChange?.Invoke();
        return org;
    }

    public void Reset()
    {
        SessionOrgasm = 0;
        currOrgasm = 0;
    }

    public float ArousalSlider()
    {
        return Arousal / maxArousal;
    }

    public string ArousalStatus()
    {
        return $"{Arousal}/{maxArousal}";
    }

    public delegate void ArousalChange();

    public static event ArousalChange arousalChange;

    public delegate void Orgasmed();

    public static event Orgasmed orgasmed;

    public void ManualArousalUpdate()
    {
        arousalChange?.Invoke();
    }
}