using System.Collections.Generic;

public class DisplayVore
{
    public DisplayVore(Vore.VoreBasic organ) => VoreOrgan = organ;

    public List<Vore.ThePrey> Preys => VoreOrgan.Preys;
    public Vore.VoreBasic VoreOrgan { get; private set; }

    public float Progress()
    {
        float donest = 0;
        Preys.ForEach(p =>
        {
            float f = p.Progress();
            if (f > donest)
            {
                donest = f;
            }
        });
        return donest;
    }
}
