using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class afterBattleEnemy : MonoBehaviour
{
    public List<BasicChar> _enemies = new List<BasicChar>();
    public playerMain _player;
    public Slider _mascSlider, _femiSlider;
    // essence gameobject

    public void AddEnemy(BasicChar enemy)
    {
        _enemies.Add(enemy);
    }
    private void OnDisable()
    {
        _enemies.Clear();
    }

    public string DrainMasc()
    {
        float toDrain = _player.EssDrain;
        float have = 0f;
        for (int i = 0; i < _enemies.Count; i++)
        {
            have += _enemies[i].LoseMasc(toDrain);
        }
        _player.Masc.Gain(have);
        return "drain masc";
    }
    public string GiveMasc()
    {
        float toGive = 3f;
        for (int i = 0; i< _enemies.Count; i++)
        {
            if (_player.Masc.Amount > toGive)
            {
                _enemies[i].Masc.Gain(_player.LoseMasc(toGive));
            }
        }
        return "give masc";
    }

    public string DrainFemi()
    {
        float toDrain = 20f;
        float have = 0f;
        for (int i = 0; i < _enemies.Count; i++)
        {
            have += _enemies[i].LoseFemi(toDrain);
        }
        _player.Femi.Gain(have);
        return "Drainfemi";
    }
    public string GiveFemi()
    {
        float toGive = 3f;
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Femi.Gain(_player.LoseFemi(toGive));
        }
        return "give femi";
    }
}
