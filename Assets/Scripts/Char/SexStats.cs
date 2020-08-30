using UnityEngine;

[System.Serializable]
public class SexStats
{
    [SerializeField] private ExpSystem dickExp = new ExpSystem(0);
    [SerializeField] private ExpSystem vagExp = new ExpSystem(0);
    [SerializeField] private ExpSystem handExp = new ExpSystem(0);
    [SerializeField] private ExpSystem mouthExp = new ExpSystem(0);
    [SerializeField] private ExpSystem analExp = new ExpSystem(0);
    [SerializeField] private ExpSystem breastsExp = new ExpSystem(0);
    [SerializeField] private CharStats maxArousal = new CharStats(100);
    private int currOrgasm = 0;
    public float Arousal { get; private set; } = 0;
    public int Orgasms { get; private set; } = 0;
    public int SessionOrgasm { get; private set; } = 0;

    public bool CanDrain => currOrgasm > 0;

    public void Drained() => currOrgasm--;

    /// <summary>Returns true if orgasm ocurs</summary>
    /// <param name="gain"></param>
    public bool GainArousal(float gain)
    {
        Arousal += gain;
        bool org = Arousal > MaxArousal.Value;
        if (org)
        {
            Orgasms++;
            SessionOrgasm++;
            currOrgasm++;
            Arousal -= MaxArousal.Value;
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

    public float ArousalSlider => Arousal / MaxArousal.Value;

    public string ArousalStatus => $"{Arousal}/{MaxArousal}";

    public ExpSystem DickExp => dickExp;
    public ExpSystem VagExp => vagExp;
    public ExpSystem HandExp => handExp;
    public ExpSystem MouthExp => mouthExp;
    public ExpSystem AnalExp => analExp;
    public ExpSystem BreastsExp => breastsExp;

    public CharStats MaxArousal => maxArousal;

    public delegate void ArousalChange();

    public event ArousalChange ArousalChangeEvent;

    public delegate void Orgasmed();

    public event Orgasmed OrgasmedEvent;
}

public static class SexStatsExtensions
{
    public static int MaxOrgasm(this BasicChar basicChar)
    {
        int baseOrg = 1;
        baseOrg += Mathf.FloorToInt(basicChar.Stats.End / 15);
        // TODO perks and stuff
        return baseOrg;
    }

    public static bool CanOrgasmMore(this BasicChar basicChar) => basicChar.SexStats.SessionOrgasm < basicChar.MaxOrgasm();
}