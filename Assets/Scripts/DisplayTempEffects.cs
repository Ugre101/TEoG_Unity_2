using System.Collections.Generic;
using UnityEngine;

public class DisplayTempEffects : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player;

    // Start is called before the first frame update
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
        AddedEffectEvent += DisplayEffects;
    }
    private void DisplayEffects()
    {
        TempMods();
        // The rest
    }

    private void TempMods()
    {
        List<DisplayMod> displayMods = new List<DisplayMod>();
        foreach (CharStats stat in player.Stats.GetAll)
        {
            if (stat.TempMods.Count > 0)
            {
                stat.TempMods.ForEach(m =>
                {
                    if (!displayMods.Exists(dp => dp.Source == m.Source))
                    {
                        displayMods.Add(new DisplayMod(m.Source, m.Duration));
                    }
                });
            }
        }
    }

    private class DisplayMod
    {
        public DisplayMod(string parSource, int parDuration)
        {
            Source = parSource;
            Duration = parDuration;
        }

        public string Source { get; private set; }
        public int Duration { get; private set; }
    }

    public delegate void AddedATempEffect();

    private static event AddedATempEffect AddedEffectEvent;

    public static void AddedEffect() => AddedEffectEvent?.Invoke();
}