using TMPro;
using UnityEngine;

public class ShowGold : MonoBehaviour
{
    [SerializeField]
    private BasicChar owner = null;

    [SerializeField]
    private TextMeshProUGUI amountOfGold = null;

    private void OnEnable()
    {
        if (owner == null)
        {
            // if no owner assinged default to player
            owner = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
        owner.Currency.GoldChanged += UpdateGold;
    }

    private void OnDisable()
    {
        owner.Currency.GoldChanged -= UpdateGold;
    }

    private void UpdateGold()
    {
        amountOfGold.text = owner.Currency.Gold.ToString();
    }
}