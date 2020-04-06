using UnityEngine;

public class SexStats
{
    private readonly float maxArousal = 100f;
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
}

public static class SexStatsExtensions
{
    public static int MaxOrgasm(this BasicChar basicChar)
    {
        int baseOrg = 1;
        baseOrg += Mathf.FloorToInt(basicChar.Stats.End / 20);
        // TODO perks and stuff
        return baseOrg;
    }

    public static bool CanOrgasmMore(this BasicChar basicChar) => basicChar.SexStats.SessionOrgasm < basicChar.MaxOrgasm();
}