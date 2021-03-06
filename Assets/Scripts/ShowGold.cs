﻿using TMPro;
using UnityEngine;

public class ShowGold : MonoBehaviour
{
    [SerializeField] private BasicChar owner = null;

    [SerializeField] private TextMeshProUGUI amountOfGold = null;

    private void OnEnable()
    {
        owner = owner != null ? owner : PlayerMain.Player;
        owner.Currency.GoldChanged += UpdateGold;
        UpdateGold(owner.Currency.Gold);
    }

    private void OnDisable() => owner.Currency.GoldChanged -= UpdateGold;

    private void UpdateGold(float gold) => amountOfGold.text = gold.ToString();
}