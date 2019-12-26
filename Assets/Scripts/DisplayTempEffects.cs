using System.Collections.Generic;
using UnityEngine;

public partial class DisplayTempEffects : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private TempEffect tempEffectPrefab = null;

    [SerializeField]
    private Transform container = null;

    private List<DisplayMod> displayMods = new List<DisplayMod>();

    // Start is called before the first frame update
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
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

    private void Update()
    {
        if (lastCount != ModsICareAbout())
        {
            DisplayEffects();
            lastCount = ModsICareAbout();
        }
    }

    private void DisplayEffects()
    {
        TempMods();
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

    private void PrintDisplayMods()
    {
        container.KillChildren();
        Debug.Log(displayMods.Count);
        foreach (DisplayMod dm in displayMods)
        {
            TempEffect te = Instantiate(tempEffectPrefab, container);
            te.Setup(dm);
        }
    }

    public delegate void AddedATempEffect();

    private static event AddedATempEffect AddedEffectEvent;

    public static void AddedEffect() => AddedEffectEvent?.Invoke();
}