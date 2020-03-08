using System.Collections.Generic;
using UnityEngine;
using Vore;

public class DisplayVore
{
    public DisplayVore(VoreBasic organ) => VoreOrgan = organ;

    public List<ThePrey> Preys => VoreOrgan.Preys;
    public VoreBasic VoreOrgan { get; private set; }

    public float Progress()
    {
        float donest = 0;
        Preys.ForEach(p =>
        {
            float f = p.Progress;
            if (f > donest)
            {
                donest = f;
            }
        });
        return Mathf.Round(donest * 100) / 100;
    }
}