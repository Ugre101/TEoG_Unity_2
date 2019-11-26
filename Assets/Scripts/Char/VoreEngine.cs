using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    [System.Serializable]
    public class VoreEngine
    {
        private EventLog EveLog => pred.BasicCharGame.EventLog;

        private readonly BasicChar pred;
        public bool Active = false;

        #region voreOrgans

        [SerializeField]
        private VoreBalls balls;

        public VoreBalls Balls => balls;

        [SerializeField]
        private VoreBoobs boobs;

        public VoreBoobs Boobs => boobs;

        [SerializeField]
        private VoreStomach stomach;

        public VoreStomach Stomach => stomach;

        [SerializeField]
        private VoreAnal anal;

        public VoreAnal Anal => anal;

        #endregion voreOrgans

        public VoreEngine(BasicChar parRred)
        {
            pred = parRred;
            balls = new VoreBalls(parRred);
            boobs = new VoreBoobs(parRred);
            stomach = new VoreStomach(parRred);
            anal = new VoreAnal(parRred);
        }

        public void Digest()
        {
            if (EveLog != null)
            {
                List<ThePrey> Ballsdigested = Balls.Digest();
                if (Ballsdigested.Count > 0)
                {
                    foreach (ThePrey prey in Ballsdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been fully transfomed into cum.";
                        EveLog.AddTo(text);
                    }
                }
                List<ThePrey> Boobsdigested = Boobs.Digest();
                if (Boobsdigested.Count > 0)
                {
                    foreach (ThePrey prey in Boobsdigested)
                    {
                        string text = $"{prey.Prey.FullName} is now nothing but milk.";
                        EveLog.AddTo(text);
                    }
                }
                List<ThePrey> Stomachdigested = Stomach.Digest();
                if (Stomachdigested.Count > 0)
                {
                    foreach (ThePrey prey in Stomachdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been digested.";
                        EveLog.AddTo(text);
                    }
                }
                List<ThePrey> Analdigested = Anal.Digest();
                if (Analdigested.Count > 0)
                {
                    foreach (ThePrey prey in Analdigested)
                    {
                        string text = $"{prey.Prey.FullName} has been reduced to nothing in your bowels.";
                        EveLog.AddTo(text);
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
        [SerializeField]
        private BasicChar prey;

        public BasicChar Prey => prey;

        [SerializeField]
        private float startWeight;

        public float StartWeight => startWeight;

        public ThePrey(BasicChar parPrey)
        {
            prey = parPrey;
            startWeight = parPrey.Weight;
        }

        /// <summary>
        /// First digest the fat, then the muscle and last the bones(height).
        /// </summary>
        /// <param name="toDigest">Amount to digest</param>
        /// <returns></returns>
        public float Digest(float toDigest)
        {
            float fatGain = Mathf.Min(toDigest, prey.Weight);
            if (prey.Body.fat.Value > 0)
            {
                prey.Body.fat.Lose(toDigest);
            }
            else if (prey.Body.muscle.Value > 0)
            {
                prey.Body.muscle.Lose(toDigest);
            }
            else
            {
                prey.Body.height.Lose(toDigest);
            }
            return fatGain;
        }
    }
}