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
    /* Amount of essence body can hold without auto transforming; if exceded body will try to tranform gender with all 
     * essence it have. Even that which was stable.*/
    [SerializeField] private CharStats stableEssence = new CharStats(0);
    public CharStats StableEssence => stableEssence;
    public bool AutoEss => autoEss;
    public bool SetAutoEss { set => autoEss = value; }
}