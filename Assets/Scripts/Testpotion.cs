using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TestPotion", menuName = "TestPotion")]
public class TestPotion : Drinks
{
    public override void Use()
    {
        Debug.Log("Works");
        base.Use();
    }
}
