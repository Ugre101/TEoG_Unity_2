using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public class VoreChar : MonoBehaviour
    {
        #region Containers

        [SerializeField]
        private VoreStomachContainer stomach = null;

        public VoreStomachContainer Stomach => stomach;

        [SerializeField]
        private VoreAnalContainer anal = null;

        public VoreAnalContainer Anal => anal;

        [SerializeField]
        private VoreVaginaContainer vagina = null;

        public VoreVaginaContainer Vagina => vagina;

        [SerializeField]
        private VoreBoobsContainer boobs = null;

        public VoreBoobsContainer Boobs => boobs;

        [SerializeField]
        private VoreBallsContainer balls = null;

        public VoreBallsContainer Balls => balls;

        #endregion Containers

        public VoreSaves Save()
        {
            return new VoreSaves(Balls.Preys, Anal.Preys, Boobs.Preys, Stomach.Preys, Vagina.Preys);
        }
    }

    [System.Serializable]
    public class VoreSave
    {
        public string name;
        public ThePrey prey;

        public VoreSave(string parName, ThePrey parPrey)
        {
            name = parName;
            prey = parPrey;
        }
    }

    [System.Serializable]
    public class VoreSaves
    {
        public List<VoreSave> balls;
        public List<VoreSave> anal;
        public List<VoreSave> boobs;
        public List<VoreSave> stomach;
        public List<VoreSave> vagina;

        public VoreSaves(List<ThePrey> ballPreys, List<ThePrey> analPreys, List<ThePrey> boobsPreys,
            List<ThePrey> stomachPreys, List<ThePrey> vaginaPreys)
        {
            foreach (ThePrey prey in ballPreys)
            {
                balls.Add(new VoreSave(prey.Prey.name, prey));
            }
            foreach (ThePrey prey in analPreys)
            {
                anal.Add(new VoreSave(prey.Prey.name, prey));
            }
            foreach (ThePrey prey in boobsPreys)
            {
                boobs.Add(new VoreSave(prey.Prey.name, prey));
            }
            foreach (ThePrey prey in stomachPreys)
            {
                stomach.Add(new VoreSave(prey.Prey.name, prey));
            }
            foreach (ThePrey prey in vaginaPreys)
            {
                vagina.Add(new VoreSave(prey.Prey.name, prey));
            }
        }
    }
}