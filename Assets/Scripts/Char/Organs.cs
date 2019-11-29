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

    public void RefreshOrgans(BasicChar basicChar, bool autoEss = false)
    {
        Dicks.RemoveAll(d => d.Size <= 0);
        Balls.RemoveAll(b => b.Size <= 0);
        Vaginas.RemoveAll(v => v.Size <= 0);
        Boobs.RemoveAll(b => b.Size <= 0);
        if (autoEss)
        {
            if (basicChar.Masc.Amount > 0)
            {
                if (Dicks.Total() <= Balls.Total() * 2f + 1f)
                {
                    if (Dicks.Exists(d => basicChar.Masc.Amount >= d.Cost))
                    {
                        foreach (Dick d in Dicks)
                        {
                            if (basicChar.Masc.Amount >= d.Cost)
                            {
                                basicChar.Masc.Lose(d.Grow());
                            }
                        }
                    }
                    else if (basicChar.Masc.Amount >= Dicks.Cost())
                    {
                        basicChar.Masc.Lose(Dicks.Cost());
                        Dicks.AddDick();
                    }
                }
                else
                {
                    if (Balls.Exists(b => basicChar.Masc.Amount >= b.Cost))
                    {
                        foreach (Balls b in Balls)
                        {
                            if (basicChar.Masc.Amount >= b.Cost)
                            {
                                basicChar.Masc.Lose(b.Grow());
                            }
                        }
                    }
                    else if (basicChar.Masc.Amount >= Balls.Cost())
                    {
                        basicChar.Masc.Lose(Balls.Cost());
                        Balls.AddBalls();
                    }
                }
            }
            if (basicChar.Femi.Amount > 0)
            {
                if (Boobs.Total() <= Vaginas.Total() * 1.5f + 1f)
                {
                    if (Boobs.Exists(b => basicChar.Femi.Amount >= b.Cost))
                    {
                        foreach (Boobs b in Boobs)
                        {
                            if (basicChar.Femi.Amount >= b.Cost)
                            {
                                basicChar.Femi.Lose(b.Grow());
                            }
                        }
                    }
                    else if (basicChar.Femi.Amount >= Boobs.Cost())
                    {
                        basicChar.Femi.Lose(Boobs.Cost());
                        Boobs.AddBoobs();
                    }
                }
                else
                {
                    if (Vaginas.Exists(v => basicChar.Femi.Amount >= v.Cost))
                    {
                        foreach (Vagina v in Vaginas)
                        {
                            if (basicChar.Femi.Amount >= v.Cost)
                            {
                                basicChar.Femi.Lose(v.Grow());
                            }
                        }
                    }
                    else if (basicChar.Femi.Amount >= Vaginas.Cost())
                    {
                        basicChar.Femi.Lose(Vaginas.Cost());
                        Vaginas.AddVag();
                    }
                }
            }
        }
    }
}