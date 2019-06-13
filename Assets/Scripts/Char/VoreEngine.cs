 using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VoreEngine
{
    public BasicChar Pred;
    public VoreBalls Balls;
    public VoreBoobs Boobs;
    public VoreStomach Stomach;
    public VoreAnal Anal;
    public VoreEngine(BasicChar pred)
    {
        Pred = pred;
        Balls = new VoreBalls(pred);
        Boobs = new VoreBoobs(pred);
        Stomach = new VoreStomach(pred);
        Anal = new VoreAnal(pred);
    }
}
public class ThePrey
{
    private BasicChar prey;
    public BasicChar Prey { get { return prey; } }
    private float startWeight;
    public float StartWeight { get { return startWeight; } }
    public ThePrey(BasicChar whom)
    {
        prey = whom;
        startWeight = whom.weight;
    }
}
[System.Serializable]
public class VoreBasic
{
    protected BasicChar Pred;
    protected List<ThePrey> preys = new List<ThePrey>();
    public List<ThePrey> Preys { get { return preys; } }
    public virtual float Cap()
    {
        float cap = 0;
        return cap;
    }
    public virtual float Current()
    {
        float cur = 0;
        foreach (ThePrey p in preys)
        {
            cur += p.Prey.weight;
        }
        return cur;
    }
    public virtual bool Vore(BasicChar p)
    {
        if (Current() + p.weight <= Cap())
        {
            preys.Add(new ThePrey(p));
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class VoreBalls : VoreBasic
{
    public VoreBalls (BasicChar pred) { Pred = pred; }
    public override float Cap()
    {
        float cap = 0;
        foreach (Balls b in Pred.Balls)
        {
            cap += b.Size;
        }
        return cap;
    }

}
public class VoreBoobs : VoreBasic
{
    public VoreBoobs(BasicChar pred) { Pred = pred; }
    public override float Cap()
    {
        float cap = 0;
        foreach (Boobs b in Pred.Boobs)
        {
            cap += b.Size;
        }
        return cap;
    }

}
public class VoreStomach : VoreBasic
{
    public VoreStomach(BasicChar pred) { Pred = pred; }

    public override float Cap()
    {
        float cap = 0;
        return cap;
    }

}
public class VoreAnal : VoreBasic
{
    public VoreAnal(BasicChar pred) { Pred = pred; }
    public float Cap()
    {
        float cap = 0;
        return cap;
    }
}
