using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexEnemy : SexChar
{
    public AfterBattleActions enemy;
    public override void OnEnable()
    {
        if (enemy.enemies.Count > 0)
        {
            whom = enemy.enemies[0];
            base.OnEnable();
        }
    }
}
