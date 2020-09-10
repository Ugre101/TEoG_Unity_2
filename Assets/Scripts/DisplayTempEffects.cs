using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vore;

namespace Ugre.GameUITempEffects
{
    public class DisplayTempEffects : MonoBehaviour
    {
        private static BasicChar Player => PlayerMain.Player;

        [SerializeField] private TempEffect tempEffectPrefab = null;
        [SerializeField] private PregEffect pregEffectPrefab = null;
        [SerializeField] private TempVore tempVorePrefab = null;

        [SerializeField] private Transform container = null;
        [SerializeField] private GameUIHoverText hoverText = null;
        private readonly List<DisplayMod> displayMods = new List<DisplayMod>();
        private readonly List<DisplayVore> displayVores = new List<DisplayVore>();

        // Start is called before the first frame update

        private int lastPreyCount = 0;

        private void OnEnable()
        {
            Save.LoadEvent += DisplayEffects;
            Player.Stats.GetAll.ForEach(s => { s.AddedTempEvent += DisplayEffects; });
            Player.Hp.AddedTempEvent += DisplayEffects;
            Player.Wp.AddedTempEvent += DisplayEffects;

            DisplayEffects();
            lastPreyCount = Player.Vore.TotalPreyCount;
        }

        private void OnDisable()
        {
            Save.LoadEvent -= DisplayEffects;
            Player.Stats.GetAll.ForEach(s => { s.AddedTempEvent -= DisplayEffects; });
            Player.Hp.AddedTempEvent -= DisplayEffects;
            Player.Wp.AddedTempEvent -= DisplayEffects;
        }

        private void Update()
        {
            if (Player.Vore.Active && lastPreyCount != Player.Vore.TotalPreyCount)
            {
                DisplayEffects();
                lastPreyCount = Player.Vore.TotalPreyCount;
            }
        }

        private void DisplayEffects()
        {
            container.KillChildren();
            hoverText.StopHovering();
            TempMods();
            VorePreys();
            // The rest
        }

        private void TempMods()
        {
            displayMods.Clear();
            foreach (List<TempStatMod> tempMods in Player.Stats.GetAll.Select(stat => stat.TempMods).Where(tempMods => tempMods.Count > 0))
            {
                foreach (TempStatMod m in tempMods)
                {
                    if (!displayMods.Exists(dp => dp.Source == m.Source))
                    {
                        displayMods.Add(new DisplayMod(m));
                    }
                    else
                    {
                    }
                }
            }
            List<Health> healths = new List<Health>() { Player.Hp, Player.Wp };
            foreach (List<TempHealthMod> tempMods in healths.Select(health => health.TempHealthMods).Where(tempMods => tempMods.Count > 0))
            {
                foreach (TempHealthMod m in tempMods.Where(m => !displayMods.Exists(dp => dp.Source == m.Source)))
                {
                    displayMods.Add(new DisplayMod(m));
                }
            }
            PrintDisplayMods();
        }

        private void VorePreys()
        {
            displayVores.Clear();
            foreach (VoreBasic vo in Player.Vore.VoreOrgans.Where(vo => vo.PreyCount > 0))
            {
                displayVores.Add(new DisplayVore(vo));
            }

            PrintDisplayVores();
        }

        private void PrintDisplayMods() => displayMods.ForEach(dm => Instantiate(tempEffectPrefab, container).Setup(dm, hoverText));

        private void PrintDisplayVores() => displayVores.ForEach(dv => Instantiate(tempVorePrefab, container).Setup(dv, hoverText));
    }
}