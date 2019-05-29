public class SexStats
{
    private float arousal;
    private float maxArousal = 100f;
    private int orgasms = 0;
    private int sessionOrgasm = 0;
    public float Arousal { get { return arousal; } }
    public int Orgasms { get { return orgasms; } }
    public int SessionOrgasm { get { return sessionOrgasm; } }

    public bool GainArousal(float gain)
    {
        arousal += gain;
        if (arousal > maxArousal)
        {
            orgasms++;
            sessionOrgasm++;
            arousal -= maxArousal;
            arousalChange();
            return true;
        }
        else
        {
            arousalChange();
            return false;
        }
    }

    public void Reset()
    {
        sessionOrgasm = 0;
        //orgasms = 0;
        arousal = 0;
    }

    public float ArousalSlider()
    {
        return arousal / maxArousal;
    }

    public string ArousalStatus()
    {
        return $"{arousal}/{maxArousal}";
    }

    public delegate void ArousalChange();

    public static event ArousalChange arousalChange;

    public void ManualArousalUpdate()
    {
        arousalChange();
    }
}