using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vore;

public class DisplayVore
{
    public DisplayVore(VoreBasic organ) => VoreOrgan = organ;

    public List<ThePrey> Preys => VoreOrgan.Preys;
    public VoreBasic VoreOrgan { get; private set; }

    public float Progress()
    {
        float donest = Preys.Max(p => p.Progress);
        return Mathf.Round(donest * 100) / 100;
    }
}