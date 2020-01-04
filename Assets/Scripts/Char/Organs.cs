using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Organs
{
    [SerializeField]
    private List<Dick> dicks = new List<Dick>();

    public List<Dick> Dicks => dicks;

    [SerializeField]
    private List<Balls> balls = new List<Balls>();

    public List<Balls> Balls => balls;

    public float CumSlider => Balls.CumTotal() / Balls.CumMax();

    public string CumStatus => $"{Mathf.Round(Balls.CumTotal())}";

    [SerializeField]
    private List<Boobs> boobs = new List<Boobs>();

    public List<Boobs> Boobs => boobs;

    [SerializeField]
    private bool lactating = false;

    public bool Lactating => lactating;

    public float MilkSlider => Boobs.MilkTotal() / Boobs.MilkMax();

    public string MilkStatus => $"{Mathf.Round(Boobs.MilkTotal() / 1000)}";

    [SerializeField]
    private List<Vagina> vaginas = new List<Vagina>();

    public List<Vagina> Vaginas => vaginas;
}

public static class OrganExtension
{
    public static bool HaveDick(this Organs orgs) => orgs.Dicks.Count > 0;

    public static bool HaveDick(this Organs orgs, float minSize) => orgs.Dicks.Count > 0 ? orgs.Dicks.Biggest() >= minSize : false;

    public static bool HaveBalls(this Organs organs) => organs.Balls.Count > 0;

    public static bool HaveBalls(this Organs organs, float minSize) => organs.Balls.Count > 0 ? organs.Balls.Biggest() >= minSize : false;

    public static bool HaveVagina(this Organs organs) => organs.Vaginas.Count > 0;

    public static bool HaveVagina(this Organs organs, float minSize) => organs.Vaginas.Count > 0 ? organs.Vaginas.Biggest() >= minSize : false;

    // if have boobs check if boobs are big enough.
    public static bool HaveBoobs(this Organs organs) => organs.Boobs.Count > 0 ? organs.Boobs.Biggest() > 3 : false;

    public static bool HaveBoobs(this Organs organs, float minSize) => organs.Boobs.Count > 0 ? organs.Boobs.Biggest() >= minSize : false;
}