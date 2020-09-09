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
        [SerializeField] private ExpSystem voreExp = new ExpSystem();
        public ExpSystem VoreExp => voreExp;

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
        private List<VoreBasic> voreOrgans;

        public List<VoreBasic> VoreOrgans => voreOrgans = voreOrgans ?? new List<VoreBasic>() { Balls, Boobs, Stomach, Anal, Vagina };

        public VoreBasic GetVoreOrgan(VoreContainers container) => VoreOrgans.Find(v => v.VoreContainers == container);

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
            balls.PassOnExp += voreExp.GainExp;
            boobs.PassOnExp += voreExp.GainExp;
            stomach.PassOnExp += voreExp.GainExp;
            anal.PassOnExp += voreExp.GainExp;
            vagina.PassOnExp += voreExp.GainExp;
        }

        private bool? playerIsPred;

        private bool PlayerIsPred()
        {
            if (!playerIsPred.HasValue)
            {
                playerIsPred = pred.Identity.Id == PlayerMain.PlayerId;
            }
            return playerIsPred.Value;
        }

        public void Digest(int times)
        {
            Balls.Digest(p => BallsDigested(p), times);
            Boobs.Digest(p => BoobsDigested(p), times);
            Stomach.Digest(p => StomachDigested(p), times);
            Anal.Digest(p => AnalDigested(p), times);
            if (Vagina.ChildTf)
            {
                vagina.TransformToChild(p => TfToChild(p), times);
            }
            else
            {
                Vagina.Digest(p => VaginaDigested(p), times);
            }
            void TfToChild(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has shrunk");//    pred.VoreChar.Vagina.PreyIsRebithed(thePrey);

            void VaginaDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)}");//   pred.VoreChar.Vagina.PreyIsdigested(thePrey);

            void BallsDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been fully transfomed into cum.");//   pred.VoreChar.Balls.PreyIsdigested(thePrey);

            void BoobsDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} is now nothing but milk.");//   pred.VoreChar.Boobs.PreyIsdigested(thePrey);

            void StomachDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been digested.");//  pred.VoreChar.Stomach.PreyIsdigested(thePrey);

            void AnalDigested(ThePrey thePrey) => PlayerPredEventLog($"{FullName(thePrey)} has been reduced to nothing in your bowels.");//  pred.VoreChar.Anal.PreyIsdigested(thePrey);
        }

        private string FullName(ThePrey thePrey) => thePrey.Prey.Identity.FullName;

        private void PlayerPredEventLog(string text)
        {
            if (PlayerIsPred())
            {
                EventLog.AddTo(text);
            }
        }

        [SerializeField] private VorePerksSystem perks = new VorePerksSystem();
        public VorePerksSystem Perks => perks;
    }

    [System.Serializable]
    public class ThePrey
    {
        [SerializeField] private float startWeight;
        [SerializeField] private BasicChar prey;
        public float StartWeight => startWeight;
        public BasicChar Prey => prey;

        public ThePrey(BasicChar basicChar)
        {
            //  JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(basicChar), this);
            startWeight = basicChar.Body.Weight;
            prey = basicChar;
        }

        /// <summary> First digest the fat, then the muscle and last the bones(height). </summary>
        /// <param name="toDigest">Amount to digest</param>
        /// <returns>Amount digested</returns>
        public float Digest(float toDigest)
        {
            Body body = Prey.Body;
            float fatGain = Mathf.Min(toDigest, body.Weight);
            while (toDigest > 0 && (body.Fat.Value > 0 || body.Muscle.Value > 0 || body.Height.Value > 0))
            {
                toDigest -= body.Fat.LoseFlat(toDigest);
                toDigest -= body.Muscle.LoseFlat(toDigest);
                toDigest -= body.Height.LoseFlat(toDigest);
            }
            return fatGain;
        }

        public float Progress => (StartWeight - Prey.Body.Weight) / StartWeight;

        public string PreyDesc
        {
            get
            {
                string desc = string.Empty;
                if (!(Progress > 0)) return desc;
                desc += Prey.Identity.FirstName;
                if (Progress < 0.3f)
                {
                    desc += $" has started to show signs of their digestion "; //Starting to digest
                }
                else if (Progress < 0.5f)
                {
                    desc += $" is halfway trough their digestion "; // Almost half digested check amount fat & muscle
                }
                else if (Progress < 0.8f)
                {
                    desc += $"  "; // More than half way digested
                }
                else
                {
                    desc += $""; // Almost fully digested
                }
                if (Prey.Body.Fat.Value < 1 && Prey.Body.Muscle.Value < 1)
                {
                    desc += $" all of {Prey.HisHer()} fat and muscle has melted of their bones.";
                }
                else if (Prey.Body.Fat.Value < 1)
                {
                    desc += " all fat has melted of ";
                }
                else if (Prey.Body.Muscle.Value < 1)
                {
                    desc += $" all muscle has melted of {Prey.HisHer()} body ";
                }
                return desc;
            }
        }
    }
}