using UnityEngine;

[System.Serializable]
public class Reward
{
    [Range(0, 1000)]
    [SerializeField]
    private int expReward = 0;

    public int ExpReward => (int)Mathf.Floor(expReward * Random.Range(05f, 1.5f));

    [Range(0, 1000)]
    [SerializeField]
    private int goldReward = 0;

    public int GoldReward => (int)Mathf.Floor(goldReward * Random.Range(05f, 1.5f));
}