using TMPro;
using UnityEngine;

public class ShowGold : MonoBehaviour
{
    private BasicChar owner => PlayerMain.Player;

    [SerializeField] private TextMeshProUGUI amountOfGold = null;

    private void OnEnable()
    {
        owner.Currency.GoldChanged += UpdateGold;
        UpdateGold(owner.Currency.Gold);
    }

    private void OnDisable() => owner.Currency.GoldChanged -= UpdateGold;

    private void UpdateGold(float gold) => amountOfGold.text = gold.ToString();
}