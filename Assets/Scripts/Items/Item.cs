using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Consumables,
    Misc
}
[System.Serializable]
public class Item
{
    public int Amount = 1;
    [SerializeField]
    protected ItemTypes type;
    public ItemTypes Type { get { return type; } }
    [SerializeField]
    protected string name = string.Empty;
    public string Name { get { return name; } }
    protected string useName = "Use";
    public string UseName { get { return useName; } }
    public virtual bool Use(BasicChar user)
    {
        Amount--;
        return Amount > 0;
    }

    public bool Remove(int toRemove =1)
    {
        Amount -= toRemove;
        return Amount > 0;
    }
}
public class Drinks : Item
{
    public Drinks()
    {
        useName = "Drink";
        type = ItemTypes.Consumables;
    }
}
public class Edibles : Item
{
    public Edibles()
    {
        useName = "Eat";
        type = ItemTypes.Consumables;
    }
}
public class Misc : Item
{
    public Misc()
    {
        useName = "Use";
        type = ItemTypes.Misc;
    }
}

// In game items
public class TestPotion : Drinks
{ 
    public TestPotion()
    {
        name = "Test potion";
    }
    public override bool Use(BasicChar user)
    {
        Debug.Log("Test potion");
        return base.Use(user);
    }
}
public class Ale : Item
{
    public override bool Use(BasicChar user)
    {
        Debug.Log("hehe you feel good");
        return base.Use(user);
    }
}