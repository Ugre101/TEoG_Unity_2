public class SexStats
{
    private readonly float maxArousal = 100f;
    private int currOrgasm = 0;
    public float Arousal { get; private set; } = 0;
    public int Orgasms { get; private set; } = 0;
    public int SessionOrgasm { get; private set; } = 0;

    public bool CanDrain => currOrgasm > 0;

    public void Drained() => currOrgasm--;

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
            OrgasmedEvent?.Invoke();
        }
        ArousalChangeEvent?.Invoke();
        return org;
    }

    public void Reset()
    {
        Arousal = 0;
        SessionOrgasm = 0;
        currOrgasm = 0;
    }

    public float ArousalSlider => Arousal / maxArousal;

    public string ArousalStatus => $"{Arousal}/{maxArousal}";

    public delegate void ArousalChange();

    public event ArousalChange ArousalChangeEvent;

    public delegate void Orgasmed();

    public event Orgasmed OrgasmedEvent;

    public void ManualArousalUpdate() => ArousalChangeEvent?.Invoke();
}