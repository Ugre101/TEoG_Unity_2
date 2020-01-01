using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vore
{
    [Serializable]
    public abstract class VoreBasic
    {
        public VoreBasic(BasicChar parPred) => pred = parPred;

        protected readonly BasicChar pred;

        [SerializeField]
        protected List<ThePrey> preys = new List<ThePrey>();

        public List<ThePrey> Preys => preys;

        [SerializeField]
        protected int voreExp = 0;

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

        public virtual bool CanVore(ThePrey parPrey)
        {
            Debug.Log(Current + parPrey.Prey.Weight + " <= " + MaxCapacity());
            return Current + parPrey.Prey.Weight <= MaxCapacity();
        }

        public virtual bool Vore(ThePrey parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(parPrey);
                return true;
            }
            return false;
        }

        public List<ThePrey> Digest(float toDigest = 1f)
        {
            List<ThePrey> Digested = new List<ThePrey>();
            foreach (ThePrey prey in Preys)
            {
                pred.Body.Fat.GainFlat(prey.Digest(toDigest));
                if (prey.Prey.Weight <= 0)
                {
                    Digested.Add(prey);
                }
                voreExp += Mathf.FloorToInt(toDigest);
            }
            return Digested;
        }
    }

    [Serializable]
    public class VoreBalls : VoreBasic
    {
        public VoreBalls(BasicChar parPred) : base(parPred)
        {
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
        }

        [SerializeField]
        private bool childTf = false;

        public bool ChildTf => childTf;

        public void ToggleChildTf() => childTf = !childTf;

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Vaginas.Sum(v => v.Size);
            return cap * VoreExpCapBonus;
        }

        public void TransformToChild()
        {
            foreach (ThePrey prey in Preys)
            {
                prey.Prey.Age.AgeDown();
                // TODO make them shrink once they start aging under adult age.
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

            [SerializeField]
            private ThePrey prey;

            public ThePrey Prey => prey;

            [SerializeField]
            private int startAge;

            [SerializeField]
            private int lastAge;

            public int CurAge => prey.Prey.Age.AgeYears;
            public int StartAge => startAge;

            public bool AgeDown()
            {
                prey.Prey.Age.AgeDown();
                if (lastAge != CurAge)
                {
                    float shrinkFactor = lastAge / CurAge;
                    lastAge = CurAge;
                    prey.Prey.Body.Height.LosePrecent(shrinkFactor);
                    prey.Prey.Body.Fat.LosePrecent(shrinkFactor);
                    prey.Prey.Body.Muscle.LosePrecent(shrinkFactor);
                    // TODO shrink prey maybe eventlog it?
                    // prey shall shrink very little while over 25, but once under 18 they shrink steadely.
                    return true;
                }
                return false;
            }
        }
    }
}