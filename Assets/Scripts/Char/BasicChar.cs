﻿using System.Collections.Generic;
using UnityEngine;

public abstract class BasicChar : MonoBehaviour
{
    [SerializeField]
    private float healthPoints, willHealthPoints;

    public float Hp
    {
        get { return Mathf.Round(healthPoints); }
        set
        {
            float change = Mathf.Clamp(value, -healthPoints, MaxHP - healthPoints);
            healthPoints += change;
            updateSlider?.Invoke();
        }
    }

    public float Wp
    {
        get { return Mathf.Round(willHealthPoints); }
        set
        {
            float change = Mathf.Clamp(value, -willHealthPoints, MaxWP - willHealthPoints);
            willHealthPoints += change;
            updateSlider?.Invoke();
        }
    }

    [SerializeField]
    private CharStats maxHealthPoints, maxWillHealthPoints;

    public float MaxHP
    {
        get { return maxHealthPoints._value; }
        set { maxHealthPoints._baseValue = value; }
    }

    public float MaxWP
    {
        get { return maxWillHealthPoints._value; }
        set { maxWillHealthPoints._baseValue = value; }
    }

    [SerializeField]
    private int experience = 0, level = 0, statPoints = 0, perkPoints = 0;

    public int Exp
    {
        get { return experience; }
        set
        {
            experience += value;
            if (experience >= maxEXP())
            {
                experience -= maxEXP();
                level++;
                statPoints += 3;
                perkPoints++;
                // Eventlog level up;
            }
            expChange?.Invoke();
        }
    }

    public int Level { get { return level; } }

    public int StatPoints { get { return statPoints; } }
    public int PerkPoints { get { return perkPoints; } }

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

    public void init(int lvl, float maxhp, float maxwp, float str, float charm, float gold)
    {
        MaxHP = maxhp;
        Hp = MaxHP;
        MaxWP = maxwp;
        Wp = MaxWP;
        Str = str;
        Charm = charm;
        level = lvl;
        Gold = gold;
    }

    public bool TakeHealthDamage(float damageDealt)
    {
        float dmg = damageDealt;
        Hp = Mathf.Min(0, -dmg);
        if (Hp <= 0)
        {
            return true;
            // Defeat player wins toggle afterbattle/sex
        }
        return false;
    }

    public bool TakeWillDamage(float damageDealt)
    {
        float dmg = damageDealt;
        Wp = Mathf.Min(0, -dmg);
        if (Wp <= 0)
        {
            return true;
            // Defeat
        }
        return false;
    }

    public float hpSlider()
    {
        return Hp / MaxHP;
    }

    public string hpStatus()
    {
        return $"{Hp}/{MaxHP}";
    }

    public float wpSlider()
    {
        return Wp / MaxWP;
    }

    public string wpStatus()
    {
        return $"{Wp}/{MaxWP}";
    }

    public float expSlider()
    {
        return (float)Exp / (float)maxEXP();
    }

    public string expStatus()
    {
        return $"{Exp}/{maxEXP()}";
    }

    private int maxEXP()
    {
        return (int)Mathf.Round(30f * Mathf.Pow(1.05f, level - 1f));
    }

    public string levelStatus()
    {
        return $"Level: {level}";
    }

    public delegate void ExpChange();

    public static event ExpChange expChange;

    public void manualExpUpdate()
    {
        expChange();
    }

    public delegate void UpdateSlider();

    public static event UpdateSlider updateSlider;

    public void manualSliderUpdate()
    {
        updateSlider();
    }

    [SerializeField]
    private bool AutoEssence = true;

    public void ToggleAutoEssence()
    {
        AutoEssence = AutoEssence ? false : true;
        // if autoessence check if need to grow stuff
    }

    [SerializeField]
    private float masc, femi;

    public float Masc { get { return masc; } }
    public float Femi { get { return femi; } }

    public void GainMasc(float mascToGain)
    {
        masc += Mathf.Max(0, mascToGain);
        essenceSlider?.Invoke();
        // if auto grow organs
    }

