using TMPro;
using UnityEngine;
using System;

public class LooksMenu : MonoBehaviour
{
    public TextMeshProUGUI _looksIntro;
    public playerMain _player;
    public Settings _sett;
    private string _intro, _organs, _stats;

    private void OnEnable()
    {
        // if missing disable script
        if (_player == null)
        {
            GetComponent<LooksMenu>().enabled = false;
        }
        _intro = $"{_player.FullName}";
        _organs = $"{_sett.DicksLook(_player.Dicks)}";
        _stats = $"{_player.Str}";
        if (_looksIntro != null)
        {
            _looksIntro.text =  string.Format ("{1}{0}{2}{0}{3}",Environment.NewLine + Environment.NewLine, _intro,_organs,_stats);
        }
    }
}