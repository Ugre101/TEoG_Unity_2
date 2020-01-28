using System;
using UnityEngine;

[Serializable]
public class EssenceSystem
{
    [SerializeField] private Essence masc = new Essence();

    public Essence Masc => masc;

    [SerializeField] private Essence femi = new Essence();

    public Essence Femi => femi;

    [SerializeField] private bool autoEss = true;

    public bool AutoEss => autoEss;
    public bool SetAutoEss { set => autoEss = value; }
}