    public float LoseMasc(float mascToLose)
    {
        float have = Mathf.Clamp(masc, 0, mascToLose);
        if (have < mascToLose)
        {
            // Try to shrink organs add to have
            while (have < mascToLose && false)//and have relevant organs
            {
                have += 1;
            }
        }
        else
        {
            masc -= mascToLose;
        }
        essenceSlider?.Invoke();
        return have;
    }

    public void GainFemi(float femiToGain)
    {
        femi += Mathf.Max(0, femiToGain);
        essenceSlider?.Invoke();
    }

    public float LoseFemi(float femiToLose)
    {
        float have = Mathf.Clamp(femi, 0, femiToLose);
        if (have < femiToLose)
        {
            while (have < femiToLose && false)
            {
                have += 1;
            }
        }
        else
        {
            femi -= femiToLose;
        }
        essenceSlider?.Invoke();
        return have;
    }

    public delegate void EssenceSlider();

    public static event EssenceSlider essenceSlider;

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
    private List<SexualOrgan> dicks = new List<SexualOrgan>();

    public List<SexualOrgan> Dicks { get { return dicks; } }

    public void AddDick()
    {
        SexualOrgan dick = new SexualOrgan(OrganType.Dick);
        dicks.Add(dick);
    }

    public void GrowDick(int index)
    {
        int i = Mathf.Clamp(index, 0, dicks.Count - 1);
        dicks[i]._baseSize++;
    }
    public bool ShrinkDick(int index)
    {
        int i = Mathf.Clamp(index, 0, dicks.Count - 1);
        dicks[i]._baseSize--;
        if (dicks[i]._baseSize <= 0)
        {
            dicks.RemoveAt(i);
            return true;
        }else
        {
            return false;
        }
    }

    [SerializeField]
    private List<SexualOrgan> balls = new List<SexualOrgan>();

    public List<SexualOrgan> Balls { get { return balls; } }

    public void AddBalls()
    {
        SexualOrgan ball = new SexualOrgan(OrganType.Balls);
        balls.Add(ball);
    }
    public void GrowBalls(int index)
    {
        int i = Mathf.Clamp(index, 0, balls.Count - 1);
        balls[i]._baseSize++;
    }

    public bool ShrinkBalls(int index)
    {
        int i = Mathf.Clamp(index, 0, balls.Count - 1);
        balls[i]._baseSize--;
        if (balls[i]._baseSize <= 0)
        {
            balls.RemoveAt(i);
            return true;
        }else
        {
            return false;
        }
    }

    [SerializeField]
    private List<SexualOrgan> boobs = new List<SexualOrgan>();

    public List<SexualOrgan> Boobs { get { return boobs; } }

    public void AddBoobs()
    {
        SexualOrgan boob = new SexualOrgan(OrganType.Boobs);
        boobs.Add(boob);
    }
    public void GrowBoobs(int index)
    {
        int i = Mathf.Clamp(index, 0, boobs.Count - 1);
        boobs[i]._baseSize++;
    }
    public bool ShrinkBoobs(int index)
    {
        int i = Mathf.Clamp(index, 0, boobs.Count - 1);
        boobs[i]._baseSize--;
        if (boobs[i]._baseSize <= 0)
        {
            boobs.RemoveAt(i);
            return true;
        }else
        {
            return false;
        }
    }

    [SerializeField]
    private List<SexualOrgan> vaginas = new List<SexualOrgan>();

    public List<SexualOrgan> Vaginas { get { return vaginas; } }

    public void AddVagina()
    {
        SexualOrgan vagina = new SexualOrgan(OrganType.Vagina);
        vaginas.Add(vagina);
    }
    public void GrowVagina(int index)
    {
        int i = Mathf.Clamp(index, 0, vaginas.Count - 1);
        vaginas[i]._baseSize++;
    }
    public bool ShrinkVagina(int index)
    {
        int i = Mathf.Clamp(index, 0, vaginas.Count - 1);
        vaginas[i]._baseSize--;
        if (vaginas[i]._baseSize <= 0)
        {
            vaginas.RemoveAt(i);
            return true;
        }else
        {
            return false;
        }
    }
}