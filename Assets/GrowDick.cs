using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrowDick : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private PlayerMain player;
    private Essence Masc => player.Essence.Masc;
    private Dick dick;

    public void Setup(PlayerMain player, Dick dick)
    {
        this.player = player;
        this.dick = dick;
        DisplayCost();
        btn.onClick.AddListener(Grow);
    }

    private void DisplayCost()
    {
        btnText.text = $"{Settings.MorInch(dick.Size)} {dick.Cost}Masc";
    }

    private void Grow()
    {
        if (Masc.Amount >= dick.Cost)
        {
            dick.Grow();
            DisplayCost();
        }
    }
}