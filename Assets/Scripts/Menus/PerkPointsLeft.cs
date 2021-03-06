﻿using TMPro;
using UnityEngine;

public class PerkPointsLeft : MonoBehaviour
{
    [SerializeField] private BasicChar player = null;
    [SerializeField] private TextMeshProUGUI textUGUI = null;
    private int lastLeft;
    private bool started = false;

    // Start is called before the first frame update
    private void Start()
    {
        textUGUI = textUGUI != null ? textUGUI : GetComponent<TextMeshProUGUI>();
        player = player != null ? player : PlayerMain.Player;
        ShowPoints();
        started = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if (started)
        {
            player.ExpSystem.PerkPointsChange += ShowPoints;
        }
    }

    private void OnDisable() => player.ExpSystem.PerkPointsChange -= ShowPoints;

    private void ShowPoints()
    {
        int perkPoints = player.ExpSystem.PerkPoints;
        if (lastLeft != perkPoints)
        {
            lastLeft = perkPoints;
            textUGUI.text = $"Perkpoints: {lastLeft}";
        }
    }
}