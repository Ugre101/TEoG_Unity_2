using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    private static BasicChar Player => PlayerMain.Player;
    [SerializeField] private Button closeBtn = null, goldCheat = null, femiCheat = null, mascCheat = null, expCheat = null, stableEssCheat = null, unlockAllTeleports = null;

    // Start is called before the first frame update
    private void Start()
    {
        closeBtn.onClick.AddListener(CanvasMain.GetCanvasMain.Resume);
        if (goldCheat != null) goldCheat.onClick.AddListener(Gold);
        if (femiCheat != null) femiCheat.onClick.AddListener(Femi);
        if (mascCheat != null) mascCheat.onClick.AddListener(Masc);
        if (expCheat != null) expCheat.onClick.AddListener(Exp);
        if (stableEssCheat != null) stableEssCheat.onClick.AddListener(StableEssence);
        if (unlockAllTeleports != null) unlockAllTeleports.onClick.AddListener(UnlockTeleports);
    }

    private static void Gold() => Player.Currency.Gold += 1000;

    private static void StableEssence() => Player.Essence.StableEssence.BaseValue += 1000;

    private static void Femi() => Player.Essence.Femi.Gain(1000);

    private static void Masc() => Player.Essence.Masc.Gain(1000);

    private static void Exp() => Player.ExpSystem.GainExp(1000);

    private static void UnlockTeleports() => MapEvents.GetMapEvents.UnlockTeleports();
}