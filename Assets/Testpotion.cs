using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Test potion", menuName = "Test potion")]
public class TestPotion : Drinks
{
    public override bool Use(BasicChar user)
    {
        Debug.Log("Works");
        return base.Use(user);
    }
}
