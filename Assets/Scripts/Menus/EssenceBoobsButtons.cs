﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBoobsButtons : MonoBehaviour
{
    public GameObject prefab;
    public PlayerMain player;
    private Essence Femi => player.Essence.Femi;
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
        if (lastAmount != player.SexualOrgans.Boobs.Count)
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
        GameObject AddBoobs = Instantiate(prefab, this.transform);
        Button AddBtn = AddBoobs.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddBoobs.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add boobs: {player.SexualOrgans.Boobs.Cost()}Femi";
        foreach (Boobs b in player.SexualOrgans.Boobs)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{Settings.MorInch(b.Size)} {b.Cost}Femi";
            btn.onClick.AddListener(() => GrowBoobs(b, t));
        }
        lastAmount = player.SexualOrgans.Boobs.Count;
    }

    private void GrowBoobs(Boobs b, TextMeshProUGUI t)
    {
        if (Femi.Amount >= b.Cost)
        {
            b.Grow();
            t.text = $"{Settings.MorInch(b.Size)} {b.Cost}Femi";
        }
    }

    private void AddFunc()
    {
        if (Femi.Amount > player.SexualOrgans.Boobs.Cost())
        {
            Femi.Lose(player.SexualOrgans.Boobs.Cost());
            player.SexualOrgans.Boobs.AddBoobs();
            AddText.text = $"Add boobs: {player.SexualOrgans.Boobs.Cost()}Femi";
        }
    }
}