﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBallsButtons : MonoBehaviour
{
    public GameObject prefab;
    public playerMain player;
    public Settings settings;
    private int lastAmount;
    private TextMeshProUGUI AddText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateButtons();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastAmount != player.Balls.Count)
        {
            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject AddBalls = Instantiate(prefab, this.transform);
        Button AddBtn = AddBalls.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddBalls.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add balls: {player.BallCost()}";
        foreach (Balls b in player.Balls)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Masc";
            btn.onClick.AddListener(() => GrowBalls(b, t));
        }
        lastAmount = player.Balls.Count;
    }

    private void GrowBalls(Balls b, TextMeshProUGUI t)
    {
        if (player.Masc.Amount >= b.Cost)
        {
            b.Grow();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Masc";
        }
    }

    private void AddFunc()
    {
        if (player.Masc.Amount > player.BallCost())
        {
            player.Masc.Lose(player.BallCost());
            player.AddBalls();
            AddText.text = $"Add balls: {player.BallCost()}Masc";
        }
    }
}