using System.Collections.Generic;
using System.Text;

public class DisplayMod
{
    public DisplayMod(Mod parMod)
    {
        this.Mods = new List<Mod>() { parMod };
        Source = parMod.Source;
        if (parMod is IDuration parDur)
        {
            iDur = parDur;
        }
    }

    public List<Mod> Mods { get; }
    private readonly IDuration iDur;
    public int Duration => iDur != null ? iDur.Duration : 0;

    public string Source { get; }

    public string Desc()
    {
        StringBuilder builder = new StringBuilder();
        Mods.ForEach(m =>
        {
            if (m is TempHealthMod thm)
            {
                builder.Append(string.Format("{0} {1} {2} {3}", thm.HealthType, thm.Value, thm.ModType, thm.Source));
            }
            else if (m is TempStatMod tsm)
            {
                builder.Append(string.Format("{0} {1} {2}", tsm.Value, tsm.ModType, tsm.Source));
            }
        });
        return builder.ToString();
    }
}