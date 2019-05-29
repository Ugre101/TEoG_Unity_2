using UnityEngine;

[System.Serializable]
public class EnemyPrefab : BasicChar
{
    [Tooltip("Chosen values get mulitled by random range 0.5f to 1.5f")]
    public Reward reward;
    [SerializeField]
    [Range(0, 100)]
    private int assingStr;
    [SerializeField]
    [Range(0, 100)]
    private int assingCharm;
    [SerializeField]
    [Range(0, 100)]
    private int assingEnd;
    [SerializeField]
    [Range(0, 100)]
    private int assingDex;
    private void Start()
    {
        Str = (int)Mathf.Floor((float)assingStr * Random.Range(0.5f, 1.5f));
        Charm = (int)Mathf.Floor((float)assingCharm * Random.Range(0.5f, 1.5f));
        End = (int)Mathf.Floor((float)assingEnd * Random.Range(0.5f, 1.5f));
        Dex = (int)Mathf.Floor((float)assingDex * Random.Range(0.5f, 1.5f));
        init(1, 100f, 100f);
        Femi.Gain(200f);
        Masc.Gain(300f);
    }
}