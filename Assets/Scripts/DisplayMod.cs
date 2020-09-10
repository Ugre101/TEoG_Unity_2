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

    private List<Mod> Mods { get; }
    private readonly IDuration iDur;
    public int Duration => iDur?.Duration ?? 0;

    public string Source { get; }

    public string Desc()
    {
        StringBuilder builder = new StringBuilder();
        Mods.ForEach(m =>
        {
            switch (m)
            {
                case TempHealthMod thm:
                    builder.Append($"{thm.HealthType} {thm.Value} {thm.ModType} {thm.Source}");
                    break;
                case TempStatMod tsm:
                    builder.Append($"{tsm.Value} {tsm.ModType} {tsm.Source}");
                    break;
            }
        });
        return builder.ToString();
    }
}