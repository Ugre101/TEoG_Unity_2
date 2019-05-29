using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Reward
{
    [Range(0,1000)][SerializeField]
    private int _expReward;
    public int ExpReward { get { return (int)Mathf.Floor(_expReward * Random.Range(05f,1.5f)); } }
    [Range(0, 1000)][SerializeField]
    private int _goldReward;
    public int GoldReward { get { return (int)Mathf.Floor(_goldReward * Random.Range(05f, 1.5f)); } }
}
