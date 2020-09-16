using TMPro;
using UnityEngine;

public class ShowGold : MonoBehaviour
{
    private static BasicChar Owner => PlayerMain.Player;

    [SerializeField] private TextMeshProUGUI amountOfGold = null;

    private void OnEnable()
    {
        Owner.Currency.GoldChanged += UpdateGold;
        UpdateGold(Owner.Currency.Gold);
    }

    private void OnDisable() => Owner.Currency.GoldChanged -= UpdateGold;

    private void UpdateGold(float gold) => amountOfGold.text = gold.ToString();
}