using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    [System.Serializable]
    public class VoreEngine
    {
        private readonly BasicChar pred;
        public bool Active = false;

        #region voreOrgans

        [field: SerializeField] public VoreBalls Balls { get; private set; }
        [field: SerializeField] public VoreBoobs Boobs { get; private set; }
        [field: SerializeField] public VoreStomach Stomach { get; private set; }
        [field: SerializeField] public VoreAnal Anal { get; private set; }
        [field: SerializeField] public VoreVagina Vagina { get; private set; }
        private List<VoreBasic> voreOrgans = new List<VoreBasic>();

        public List<VoreBasic> VoreOrgans
        {
            get
            {
                if (voreOrgans.Count < 1)
                {
                    voreOrgans = new List<VoreBasic>() { Balls, Boobs, Stomach, Anal, Vagina };
                }
                return voreOrgans;
            }
        }

        #endregion voreOrgans

        public int TotalPreyCount
        {
            get
            {
                int tot = 0;
                VoreOrgans.ForEach(vo => tot += vo.Preys.Count);
                return tot;
            }
        }

        public VoreEngine(BasicChar parPred)
        {
            pred = parPred;
            Balls = new VoreBalls(parPred);
            Boobs = new VoreBoobs(parPred);
            Stomach = new VoreStomach(parPred);
            Anal = new VoreAnal(parPred);
            Vagina = new VoreVagina(parPred);
        }

        public void Digest()
        {
            if (pred.CompareTag("Player"))
            {
                List<ThePrey> Ballsdigested = Balls.Digest();
                if (Ballsdigested.Count > 0)
                {
                    foreach (ThePrey prey in Ballsdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been fully transfomed into cum.";
                        EventLog.AddTo(text);
                    }
                }
                List<ThePrey> Boobsdigested = Boobs.Digest();
                if (Boobsdigested.Count > 0)
                {
                    foreach (ThePrey prey in Boobsdigested)
                    {
                        string text = $"{prey.Prey.FullName} is now nothing but milk.";
                        EventLog.AddTo(text);
                    }
                }
                List<ThePrey> Stomachdigested = Stomach.Digest();
                if (Stomachdigested.Count > 0)
                {
                    foreach (ThePrey prey in Stomachdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been digested.";
                        EventLog.AddTo(text);
                    }
                }
                List<ThePrey> Analdigested = Anal.Digest();
                if (Analdigested.Count > 0)
                {
                    foreach (ThePrey prey in Analdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been reduced to nothing in your bowels.";
                        EventLog.AddTo(text);
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

    [System.Serializable]
    public class ThePrey
    {
        [field: SerializeField] public BasicChar Prey { get; private set; }

        public void SetPrey(BasicChar value) => Prey = value;

        [field: SerializeField] public float StartWeight { get; private set; }

        public ThePrey(BasicChar parPrey)
        {
            Prey = parPrey;
            StartWeight = parPrey.Weight;
        }

        /// <summary> First digest the fat, then the muscle and last the bones(height). </summary>
        /// <param name="toDigest">Amount to digest</param>
        /// <returns></returns>
        public float Digest(float toDigest)
        {
            float fatGain = Mathf.Min(toDigest, Prey.Weight);
            if (Prey.Body.Fat.Value > 0)
            {
                Prey.Body.Fat.LoseFlat(toDigest);
            }
            else if (Prey.Body.Muscle.Value > 0)
            {
                Prey.Body.Muscle.LoseFlat(toDigest);
            }
            else
            {
                Prey.Body.Height.LoseFlat(toDigest);
            }
            return fatGain;
        }

        public float Progress() => (StartWeight - Prey.Weight) / StartWeight;
    }
}