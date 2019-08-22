using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dorm : MonoBehaviour
{
    public int Level = 0;
    public int BaseCost = 0;
    public float CostMod = 1f;
    public int Cost
    {
        get
        {
            return Mathf.CeilToInt(BaseCost * CostMod);
        }
    }
    public bool Upgrade(int gold)
    {
        if (Cost > gold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasSpace
    {
        get
        {
            return Level * 3 > this.transform.childCount;
        }
    }
    public bool CanTake(BasicChar wannaTake)
    {
        return HasSpace && wannaTake.sexStats.SessionOrgasm >= 0;
    }
    public List<BasicChar> Servants;
    public void AddTo(GameObject toAdd)
    {
        Instantiate(toAdd, this.transform);
    }
}


