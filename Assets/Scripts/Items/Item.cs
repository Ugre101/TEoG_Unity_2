using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Consumables
{
    Testpotion,
    Ale,
    Cum,
    Milk,
}
[System.Serializable]
public class Item
{
    public BasicChar User;
    public int Amount = 1;
    [SerializeField]
    protected string name = string.Empty;
    public string Name { get { return name; } }

    public virtual bool Use(int toUse = 1)
    {
        Amount -= toUse;
        return Amount > 0;
    }

    public bool Remove(int toRemove =1)
    {
        Amount -= toRemove;
        return Amount > 0;
    }


}
public class TestPotion : Item
{ 
    public TestPotion() { name = "Test potion"; }
    public override bool Use(int toUse = 1)
    {
        Debug.Log("test potion");
        return base.Use(toUse);
    }
}
public class Ale : Item
{
    public override bool Use(int toUse = 1)
    {
        Debug.Log("hehe you feel good");
        return base.Use(toUse);
    }
}