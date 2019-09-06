using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VoreEngine
{
    private EventLog eventLog;
    private BasicChar Pred;
    public bool Active = false;
    public VoreBalls Balls;
    public VoreBoobs Boobs;
    public VoreStomach Stomach;
    public VoreAnal Anal;

    public VoreEngine(EventLog log, BasicChar pred)
    {
        eventLog = log;
        Pred = pred;
        Balls = new VoreBalls(pred);
        Boobs = new VoreBoobs(pred);
        Stomach = new VoreStomach(pred);
        Anal = new VoreAnal(pred);
    }

    public void Digest()
    {
        if (eventLog != null)
        {
            List<ThePrey> Ballsdigested = Balls.Digest();
            if (Ballsdigested.Count > 0)
            {
                foreach (ThePrey prey in Ballsdigested)
                {
                    string text = $"{prey.Prey.FullName} has been fully transfomed into cum.";
                    eventLog.AddTo(text);
                }
            }
            List<ThePrey> Boobsdigested = Boobs.Digest();
            if (Boobsdigested.Count > 0)
            {
                foreach (ThePrey prey in Boobsdigested)
                {
                    string text = $"{prey.Prey.FullName} is now nothing but milk.";
                    eventLog.AddTo(text);
                }
            }
            List<ThePrey> Stomachdigested = Stomach.Digest();
            if (Stomachdigested.Count > 0)
            {
                foreach (ThePrey prey in Stomachdigested)
                {
                    string text = $"{prey.Prey.FullName} has been digested.";
                    eventLog.AddTo(text);
                }
            }
            List<ThePrey> Analdigested = Anal.Digest();
            if (Analdigested.Count > 0)
            {
                foreach (ThePrey prey in Analdigested)
                {
                    string text = $"{prey.Prey.FullName} has been reduced to nothing in your bowels.";
                    eventLog.AddTo(text);
                }
            }
            // unbirth or rebirth
        }
        else
        {
            Balls.Digest();
            Boobs.Digest();
            Stomach.Digest();
            Anal.Digest();
            // unbirth or rebirth
        }
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
        startWeight = whom.Weight;
    }

    public float Digest(float toDigest)
    {
        float fatGain = Mathf.Min(toDigest, prey.Weight);
        prey.Weight -= toDigest;
        return fatGain;
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
            cur += p.Prey.Weight;
        }
        return cur;
    }

    public virtual bool Vore(BasicChar p)
    {
        if (Current() + p.Weight <= Cap())
        {
            preys.Add(new ThePrey(p));
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<ThePrey> Digest()
    {
        List<ThePrey> Digested = new List<ThePrey>();
        foreach (ThePrey prey in Preys)
        {
            Pred.Weight += prey.Digest(1f);
            if (prey.Prey.Weight <= 0)
            {
                Digested.Add(prey);
            }
        }
        return Digested;
    }
}

public class VoreBalls : VoreBasic
{
    public VoreBalls(BasicChar pred)
    {
        Pred = pred;
    }

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
    public VoreBoobs(BasicChar pred)
    {
        Pred = pred;
    }

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
    public VoreStomach(BasicChar pred)
    {
        Pred = pred;
    }

    public override float Cap()
    {
        float cap = 0;
        return cap;
    }
}

public class VoreAnal : VoreBasic
{
    public VoreAnal(BasicChar pred)
    {
        Pred = pred;
    }

    public override float Cap()
    {
        float cap = 0;
        return cap;
    }
}