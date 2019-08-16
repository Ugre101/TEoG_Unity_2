using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHome : SexScenes
{
    public Home home;
    public GameUI gameUI;
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (home.Dorm.HasSpace && Other.sexStats.Orgasms > 5)
        {
            return true;
        }else 
        {
            return false;
        }
    }
    public override string StartScene(playerMain player, BasicChar other)
    {
        home.Dorm.AddTo(other);
        gameUI.Resume();
        // Needs testing
        Destroy(other.transform.gameObject);
        return base.StartScene(player, other);
    }
}
