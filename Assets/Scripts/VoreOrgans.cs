using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vore
{
    [Serializable]
    public abstract class VoreBasic
    {
        public VoreContainers VoreContainers { get; protected set; }

        public VoreBasic(BasicChar parPred) => pred = parPred;

        protected readonly BasicChar pred;

        [SerializeField] protected List<ThePrey> preys = new List<ThePrey>();

        public List<ThePrey> Preys => preys;
        public int PreyCount => Preys.Count;

        [SerializeField] protected int voreExp = 0;

        protected float VoreExpCapBonus => 1f + (voreExp / 100);

        public virtual float MaxCapacity()
        {
            float cap = 0;
            return cap * VoreExpCapBonus;
        }

        /// <summary> Return weight of prey content</summary>
        public virtual float Current => preys.Sum(p => p.Prey.Weight);

        /// <summary>Returns how full it is; 0.5 = 50%</summary>
        public virtual float FillPrecent => Current / MaxCapacity();

        public virtual bool CanVore(ThePrey parPrey) => Current + parPrey.Prey.Weight <= MaxCapacity();

        public virtual bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                pred.VoreChar.Stomach.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        [SerializeField] protected bool digestion = true;

        public bool Digestion => digestion;

        public virtual bool ToggleDigestion => digestion = !digestion;

        public void Digest(Action<ThePrey> digested, float toDigest = 1f)
        {
            for (int i = Preys.Count - 1; i >= 0; i--)
            {
                ThePrey prey = Preys[i];
                pred.Body.Fat.GainFlat(prey.Digest(toDigest));
                if (prey.Prey.Weight <= 1)
                {
                    digested?.Invoke(prey);
                    Preys.Remove(prey);
                }
                voreExp += Mathf.FloorToInt(toDigest);
            }
        }
    }

    [Serializable]
    public class VoreBalls : VoreBasic
    {
        public VoreBalls(BasicChar parPred) : base(parPred)
        {
            VoreContainers = VoreContainers.Balls;
        }

        public override bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                pred.VoreChar.Balls.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Balls.Sum(b => b.Size);
            return cap * VoreExpCapBonus;
        }
    }

    [Serializable]
    public class VoreBoobs : VoreBasic
    {
        public VoreBoobs(BasicChar pred) : base(pred)
        {
            VoreContainers = VoreContainers.Boobs;
        }

        public override bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                pred.VoreChar.Boobs.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Boobs.Sum(b => b.Size);
            return cap * VoreExpCapBonus;
        }
    }

    [Serializable]
    public class VoreStomach : VoreBasic
    {
        public VoreStomach(BasicChar pred) : base(pred)
        {
            VoreContainers = VoreContainers.Stomach;
        }

        public override bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                pred.VoreChar.Stomach.AddPrey(parPrey);
                preys.Add(parPrey);
                return true;
            }
            return false;
        }

        public override float MaxCapacity()
        {
            float cap = pred.Body.Height.Value / 3;
            return cap * VoreExpCapBonus;
        }
    }

    [Serializable]
    public class VoreAnal : VoreBasic
    {
        public VoreAnal(BasicChar pred) : base(pred)
        {
            VoreContainers = VoreContainers.Anal;
        }

        public override bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                pred.VoreChar.Anal.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        public override float MaxCapacity()
        {
            float cap = pred.Body.Height.Value / 4;
            return cap * VoreExpCapBonus;
        }
    }

    [Serializable]
    public class VoreVagina : VoreBasic
    {
        public VoreVagina(BasicChar Pred) : base(Pred)
        {
            VoreContainers = VoreContainers.Vagina;
        }

        public override bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                pred.VoreChar.Vagina.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        [SerializeField] private bool childTf = false;

        public bool ChildTf => childTf;

        public bool ToggleChildTf()
        {
            if (Digestion) { digestion = false; }
            return childTf = !childTf;
        }

        public override bool ToggleDigestion
        {
            get
            {
                if (ChildTf) { childTf = false; }
                return base.ToggleDigestion;
            }
        }

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Vaginas.Sum(v => v.Size);
            return cap * VoreExpCapBonus;
        }

        public void TransformToChild(Action<ThePrey> tfChild)
        {
            foreach (ThePrey prey in Preys)
            {
                Age age = prey.Prey.Age;
                age.AgeDown();
                if (age.AgeYears < 1)
                {
                    tfChild?.Invoke(prey);
                }
            }
        }

        [Serializable]
        public class PreyToChild
        {
            public PreyToChild(ThePrey parPrey)
            {
                prey = parPrey;
                startAge = prey.Prey.Age.AgeYears;
                lastAge = startAge;
            }

            [SerializeField] private ThePrey prey;

            public ThePrey Prey => prey;

            [SerializeField] private int startAge;

            [SerializeField] private int lastAge;

            public int CurAge => prey.Prey.Age.AgeYears;
            public int StartAge => startAge;

            public bool AgeDown()
            {
                BasicChar bChar = prey.Prey;
                bChar.Age.AgeDown();
                if (lastAge != CurAge)
                {
                    float shrinkFactor = lastAge / CurAge;
                    lastAge = CurAge;
                    Body body = bChar.Body;
                    body.Height.LosePrecent(shrinkFactor);
                    body.Fat.LosePrecent(shrinkFactor);
                    body.Muscle.LosePrecent(shrinkFactor);
                    // TODO shrink prey maybe eventlog it?
                    // prey shall shrink very little while over 25, but once under 18 they shrink steadely.
                    return true;
                }
                return false;
            }
        }
    }
}