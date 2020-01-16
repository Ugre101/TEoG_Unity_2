using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vore
{
    [System.Serializable]
    public class VoreEngine
    {
        private readonly BasicChar pred;
        [SerializeField] private bool active = false;
        public bool Active => active;
        public bool ToogleVore => active = !active;

        #region voreOrgans

        [SerializeField] private VoreBalls balls;
        public VoreBalls Balls => balls;
        [SerializeField] private VoreBoobs boobs;
        public VoreBoobs Boobs => boobs;
        [SerializeField] private VoreStomach stomach;
        public VoreStomach Stomach => stomach;
        [SerializeField] private VoreAnal anal;
        public VoreAnal Anal => anal;
        [SerializeField] private VoreVagina vagina;
        public VoreVagina Vagina => vagina;
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

        public int TotalPreyCount => VoreOrgans.Sum(vo => vo.PreyCount);

        public VoreEngine(BasicChar parPred)
        {
            pred = parPred;
            balls = new VoreBalls(parPred);
            boobs = new VoreBoobs(parPred);
            stomach = new VoreStomach(parPred);
            anal = new VoreAnal(parPred);
            vagina = new VoreVagina(parPred);
        }

        private bool? playerIsPred;

        private bool PlayerPred
        {
            get
            {
                if (!playerIsPred.HasValue)
                {
                    playerIsPred = pred.CompareTag(PlayerMain.GetPlayer.tag);
                }
                return playerIsPred.Value;
            }
        }

        public void Digest()
        {
            Balls.Digest(p => BallsDigested(p));
            Boobs.Digest(p => BoobsDigested(p));
            Stomach.Digest(p => StomachDigested(p));
            Anal.Digest(p => AnalDigested(p));
            if (Vagina.ChildTf)
            {
                vagina.TransformToChild(p => TfToChild(p));
            }
            else
            {
                Vagina.Digest(p => VaginaDigested(p));
            }
            string FullName(ThePrey thePrey) => thePrey.Prey.Identity.FullName;
            void TfToChild(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has shrunk");
            void VaginaDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)}");
            void BallsDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been fully transfomed into cum.");
            void BoobsDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} is now nothing but milk.");
            void StomachDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been digested.");
            void AnalDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been reduced to nothing in your bowels.");
        }

        private void PlayerPredEventLog(string text)
        {
            if (PlayerPred)
            {
                EventLog.AddTo(text);
            }
        }
    }

    [System.Serializable]
    public class ThePrey
    {
        [SerializeField] private BasicChar prey;
        public BasicChar Prey => prey;

        public void SetPrey(BasicChar value) => prey = value;

        [SerializeField] private float startWeight;
        public float StartWeight => startWeight;

        public ThePrey(BasicChar parPrey)
        {
            prey = parPrey;
            startWeight = parPrey.Weight;
        }

        /// <summary> First digest the fat, then the muscle and last the bones(height). </summary>
        /// <param name="toDigest">Amount to digest</param>
        /// <returns>Amount digested</returns>
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

        public float Progress => (StartWeight - Prey.Weight) / StartWeight;
    }
}