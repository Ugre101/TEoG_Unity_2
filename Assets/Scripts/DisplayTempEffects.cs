using System.Collections.Generic;
using UnityEngine;

public class DisplayTempEffects : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private TempEffect tempEffectPrefab = null;

    [SerializeField]
    private TempVore tempVorePrefab = null;

    [SerializeField]
    private Transform container = null;

    private readonly List<DisplayMod> displayMods = new List<DisplayMod>();
    private readonly List<DisplayVore> displayVores = new List<DisplayVore>();

    // Start is called before the first frame update
    private void Start()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
        AddedEffectEvent += DisplayEffects;
        SaveMananger.GameLoaded += DisplayEffects;
        DisplayEffects();
    }

    private int ModsICareAbout()
    {
        int Icare = 0;
        player.Stats.GetAll.ForEach(m => { Icare += m.TempMods.Count; });
        Icare += player.WP.TempHealthMods.Count;
        Icare += player.HP.TempHealthMods.Count;
        return Icare;
    }

    private int lastCount = 0;
    private int lastPreyCount = 0;

    private void Update()
    {
        if (lastCount != ModsICareAbout())
        {
            DisplayEffects();
            lastCount = ModsICareAbout();
        }
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
            if (stat.TempMods.Count > 0)
            {
                stat.TempMods.ForEach(m =>
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
        foreach (Health h in healths)
        {
            if (h.TempHealthMods.Count > 0)
            {
                h.TempHealthMods.ForEach(m =>
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
            if (vo.Preys.Count > 0)
            {
                displayVores.Add(new DisplayVore(vo));
            }
        });
        PrintDisplayVores();
    }

    private void PrintDisplayMods()
    {
        foreach (DisplayMod dm in displayMods)
        {
            TempEffect te = Instantiate(tempEffectPrefab, container);
            te.Setup(dm);
        }
    }

    private void PrintDisplayVores()
    {
        displayVores.ForEach(dv =>
        {
            TempVore tv = Instantiate(tempVorePrefab, container);
            tv.Setup(dv);
        });
    }

    public delegate void AddedATempEffect();

    private static event AddedATempEffect AddedEffectEvent;

    public static void AddedEffect() => AddedEffectEvent?.Invoke();
}