using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DisplayTempEffects : MonoBehaviour
    {
        [SerializeField] private PlayerMain player = null;

        [SerializeField] private TempEffect tempEffectPrefab = null;

        [SerializeField] private TempVore tempVorePrefab = null;

        [SerializeField] private Transform container = null;

        private readonly List<DisplayMod> displayMods = new List<DisplayMod>();
        private readonly List<DisplayVore> displayVores = new List<DisplayVore>();

        // Start is called before the first frame update
        private void Start()
        {
            player = player != null ? player : PlayerMain.GetPlayer;
            Save.LoadEvent += DisplayEffects;
            player.Stats.GetAll.ForEach(s => { s.AddedTempEvent += DisplayEffects; });
            player.HP.AddedTempEvent += DisplayEffects;
            player.WP.AddedTempEvent += DisplayEffects;
            DisplayEffects();
        }

        private int lastPreyCount = 0;

        private void OnEnable()
        {
            if (player != null)
            {
                DisplayEffects();
                lastPreyCount = player.Vore.TotalPreyCount;
            }
        }

        private void Update()
        {
            if (player.Vore.Active)
            {
                if (lastPreyCount != player.Vore.TotalPreyCount)
                {
                    DisplayEffects();
                    lastPreyCount = player.Vore.TotalPreyCount;
                }
            }
        }

        private void DisplayEffects()
        {
            container.KillChildren();
            TempMods();
            VorePreys();
            // The rest
        }

        private void TempMods()
        {
            displayMods.Clear();
            foreach (CharStats stat in player.Stats.GetAll)
            {
                List<TempStatMod> tempMods = stat.TempMods;
                if (tempMods.Count > 0)
                {
                    tempMods.ForEach(m =>
                    {
                        if (!displayMods.Exists(dp => dp.Source == m.Source))
                        {
                            displayMods.Add(new DisplayMod(m));
                        }
                        else
                        {
                        }
                    });
                }
            }
            List<Health> healths = new List<Health>() { player.HP, player.WP };
            foreach (Health health in healths)
            {
                List<TempHealthMod> tempMods = health.TempHealthMods;
                if (tempMods.Count > 0)
                {
                    tempMods.ForEach(m =>
                    {
                        if (!displayMods.Exists(dp => dp.Source == m.Source))
                        {
                            displayMods.Add(new DisplayMod(m));
                        }
                    });
                }
            }
            PrintDisplayMods();
        }

        private void VorePreys()
        {
            displayVores.Clear();
            player.Vore.VoreOrgans.ForEach(vo =>
            {
                if (vo.PreyCount > 0)
                {
                    displayVores.Add(new DisplayVore(vo));
                }
            });
            PrintDisplayVores();
        }

        private void PrintDisplayMods() => displayMods.ForEach(dm => Instantiate(tempEffectPrefab, container).Setup(dm));

        private void PrintDisplayVores() => displayVores.ForEach(dv => Instantiate(tempVorePrefab, container).Setup(dv));
    }
}