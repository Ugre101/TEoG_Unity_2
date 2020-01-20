using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrowBalls : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private PlayerMain player;
    private Essence Masc => player.Essence.Masc;
    private Balls balls;

    public void Setup(PlayerMain player, Balls balls)
    {
        this.player = player;
        this.balls = balls;
        DisplayCost();
        btn.onClick.AddListener(Grow);
    }

    private void DisplayCost()
    {
        btnText.text = $"{Settings.MorInch(balls.Size)} {balls.Cost}Masc";
    }

    private void Grow()
    {
        if (Masc.Amount >= balls.Cost)
        {
            balls.Grow();
            DisplayCost();
        }
    }
}