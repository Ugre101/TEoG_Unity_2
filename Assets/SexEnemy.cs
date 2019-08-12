using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexEnemy : SexChar
{
    public afterBattleEnemy enemy;
    public override void OnEnable()
    {
        if (enemy._enemies.Count > 0)
        {
            whom = enemy._enemies[0];
            base.OnEnable();
        }
    }
}
