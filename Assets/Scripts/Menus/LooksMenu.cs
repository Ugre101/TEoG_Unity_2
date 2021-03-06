﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LooksMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI looksText = null;

    private BasicChar Player => PlayerMain.Player;
    //  [SerializeField] private Button toggleExact = null;

    //    private bool exactDetails = false;

    [SerializeField] private Button sortAllBtn = null, sortBodyBtn = null, sortOrgansBtn = null, sortPregBtn = null;

    private void Start()
    {
        // toggleExact.onClick.AddListener(() => exactDetails = !exactDetails);
        sortAllBtn.onClick.AddListener(() => looksText.text = Summary);
        sortBodyBtn.onClick.AddListener(() => looksText.text = BodyLook());
        sortOrgansBtn.onClick.AddListener(() => looksText.text = SexOrgans());
        sortPregBtn.onClick.AddListener(() => looksText.text = PregnancyLook());
    }

    private void OnEnable()
    {
        if (looksText != null && Player != null)
        {
            looksText.text = Summary;
        }
    }

    private string Summary => Player.Summary() + "\n\n" + BodyLook() + "\n\n" + SexOrgans() + "\n\n" + PregnancyLook();

    private string BodyLook()
    {
        string body = $"";
        return body;
    }

    private string SexOrgans()
    {
        string toReturn = " ";
        SexualOrgans so = Player.SexualOrgans;
        if (so.HaveBoobs())
        {
            toReturn += so.Boobs.Looks + "\n\n";
        }
        if (so.HaveVagina())
        {
            toReturn += so.Vaginas.Looks + "\n\n";
        }
        if (so.HaveDick())
        {
            toReturn += so.Dicks.Looks + "\n\n";
        }
        if (so.HaveBalls())
        {
            toReturn += so.Balls.Looks + "\n\n";
        }
        return toReturn;
    }

    private string PregnancyLook()
    {
        PregnancySystem pregnancySystem = Player.PregnancySystem;
        string pregLook = $"Virility: {pregnancySystem.Virility.Value}\n" +
            $"Fertility: {pregnancySystem.Fertility.Value}\n\n";
        if (Player.Pregnant)
        {
            Player.SexualOrgans.Vaginas.List.FindAll(v => v.Womb.HasFetus).ForEach(vag => pregLook += FetusDesc(vag) + "\n");
        }
        List<Child> children = pregnancySystem.Children;
        if (children.Count > 0)
        {
            pregLook += $"\n You have {children.Count} children.";
            //TODO make a more advanced menu
        }
        return pregLook;
    }

    private string FetusDesc(Vagina pregVag)
    {
        int index = Player.SexualOrgans.Vaginas.List.IndexOf(pregVag);
        if (pregVag.Womb.Fetuses.Count > 1)
        {
            // TODO add text for multitude of children
        }
        return $"The unborn child inside your {index.FirstSecondEtc()} vagina's womb " +
            $"is about {pregVag.Womb.AgeOfOldest} old.";
    }
}