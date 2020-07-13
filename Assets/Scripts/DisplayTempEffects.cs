using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DisplayTempEffects : MonoBehaviour
    {
        private PlayerMain player => PlayerHolder.Player;

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
            player.Stats.GetAll.ForEach(s => { s.AddedTempEvent += DisplayEffects; });
            player.HP.AddedTempEvent += DisplayEffects;
            player.WP.AddedTempEvent += DisplayEffects;

            DisplayEffects();
            lastPreyCount = player.Vore.TotalPreyCount;
        }

        private void OnDisable()
        {
            Save.LoadEvent -= DisplayEffects;
            player.Stats.GetAll.ForEach(s => { s.AddedTempEvent -= DisplayEffects; });
            player.HP.AddedTempEvent -= DisplayEffects;
            player.WP.AddedTempEvent -= DisplayEffects;
        }

        private void Update()
        {
            if (player.Vore.Active && lastPreyCount != player.Vore.TotalPreyCount)
            {
                DisplayEffects();
                lastPreyCount = player.Vore.TotalPreyCount;
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

        private void PrintDisplayMods() => displayMods.ForEach(dm => Instantiate(tempEffectPrefab, container).Setup(dm, hoverText));

        private void PrintDisplayVores() => displayVores.ForEach(dv => Instantiate(tempVorePrefab, container).Setup(dv, hoverText));
    }
}