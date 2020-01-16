using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public class VoreChar : MonoBehaviour
    {
        #region Containers

        [SerializeField] private VoreStomachContainer stomach = null;

        public VoreStomachContainer Stomach => stomach;

        [SerializeField] private VoreAnalContainer anal = null;

        public VoreAnalContainer Anal => anal;

        [SerializeField] private VoreVaginaContainer vagina = null;

        public VoreVaginaContainer Vagina => vagina;

        [SerializeField] private VoreBoobsContainer boobs = null;

        public VoreBoobsContainer Boobs => boobs;

        [SerializeField] private VoreBallsContainer balls = null;

        public VoreBallsContainer Balls => balls;

        #endregion Containers

        public VoreSaves Save => new VoreSaves(Balls.GetPreys(), Anal.GetPreys(), Boobs.GetPreys(), Stomach.GetPreys(), Vagina.GetPreys());

        public void Load(VoreSaves parSaves, BasicChar pred)
        {
            VoreEngine vore = pred.Vore;
            LoadPreys(Stomach, parSaves.Stomach, vore.Stomach.Preys);
            LoadPreys(Anal, parSaves.Anal, vore.Anal.Preys);
            LoadPreys(Vagina, parSaves.Vagina, vore.Vagina.Preys);
            LoadPreys(Boobs, parSaves.Boobs, vore.Boobs.Preys);
            LoadPreys(Balls, parSaves.Balls, vore.Balls.Preys);
        }

        [SerializeField] private List<BasicChar> preyPrefabs = new List<BasicChar>();

        [SerializeField] private BasicChar defaultPrefab = null;

        private void LoadPreys(VoreContainer container, List<VoreSave> saves, List<ThePrey> preys)
        {
            container.transform.KillChildren();
            for (int i = 0; i < saves.Count; i++)
            {
                VoreSave vs = saves[i];
                BasicChar loaded = preyPrefabs.Exists(n => n.name == vs.Name)
                    ? InstantiateVoreChar(container, vs, preyPrefabs.Find(n => n.name == vs.Name))
                    : InstantiateVoreChar(container, vs, defaultPrefab);
                preys[i].SetPrey(loaded);
            }
        }

        private BasicChar InstantiateVoreChar(VoreContainer container, VoreSave vs, BasicChar basicChar)
        {
            BasicChar loaded = Instantiate(basicChar, container.transform);
            loaded.name = vs.Name;
            JsonUtility.FromJsonOverwrite(vs.Prey, loaded);
            return loaded;
        }
    }

    [System.Serializable]
    public class VoreSave
    {
        [SerializeField] private string name;
        [SerializeField] private string prey;

        public string Name => name;
        public string Prey => prey;

        public VoreSave(string parName, BasicChar parPrey)
        {
            name = parName;
            prey = JsonUtility.ToJson(parPrey);
        }
    }

    [System.Serializable]
    public class VoreSaves
    {
        [SerializeField] private List<VoreSave> balls = new List<VoreSave>();
        [SerializeField] private List<VoreSave> anal = new List<VoreSave>();
        [SerializeField] private List<VoreSave> boobs = new List<VoreSave>();
        [SerializeField] private List<VoreSave> stomach = new List<VoreSave>();
        [SerializeField] private List<VoreSave> vagina = new List<VoreSave>();

        public List<VoreSave> Balls => balls;
        public List<VoreSave> Anal => anal;
        public List<VoreSave> Boobs => boobs;
        public List<VoreSave> Stomach => stomach;
        public List<VoreSave> Vagina => vagina;

        public VoreSaves(List<BasicChar> ballPreys, List<BasicChar> analPreys, List<BasicChar> boobsPreys, List<BasicChar> stomachPreys, List<BasicChar> vaginaPreys)
        {
            foreach (BasicChar prey in ballPreys)
            {
                balls.Add(SaveName(prey));
            }
            foreach (BasicChar prey in analPreys)
            {
                anal.Add(SaveName(prey));
            }
            foreach (BasicChar prey in boobsPreys)
            {
                boobs.Add(SaveName(prey));
            }
            foreach (BasicChar prey in stomachPreys)
            {
                stomach.Add(SaveName(prey));
            }
            foreach (BasicChar prey in vaginaPreys)
            {
                vagina.Add(SaveName(prey));
            }
        }

        private VoreSave SaveName(BasicChar prey) => new VoreSave(prey.name, prey);
    }
}