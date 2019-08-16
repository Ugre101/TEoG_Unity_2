using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSex : SexScenes
{
    public GameUI gameUI;

    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        // might add some cases where you can't leave right away in future;
        return true;
    }
    public override string StartScene(playerMain player, BasicChar other)
    {
        gameUI.Resume();
        return base.StartScene(player, other);
    }
}
