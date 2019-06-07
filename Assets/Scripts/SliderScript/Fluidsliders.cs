using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluidsliders : MonoBehaviour
{
    public playerMain _player;
    public GameObject cBar, mBar;
    private void OnEnable()
    {
        if (_player.BallTotal() <= 0)
        {
            cBar.SetActive(false);
        }else
        {
            cBar.SetActive(true);
        }
        if (_player.Lactating)
        {
            mBar.SetActive(true);
        }else
        {
            mBar.SetActive(false);
        }
    }
}
