using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vore
{
    [Serializable]
    public abstract class VoreBasic
    {
        public VoreContainers VoreContainers { get; }

        public VoreBasic(BasicChar parPred, VoreContainers voreContainer)
        {
            pred = parPred;
            VoreContainers = voreContainer;
        }

        protected readonly BasicChar pred;

        [SerializeField] protected List<ThePrey> preys = new List<ThePrey>();

        public List<ThePrey> Preys => preys;
        public int PreyCount => Preys.Count;

        [SerializeField] protected int voreExp = 0;
        public Action<int> PassOnExp;

        public void GainExp(int gain)
        {
            voreExp += gain;
            PassOnExp?.Invoke(gain);
        }

        protected float VoreExpCapBonus => 1f + (voreExp / 100);

        public virtual float MaxCapacity()
        {
            float cap = 0;
            return cap * VoreExpCapBonus;
        }

        /// <summary> Return weight of prey content</summary>
        public virtual float Current => preys.Sum(p => p.Body.Weight * CompresionFactor);

        /// <summary>Returns how full it is; 0.5 = 50%</summary>
        public virtual float FillPrecent => Current / MaxCapacity();

        public virtual bool CanVore(BasicChar parPrey) => Current + parPrey.Body.Weight <= MaxCapacity();

        public virtual bool Vore(BasicChar parPrey)
        {
            if (CanVore(parPrey))
            {
                preys.Add(new ThePrey(parPrey));
                //  pred.VoreChar.Stomach.AddPrey(parPrey);
                return true;
            }
            return false;
        }

        [SerializeField] protected bool digestion = true;

        public bool Digestion => digestion;

        public virtual bool ToggleDigestion => digestion = !digestion;

        protected abstract void DigestTo(float val);

        public void Digest(Action<ThePrey> callBackDigested, int times, float toDigest = 2f)
        {
            float totalDigest = (toDigest + (Perks.GetPerkLevel(VorePerks.DigestiveFluids) * 2f)) * times;
            for (int i = Preys.Count - 1; i >= 0; i--)
            {
                ThePrey prey = Preys[i];
                DigestTo(prey.Digest(totalDigest));
                // TODO test if working and implement way to toggle vore settings.
                if (Perks.HasPerk(VorePerks.OrgasmicFluids))
                {
                    float arusalGain = 5 * Perks.GetPerkLevel(VorePerks.OrgasmicFluids);
                    if (prey.SexStats.GainArousal(arusalGain))
                    {
                        if (Perks.HasPerk(VorePerks.DrainEssence))
                        {
                            float toDrain = 6 * Perks.GetPerkLevel(VorePerks.DrainEssence);
                            if (prey.CanDrainFemi() && prey.CanDrainMasc() && VoreSettings.DrainEss == ChooseEssence.Both)
                            {
                                pred.Essence.Masc.Gain(prey.Essence.Masc.Lose(toDrain / 2));
                                pred.Essence.Femi.Gain(prey.Essence.Femi.Lose(toDrain / 2));
                            }
                            else if (prey.CanDrainMasc() && (VoreSettings.DrainEss == ChooseEssence.Masc || VoreSettings.DrainEss == ChooseEssence.Both))
                            {
                                pred.Essence.Masc.Gain(prey.Essence.Masc.Lose(toDrain));
                            }
                            else if (prey.CanDrainFemi() && (VoreSettings.DrainEss == ChooseEssence.Femi || VoreSettings.DrainEss == ChooseEssence.Both))
                            {
                                pred.Essence.Femi.Gain(prey.Essence.Femi.Lose(toDrain));
                            }
                        }
                    }
                }
                if (prey.Body.Weight <= 1)
                {
                    callBackDigested?.Invoke(prey);
                    Preys.Remove(prey);
                }
                GainExp(Mathf.FloorToInt(totalDigest));
            }
        }

        protected VorePerksSystem Perks => pred.Vore.Perks;
        protected float ElasticMulti => Perks.HasPerk(VorePerks.Elastic) ? 1f + (Perks.GetPerkLevel(VorePerks.Elastic) * 0.1f) : 1f;
        protected float CompresionFactor => Perks.HasPerk(VorePerks.Compression) ? 1f - (Perks.GetPerkLevel(VorePerks.Compression) * 0.1f) : 1f;

        public void RemovePrey(ThePrey prey) => Preys.Remove(prey);
    }

    [Serializable]
    public class VoreBalls : VoreBasic
    {
        public VoreBalls(BasicChar parPred) : base(parPred, VoreContainers.Balls)
        {
        }

        public override bool Vore(BasicChar parPrey)
        {
            return base.Vore(parPrey);
        }

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Balls.List.Sum(b => b.Size * ElasticMulti);
            return cap * VoreExpCapBonus;
        }

        protected override void DigestTo(float val)
        {
            pred.SexualOrgans.Balls.List.ForEach(b =>
            {
                if (!b.Fluid.IsFull)
                {
                    b.Fluid.ReFillWith(val);
                }
            });
        }
    }

    [Serializable]
    public class VoreBoobs : VoreBasic
    {
        public VoreBoobs(BasicChar pred) : base(pred, VoreContainers.Boobs)
        {
        }

        public override bool Vore(BasicChar parPrey)
        {
            return base.Vore(parPrey);
        }

        public override float MaxCapacity()
        {
            float cap = pred.SexualOrgans.Boobs.List.Sum(b => b.Size * ElasticMulti);
            return cap * VoreExpCapBonus;
        }

        protected override void DigestTo(float val)
        {
            pred.SexualOrgans.Boobs.List.ForEach(b =>
            {
                if (!b.Fluid.IsFull)
                {
                    b.Fluid.ReFillWith(val);
                }
            });
        }
    }

    [Serializable]
    public class VoreStomach : VoreBasic
    {
        public VoreStomach(BasicChar pred) : base(pred, VoreContainers.Stomach)
        {
        }

        public override bool Vore(BasicChar parPrey)
        {
            return base.Vore(parPrey);
        }

        public override float MaxCapacity()
        {
            float cap = (pred.Body.Height.Value / 3) * ElasticMulti;
            return cap * VoreExpCapBonus;
        }

        protected override void DigestTo(float val) => pred.GainFatAndRefillScat(val);
    }

    [Serializable]
    public class VoreAnal : VoreBasic
    {
        public VoreAnal(BasicChar pred) : base(pred, VoreContainers.Anal)
        {
        }

        public override bool Vore(BasicChar parPrey)
        {
            return base.Vore(parPrey);
        }

        public override float MaxCapacity()
        {
            float cap = (pred.Body.Height.Value / 4) * ElasticMulti;
            return cap * VoreExpCapBonus;
        }

        protected override void DigestTo(float val) => pred.GainFatAndRefillScat(val);
    }

    [Serializable]
    public class VoreVagina : VoreBasic
    {
        public VoreVagina(BasicChar Pred) : base(Pred, VoreContainers.Vagina)
        {
        }

        public override bool Vore(BasicChar parPrey)
        {
            return base.Vore(parPrey);
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
            float cap = pred.SexualOrgans.Vaginas.List.Sum(v => v.Size * ElasticMulti);
            return cap * VoreExpCapBonus;
        }

        public void TransformToChild(Action<ThePrey> tfChild, int times)
        {
            foreach (ThePrey prey in Preys)
            {
                Age age = prey.Age;
                age.AgeDown(times);
                if (age.AgeYears < 1)
                {
                    tfChild?.Invoke(prey);
                }
            }
        }

        protected override void DigestTo(float val)
        {
            pred.SexualOrgans.Vaginas.List.ForEach(v =>
            {
                if (!v.Fluid.IsFull)
                {
                    v.Fluid.ReFillWith(val);
                }
            });
        }

        [Serializable]
        public class PreyToChild
        {
            public PreyToChild(ThePrey parPrey)
            {
                prey = parPrey;
                startAge = prey.Age.AgeYears;
                lastAge = startAge;
            }

            [SerializeField] private ThePrey prey;

            public ThePrey Prey => prey;

            [SerializeField] private int startAge;

            [SerializeField] private int lastAge;

            public int CurAge => prey.Age.AgeYears;
            public int StartAge => startAge;

            public bool AgeDown()
            {
                BasicChar bChar = prey;
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