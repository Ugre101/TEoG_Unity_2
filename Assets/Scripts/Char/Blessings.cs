﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blessings
{
    [SerializeField] private PregnancyBlessings pregnancyBlessings = new PregnancyBlessings();
    public PregnancyBlessings PregnancyBlessing => pregnancyBlessings;
}

public enum PregnancyBlessingsIds
{
    Incubator, // Faster pregnancy
    BroadMother, // Higher change for twins or more
    VirileLoad, // Simple virility rise
    SpermFactory, // Greatly increases sperm production at cost of increased fat burn
    PrenancyFreak, // Greatly increases virility and fertility however you will lose sanity if you don't impregnate and are impregnated.
}

public struct PregnancyBlessing
{
    public PregnancyBlessing(PregnancyBlessingsIds id, int value)
    {
        this.id = id;
        this.value = value;
    }

    [SerializeField] private PregnancyBlessingsIds id;
    [SerializeField] private int value;
    public PregnancyBlessingsIds Id => id;
    public int Value => value;

    public int IncreaseValue(int with) => value += Mathf.Abs(with);
}

[System.Serializable]
public class PregnancyBlessings
{
    [SerializeField] private List<PregnancyBlessing> pregnancyBlessings = new List<PregnancyBlessing>();

    public void AddBlessing(PregnancyBlessingsIds id) => pregnancyBlessings.Add(new PregnancyBlessing(id, 1));

    public void AddBlessing(PregnancyBlessingsIds id, int startVal) => pregnancyBlessings.Add(new PregnancyBlessing(id, startVal));

    public bool HasBlessing(PregnancyBlessingsIds id) => pregnancyBlessings.Exists(pb => pb.Id == id);

    public PregnancyBlessing GetBlessing(PregnancyBlessingsIds id) => pregnancyBlessings.Find(pb => pb.Id == id);

    public int GetBlessingValue(PregnancyBlessingsIds id) => HasBlessing(id) ? GetBlessing(id).Value : 0;
}