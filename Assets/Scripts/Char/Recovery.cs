using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HealthRecovery
{
    [System.Serializable]
    public class Recovery : IntStat
    {
        public Recovery()
        {
            baseValue = 5;
        }

        [SerializeField] private List<HealthMod> healthMods = new List<HealthMod>();
        [SerializeField] private List<TempHealthMod> tempHealthMods = new List<TempHealthMod>();

        public List<HealthMod> Mods => healthMods;
        public List<TempHealthMod> TempMods => tempHealthMods;

        protected override int GetCalcValue()
        {
            float flat = BaseValue
                + Mods.FindAll(m => m.ModType == ModTypes.Flat).Sum(m => m.Value)
                + TempMods.FindAll(m => m.ModType == ModTypes.Flat).Sum(m => m.Value);
            float precent = 1f
                + Mods.FindAll(m => m.ModType == ModTypes.Precent).Sum(m => m.Value)
                + TempMods.FindAll(m => m.ModType == ModTypes.Precent).Sum(m => m.Value);
            return Mathf.FloorToInt(flat * precent);
        }

        #region AddAndRemoveMods

        public void AddMods(HealthMod mod)
        {
            Mods.Add(mod);
            IsDirty = true;
        }

        public void AddTempMod(TempHealthMod mod)
        {
            if (TempMods.Exists(tm => tm.Source.Equals(mod.Source)))
            {
                TempHealthMod toChange = TempMods.Find(tm => tm.Source.Equals(mod.Source));
                float diminishingReturn = (float)toChange.Duration / (float)mod.Duration;
                int toIncrease = Mathf.Max(0, Mathf.FloorToInt(mod.Duration / Mathf.Max(1, 2 * diminishingReturn)));
                toChange.IncreaseDuration(toIncrease);
            }
            else
            {
                // Clone otherwise diminishingReturn doesn't work as duration increase on both.
                TempMods.Add(new TempHealthMod(mod.Value, mod.ModType, mod.HealthType, mod.Source, mod.Duration));
            }
            IsDirty = true;
        }

        public void RemoveMods(HealthMod mod)
        {
            Mods.Remove(mod);
            IsDirty = true;
        }

        public void RemoveTempMods(TempHealthMod mod)
        {
            TempMods.Remove(mod);
            IsDirty = true;
        }

        public bool RemoveFromSource(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (!Mods.Exists(sm => sm.Source.Equals(source))) return false;
            
            foreach (HealthMod sm in Mods.FindAll(s => s.Source.Equals(source)))
            {
                Mods.Remove(sm);
            }

            IsDirty = true;
            return true;
        }

        public bool RemoveTempFromSource(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (!TempMods.Exists(sm => sm.Source.Equals(source))) return false;
            
            foreach (TempHealthMod sm in TempMods.FindAll(s => s.Source.Equals(source)))
            {
                TempMods.Remove(sm);
            }

            IsDirty = true;
            return true;
        }

        #endregion AddAndRemoveMods
    }

    public static class RecoveryExtensions
    {
        public static int HpRecoveryTotal(this BasicChar basicChar, int times = 1)
        {
            int baseVal = basicChar.Hp.Recovery.Value;
            if (basicChar.Perks.HasPerk(PerksTypes.Gluttony))
            {
                baseVal += PerkEffects.Gluttony.ExtraRecovery(basicChar.Perks);
            }
            return baseVal * times;
        }

        public static int WpRecoveryTotal(this BasicChar basicChar, int times = 1)
        {
            int baseVal = basicChar.Wp.Recovery.Value;
            if (basicChar.Perks.HasPerk(PerksTypes.Gluttony))
            {
                baseVal += PerkEffects.Gluttony.ExtraRecovery(basicChar.Perks);
            }
            return baseVal * times;
        }
    }
}