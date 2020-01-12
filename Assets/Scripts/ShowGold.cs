using TMPro;
using UnityEngine;

public class ShowGold : MonoBehaviour
{
    [SerializeField] private PlayerMain owner = null;

    [SerializeField] private TextMeshProUGUI amountOfGold = null;

    private void OnEnable()
    {
        owner = owner != null ? owner : PlayerMain.GetPlayer;
        owner.Currency.GoldChanged += UpdateGold;
        UpdateGold();
    }

    private void OnDisable() => owner.Currency.GoldChanged -= UpdateGold;

    private void UpdateGold() => amountOfGold.text = owner.Currency.Gold.ToString();
}