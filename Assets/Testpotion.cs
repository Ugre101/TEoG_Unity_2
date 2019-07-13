using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPotion : Drinks
{
    // Start is called before the first frame update

    private void UseFunc()
    {
        Debug.Log("Works");
    }
    public override bool Use(BasicChar user)
    {
        return base.Use(user);
    }
}
