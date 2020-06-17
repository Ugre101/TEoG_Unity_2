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
    [SerializeField] private Genders genderPref = Genders.Doll;
    public Genders GenderPref => genderPref;
    public Genders SetGenderPref { set => genderPref = value; }
    private List<OrganContainer> allOrgans;

    public List<OrganContainer> AllOrgans
    {
        get
        {
            if (allOrgans == null)
            {
                allOrgans = new List<OrganContainer>() { Dicks, Balls, Boobs, Vaginas, };
            }
            return allOrgans;
        }
    }

    [SerializeField] private DickContainer dicks = new DickContainer();

    public DickContainer Dicks => dicks;

    [SerializeField] private BallsContaier balls = new BallsContaier();

    public BallsContaier Balls => balls;

    [SerializeField] private BoobsConatiner boobs = new BoobsConatiner();

    public BoobsConatiner Boobs => boobs;

    [SerializeField] private VaginaContainer vaginas = new VaginaContainer();

    public VaginaContainer Vaginas => vaginas;

    [SerializeField] private List<Anal> anals = new List<Anal>();
    public List<Anal> Anals => anals;

    public float ScatSlider => Anals.FluidCurrentTotal() / Anals.FluidMax();
    public string ScatStatus => $"{Mathf.Round(Anals.FluidCurrentTotal() / 1000)}";
    // TODO scat totals
}

public static class OrganExtension
{
    public static bool HaveDick(this Organs orgs) => orgs.Dicks.List.Count > 0;

    public static bool HaveDick(this Organs orgs, float minSize) => orgs.Dicks.List.Count > 0 ? orgs.Dicks.List.BiggestSize() >= minSize : false;

    public static bool HaveBalls(this Organs organs) => organs.Balls.List.Count > 0;

    public static bool HaveBalls(this Organs organs, float minSize) => organs.Balls.List.Count > 0 ? organs.Balls.List.BiggestSize() >= minSize : false;

    public static bool HaveVagina(this Organs organs) => organs.Vaginas.List.Count > 0;

    public static bool HaveVagina(this Organs organs, float minSize) => organs.Vaginas.List.Count > 0 ? organs.Vaginas.List.BiggestSize() >= minSize : false;

    // if have boobs check if boobs are big enough.
    public static bool HaveBoobs(this Organs organs) => organs.Boobs.List.Count > 0 ? organs.Boobs.List.BiggestSize() > 3 : false;

    public static bool HaveBoobs(this Organs organs, float minSize) => organs.Boobs.List.Count > 0 ? organs.Boobs.List.BiggestSize() >= minSize : false;
}