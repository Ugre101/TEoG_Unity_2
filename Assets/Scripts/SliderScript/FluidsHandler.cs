using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FluidsHandler : MonoBehaviour
{
    public PlayerMain _player;
    public GameObject cBar, mBar;
    private void OnEnable()
    {
        if (_player.SexualOrgans.Balls.Total() <= 0)
        {
            cBar.SetActive(false);
        }
        else
        {
            cBar.SetActive(true);
        }
        if (_player.SexualOrgans.Lactating)
        {
            mBar.SetActive(true);
        }
        else
        {
            mBar.SetActive(false);
        }
    }
}