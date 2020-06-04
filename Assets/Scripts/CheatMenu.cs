using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private Button closeBtn = null, goldCheat = null, femiCheat = null, mascCheat = null, expCheat = null;

    // Start is called before the first frame update
    private void Start()
    {
        player = player != null ? player : PlayerHolder.Player;
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
    }

    private void Gold() => player.Currency.Gold += 1000;

    private void Femi() => player.Essence.Femi.Gain(1000);

    private void Masc() => player.Essence.Masc.Gain(1000);

    private void Exp() => player.ExpSystem.GainExp(1000);
}