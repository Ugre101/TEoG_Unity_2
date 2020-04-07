using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Organs
{
    // if genderpref auto ess will adapt to prefer certain organs over others. But it doesn't make tf impossible just harder.
    [SerializeField] private bool genderPrefActive = false;

    public bool GenderPrefActive => genderPrefActive;
    public bool SetGenderPrefActive { set => genderPrefActive = value; }
    public bool ToggleGenderPrefActive => genderPrefActive = !genderPrefActive;
    [SerializeField] private Genders genderPref = Genders.Male;
    public Genders GenderPref => genderPref;
    public Genders SetGenderPref { set => genderPref = value; }
    [SerializeField] private List<Dick> dicks = new List<Dick>();

    public List<Dick> Dicks => dicks;

    [SerializeField] private List<Balls> balls = new List<Balls>();

    public List<Balls> Balls => balls;
    [SerializeField] private CharStats ballsBunusRefillRate = new CharStats(0);
    public CharStats BallsBunusRefillRate => ballsBunusRefillRate;

    public float CumSlider => Balls.CumTotal() / Balls.CumMax();

    public string CumStatus => $"{Mathf.Round(Balls.CumTotal())}";

    [SerializeField] private List<Boobs> boobs = new List<Boobs>();

    public List<Boobs> Boobs => boobs;
    [SerializeField] private CharStats boobsBonusRefillRate = new CharStats();
    public CharStats BoobsBonusRefillRate => boobsBonusRefillRate;
    [SerializeField] private bool lactating = false;

    public bool Lactating => lactating;

    public float MilkSlider => Boobs.MilkTotal() / Boobs.MilkMax();

    public string MilkStatus => $"{Mathf.Round(Boobs.MilkTotal() / 1000)}";

    [SerializeField] private List<Vagina> vaginas = new List<Vagina>();

    public List<Vagina> Vaginas => vaginas;

    [SerializeField] private List<Anal> anals = new List<Anal>();
    public List<Anal> Anals => anals;
    // TODO scat totals
}

public static class OrganExtension
{
    public static bool HaveDick(this Organs orgs) => orgs.Dicks.Count > 0;

    public static bool HaveDick(this Organs orgs, float minSize) => orgs.Dicks.Count > 0 ? orgs.Dicks.BiggestSize() >= minSize : false;

    public static bool HaveBalls(this Organs organs) => organs.Balls.Count > 0;

    public static bool HaveBalls(this Organs organs, float minSize) => organs.Balls.Count > 0 ? organs.Balls.BiggestSize() >= minSize : false;

    public static bool HaveVagina(this Organs organs) => organs.Vaginas.Count > 0;

    public static bool HaveVagina(this Organs organs, float minSize) => organs.Vaginas.Count > 0 ? organs.Vaginas.BiggestSize() >= minSize : false;

    // if have boobs check if boobs are big enough.
    public static bool HaveBoobs(this Organs organs) => organs.Boobs.Count > 0 ? organs.Boobs.BiggestSize() > 3 : false;

    public static bool HaveBoobs(this Organs organs, float minSize) => organs.Boobs.Count > 0 ? organs.Boobs.BiggestSize() >= minSize : false;
}