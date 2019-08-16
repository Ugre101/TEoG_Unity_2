using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeStats
{

}
public class Dorm
{
    public int Level;
    public bool HasSpace
    {
        get
        {
            return Level * 3 > Servants.Count;
        }
    }
    public List<BasicChar> Servants;
    public void AddTo(BasicChar toAdd)
    {
        Servants.Add(toAdd);
    }
}
