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
            return new VoreSaves(Balls.GetPreys(), Anal.GetPreys(), Boobs.GetPreys(), Stomach.GetPreys(), Vagina.GetPreys());
        }

        public void Load(VoreSaves parSaves, BasicChar pred)
        {
            LoadPreys(Stomach, parSaves.stomach, pred.Vore.Stomach.Preys);
            LoadPreys(Anal, parSaves.anal, pred.Vore.Anal.Preys);
            LoadPreys(Vagina, parSaves.vagina, pred.Vore.Vagina.Preys);
            LoadPreys(Boobs, parSaves.boobs, pred.Vore.Boobs.Preys);
            LoadPreys(Balls, parSaves.balls, pred.Vore.Balls.Preys);
        }

        [SerializeField]
        private List<BasicChar> preyPrefabs = new List<BasicChar>();

        [SerializeField]
        private BasicChar defaultPrefab = null;

        private void LoadPreys(VoreContainer container, List<VoreSave> saves, List<ThePrey> preys)
        {
            container.transform.KillChildren();
            for (int i = 0; i < saves.Count; i++)
            {
                VoreSave vs = saves[i];
                BasicChar loaded;
                if (preyPrefabs.Exists(n => n.name == vs.name))
                {
                    loaded = Instantiate(preyPrefabs.Find(n => n.name == vs.name), container.transform);
                    loaded.name = vs.name;
                    JsonUtility.FromJsonOverwrite(vs.prey, loaded);
                }
                else
                {
                    loaded = Instantiate(defaultPrefab, container.transform);
                    loaded.name = vs.name;
                    JsonUtility.FromJsonOverwrite(vs.prey, loaded);
                }
                preys[i].SetPrey = loaded;
            }
        }
    }

    [System.Serializable]
    public class VoreSave
    {
        public string name;
        public string prey;

        public VoreSave(string parName, BasicChar parPrey)
        {
            name = parName;
            prey = JsonUtility.ToJson(parPrey);
        }
    }

    [System.Serializable]
    public class VoreSaves
    {
        public List<VoreSave> balls = new List<VoreSave>();
        public List<VoreSave> anal = new List<VoreSave>();
        public List<VoreSave> boobs = new List<VoreSave>();
        public List<VoreSave> stomach = new List<VoreSave>();
        public List<VoreSave> vagina = new List<VoreSave>();

        public VoreSaves(List<BasicChar> ballPreys, List<BasicChar> analPreys, List<BasicChar> boobsPreys, List<BasicChar> stomachPreys, List<BasicChar> vaginaPreys)
        {
            foreach (BasicChar prey in ballPreys)
            {
                balls.Add(new VoreSave(prey.name, prey));
            }
            foreach (BasicChar prey in analPreys)
            {
                anal.Add(new VoreSave(prey.name, prey));
            }
            foreach (BasicChar prey in boobsPreys)
            {
                boobs.Add(new VoreSave(prey.name, prey));
            }
            foreach (BasicChar prey in stomachPreys)
            {
                stomach.Add(new VoreSave(prey.name, prey));
            }
            foreach (BasicChar prey in vaginaPreys)
            {
                vagina.Add(new VoreSave(prey.name, prey));
            }
        }
    }
}