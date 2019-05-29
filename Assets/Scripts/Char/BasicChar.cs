using System.Collections.Generic;
using UnityEngine;

public abstract class BasicChar : MonoBehaviour
{
    [SerializeField]
    private string firstName, lastName;

    public string FirstName { get { return firstName; } set { firstName = value; } }
    public string LastName { get { return lastName; } set { lastName = value; } }
    public string FullName { get { return $"{firstName} {lastName}"; } }

    [SerializeField]
    private Health hp, wp;

    public Health HP { get { return hp; } }
    public Health WP { get { return wp; } }

    [SerializeField]
    public ExpSystem expSystem = new ExpSystem();

    public int Level { get { return expSystem.Level; } }
    public int Exp { get { return expSystem.Exp; } set { expSystem.Exp += value; } }
    public int StatsPoints { get { return expSystem.StatPoints; } }
    public int PerkPoints { get { return expSystem.PerkPoints; } }

    // Public
    [SerializeField]
    private CharStats strength;

    public float Str
    {
        get { return strength._value; }
        set { strength._baseValue = value; }
    }

    [SerializeField]
    private CharStats charm;

    public float Charm
    {
        get { return charm._value; }
        set { charm._baseValue = value; }
    }

    [SerializeField]
    private CharStats endurance;

    public float End
    {
        get { return endurance._value; }
        set { endurance._baseValue = value; }
    }

    [SerializeField]
    private CharStats dexterity;

    public float Dex
    {
        get { return dexterity._value; }
        set { dexterity._baseValue = value; }
    }

    public void init(int lvl, float maxhp, float maxwp)
    {
        hp = new Health(maxhp);
        wp = new Health(maxwp);
        expSystem.Level = lvl;
    }

    [SerializeField]
    private bool autoEss = true;

    public bool AutoEss { get { return autoEss; } }

    public void ToggleAutoEssence()
    {
        autoEss = autoEss ? false : true;
        // if autoessence check if need to grow stuff
    }

    [SerializeField]
    private Essence masc, femi;

    public Essence Masc { get { return masc; } }
    public Essence Femi { get { return femi; } }

    public float LoseMasc(float mascToLose)
    {
        float have = Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && false)// have needed organs
            {
                fromOrgans += 1f;
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                Masc.Gain(left);
            }
        }
        return have;
    }

    public float LoseFemi(float femiToLose)
    {
        float have = Femi.Lose(femiToLose);
        float missing = femiToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && false)// have needed organs
            {
                fromOrgans += 1f;
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                Femi.Gain(left);
            }
        }
        return have;
    }

    [SerializeField]
    private float gold = 0;

    public float Gold
    {
        get { return Mathf.Floor(gold); }
        set
        {
            gold += Mathf.Clamp(value, -gold, Mathf.Infinity);
        }
    }

    [SerializeField]
    private List<Dick> dicks = new List<Dick>();

    public List<Dick> Dicks { get { return dicks; } }

    public float DickTotal()
    {
        float tot = 0f;
        foreach (Dick dick in dicks)
        {
            tot += dick.Size;
        }
        return tot;
    }

    public void AddDick()
    {
        Dick dick = new Dick();
        dicks.Add(dick);
    }

    public float DickCost()
    {
        float cost = Mathf.Round(30 * Mathf.Pow(3, Dicks.Count));
        return cost;
    }

    [SerializeField]
    private List<Balls> balls = new List<Balls>();

    public List<Balls> Balls { get { return balls; } }

    public float BallTotal()
    {
        float tot = 0f;
        foreach (Balls ball in balls)
        {
            tot += ball.Size;
        }
        return tot;
    }

    public void AddBalls()
    {
        Balls ball = new Balls();
        balls.Add(ball);
    }

    public float BallCost()
    {
        float cost = Mathf.Round(30 * Mathf.Pow(3, Balls.Count));
        return cost;
    }

    [SerializeField]
    private List<Boobs> boobs = new List<Boobs>();

    public List<Boobs> Boobs { get { return boobs; } }

    public float BoobTotal()
    {
        float tot = 0f;
        foreach (Boobs boob in boobs)
        {
            tot += boob.Size;
        }
        return tot;
    }

    public void AddBoobs()
    {
        Boobs boob = new Boobs();
        boobs.Add(boob);
    }

    public float BoobCost()
    {
        float cost = Mathf.Round(30 * Mathf.Pow(3, Boobs.Count));
        return cost;
    }

    [SerializeField]
    private List<Vagina> vaginas = new List<Vagina>();

    public List<Vagina> Vaginas { get { return vaginas; } }

    public float VagTotal()
    {
        float tot = 0f;
        foreach (Vagina vag in vaginas)
        {
            tot += vag.Size;
        }
        return tot;
    }

    public void AddVagina()
    {
        Vagina vagina = new Vagina();
        vaginas.Add(vagina);
    }

    public float VagCost()
    {
        float cost = Mathf.Round(30 * Mathf.Pow(3, Vaginas.Count));
        return cost;
    }

    public SexStats sexStats = new SexStats();

    private void Update()
    {
        if (autoEss)
        {
            foreach (Dick d in Dicks.FindAll(x => x.Size <= 0))
            {
                Dicks.Remove(d);
            }
            foreach (Balls b in balls.FindAll(x => x.Size <= 0))
            {
                balls.Remove(b);
            }
            foreach (Vagina v in vaginas.FindAll(x => x.Size <= 0))
            {
                vaginas.Remove(v);
            }
            foreach (Boobs b in boobs.FindAll(x => x.Size <= 0))
            {
                boobs.Remove(b);
            }
            if (Masc.Amount > 100)
            {
            }
        }
    }
    
}