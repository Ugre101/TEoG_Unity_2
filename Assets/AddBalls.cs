using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddBalls : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private PlayerMain player;
    private Essence Masc => player.Essence.Masc;
    private List<Balls> Balls => player.SexualOrgans.Balls;

    public void Setup(PlayerMain player)
    {
        this.player = player;
        DisplayCost();
        btn.onClick.AddListener(AddFunc);
    }

    private void DisplayCost() => btnText.text = $"Add balls: {Balls.Cost()}";

    private void AddFunc()
    {
        if (Masc.Amount > Balls.Cost())
        {
            Masc.Lose(Balls.Cost());
            Balls.AddBalls();
            DisplayCost();
        }
    }
}