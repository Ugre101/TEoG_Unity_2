using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LooksMenu : MonoBehaviour
{
    public TextMeshProUGUI _looksIntro;
    public PlayerMain player;
    private bool exactDetails = false;

    private void OnEnable()
    {
        // if missing disable script
        if (player == null)
        {
            GetComponent<LooksMenu>().enabled = false;
        }
        if (_looksIntro != null)
        {
            _looksIntro.text = player.Looks.Summary;
        }
    }

    public void BodyLook()
    {
        string body = $"Age: {player.Age.AgeYears}years old\nHeight: {Settings.MorInch(player.Body.Height.Value)}\nWeight: {Settings.KgorP(player.Body.Weight)}";
    }

    public void StatsDetails()
    {
        string stats = $"Strength: {player.Stats.Str}\nCharm: {player.Stats.Cha}\nEndurance: {player.Stats.End}";
    }

    public void OrgansLook()
    {
        string organs = player.SexualOrgans.Dicks.Looks();
        _looksIntro.text = organs;
    }

    public void PregnancyLook()
    {
        string pregLook = $"Virility: {player.PregnancySystem.GetVirility}\n" +
            $"Fertility: {player.PregnancySystem.GetFertility}\n\n";
        if (player.Pregnant)
        {
            List<Vagina> pregVags = player.SexualOrgans.Vaginas.FindAll(v => v.Womb.HasFetus);
            foreach (Vagina vag in pregVags)
            {
                pregLook += FetusDesc(vag) + "\n";
            }
        }
    }

    private string FetusDesc(Vagina pregVag)
    {
        int index = player.SexualOrgans.Vaginas.IndexOf(pregVag);
        if (pregVag.Womb.Fetuses.Count > 1)
        {
            // TODO add text for multitude of children
        }
        return $"The unborn child inside your {index.FirstSecondEtc()} vagina's womb " +
            $"is about {pregVag.Womb.AgeOfOldest} old.";
    }
}