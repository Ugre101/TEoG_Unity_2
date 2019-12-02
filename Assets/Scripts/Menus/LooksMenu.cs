using TMPro;
using UnityEngine;

public class LooksMenu : MonoBehaviour
{
    public TextMeshProUGUI _looksIntro;
    public PlayerMain _player;
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