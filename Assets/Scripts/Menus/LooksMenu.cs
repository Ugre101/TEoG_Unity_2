 using TMPro;
using UnityEngine;
using System;

public class LooksMenu : MonoBehaviour
{
    public TextMeshProUGUI _looksIntro;
    public playerMain _player;
    public Settings _sett;

    private void OnEnable()
    {
        // if missing disable script
        if (_player == null)
        {
            GetComponent<LooksMenu>().enabled = false;
        }
        if (_looksIntro != null)
        {
            _looksIntro.text = _player.Looks.Summary;
        }
    }
}