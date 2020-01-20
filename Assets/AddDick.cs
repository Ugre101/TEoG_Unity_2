using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddDick : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private PlayerMain player;
    private Essence Masc => player.Essence.Masc;

    public void Setup(PlayerMain player)
    {
        this.player = player;
        DisplayCost();
        btn.onClick.AddListener(AddFunc);
    }

    private void AddFunc()
    {
        if (Masc.Amount > player.SexualOrgans.Dicks.Cost())
        {
            Masc.Lose(player.SexualOrgans.Dicks.Cost());
            player.SexualOrgans.Dicks.AddDick();
            DisplayCost();
        }
    }

    private void DisplayCost()
    {
        btnText.text = $"Add dick: {player.SexualOrgans.Dicks.Cost()}";
    }
}