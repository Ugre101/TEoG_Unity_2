using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private BasicChar player => PlayerMain.Player;
    [SerializeField] private Button closeBtn = null, goldCheat = null, femiCheat = null, mascCheat = null, expCheat = null, stableEssCheat = null, unlockAllTeleports = null;

    // Start is called before the first frame update
    private void Start()
    {
        closeBtn.onClick.AddListener(CanvasMain.GetCanvasMain.Resume);
        if (goldCheat != null)
        {
            goldCheat.onClick.AddListener(Gold);
        }
        if (femiCheat != null)
        {
            femiCheat.onClick.AddListener(Femi);
        }
        if (mascCheat != null)
        {
            mascCheat.onClick.AddListener(Masc);
        }
        if (expCheat != null)
        {
            expCheat.onClick.AddListener(Exp);
        }
        if (stableEssCheat != null)
        {
            stableEssCheat.onClick.AddListener(StableEssence);
        }
        if (unlockAllTeleports != null)
        {
            unlockAllTeleports.onClick.AddListener(UnlockTeleports);
        }
    }

    private void Gold() => player.Currency.Gold += 1000;

    private void StableEssence() => player.Essence.StableEssence.BaseValue += 1000;

    private void Femi() => player.Essence.Femi.Gain(1000);

    private void Masc() => player.Essence.Masc.Gain(1000);

    private void Exp() => player.ExpSystem.GainExp(1000);

    private void UnlockTeleports() => MapEvents.GetMapEvents.UnlockTeleports();
}