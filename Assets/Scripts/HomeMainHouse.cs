﻿using UnityEngine;

public class HomeMainHouse : MonoBehaviour
{
    private int Level => DormUpgrades.MainHouse.Level;

    public void Sleep()
    {
        if (Level == 0)
        {
            DateSystem.PassHour(8);
            // Heal should take care of itself
        }
    }
}