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

        [SerializeField]
        protected BasicChar pred;

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

        public virtual float Current => preys.Sum(p => p.Prey.Weight);

        public virtual bool Vore(BasicChar p)
        {
            if (Current + p.Weight <= MaxCapacity())
            {
                preys.Add(new ThePrey(p));
                return true;
            }
            return false;
        }

        public List<ThePrey> Digest(float toDigest = 1f)
        {
            List<ThePrey> Digested = new List<ThePrey>();
            foreach (ThePrey prey in Preys)
            {
                pred.Body.fat.Gain(prey.Digest(toDigest));
                if (prey.Prey.Weight <= 0)
                {
                    Digested.Add(prey);
                }
                voreExp += Mathf.FloorToInt(toDigest);
            }
            return Digested;
        }
    }

    public class VoreBalls : VoreBasic
    {
        public VoreBalls(BasicChar parPred) : base(parPred)
        {
        }

        public override float MaxCapacity()
        {
            float cap = pred.Balls.Sum(b => b.Size);
            return cap * VoreExpCapBonus;
        }
    }

    public class VoreBoobs : VoreBasic
    {
        public VoreBoobs(BasicChar pred) : base(pred)
        {
        }

        public override float MaxCapacity()
        {
            float cap = pred.Boobs.Sum(b => b.Size);
            return cap * VoreExpCapBonus;
        }
    }

    public class VoreStomach : VoreBasic
    {
        public VoreStomach(BasicChar pred) : base(pred)
        {
        }

        public override float MaxCapacity()
        {
            float cap = pred.Body.height.Value / 3;
            return cap * VoreExpCapBonus;
        }
    }

    public class VoreAnal : VoreBasic
    {
        public VoreAnal(BasicChar pred) : base(pred)
        {
        }

        public override float MaxCapacity()
        {
            float cap = pred.Body.height.Value / 4;
            return cap * VoreExpCapBonus;
        }
    }

    public class VoreVagiana : VoreBasic
    {
        public VoreVagiana(BasicChar parPred) : base(parPred)
        {
        }

        [SerializeField]
        private bool childTf = false;

        public bool ChildTf => childTf;

        public void ToggleChildTf() => childTf = !childTf;

        public override float MaxCapacity()
        {
            float cap = pred.Vaginas.Sum(v => v.Size);
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

        public class PreyToChild
        {
            public PreyToChild(ThePrey parPrey)
            {
                prey = parPrey;
                startAge = prey.Prey.Age.AgeYears;
                curAge = startAge;
            }

            private ThePrey prey;
            public ThePrey Prey => prey;
            private int startAge;
            private int curAge;
            public int StartAge => startAge;
        }
    }
}