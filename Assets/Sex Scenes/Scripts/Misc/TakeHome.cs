using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TakeHome : MonoScene
{
    public Home home;
    public GameUI gameUI;

    public void Start()
    {
        gameUI = GetComponentInParent<GameUI>();
    }
    public bool CanDo(BasicChar player, BasicChar Other)
    {
        if (home.Dorm.HasSpace && Other.sexStats.Orgasms > 5)
        {
            return true;
        }else 
        {
            return false;
        }
    }
    public void Take(playerMain player, BasicChar other)
    {
        home.Dorm.AddTo(other);
        gameUI.Resume();
        // Needs testing
        Destroy(other.transform.gameObject);
    }
}
