using UnityEngine;
[System.Serializable]
public class EnemyPrefab : BasicChar
{
    [Tooltip("Chosen values get mulitled by random range 0.5f to 1.5f")]
    public Reward reward;
    [SerializeField]
    [Range(0, 100)]
    private int assingStr = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingCharm = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingEnd = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingDex = 0;
    private void Start()
    {
        strength._baseValue = (int)Mathf.Floor((float)assingStr * Random.Range(0.5f, 1.5f));
        charm._baseValue = (int)Mathf.Floor((float)assingCharm * Random.Range(0.5f, 1.5f));
        endurance._baseValue = (int)Mathf.Floor((float)assingEnd * Random.Range(0.5f, 1.5f));
        dexterity._baseValue = (int)Mathf.Floor((float)assingDex * Random.Range(0.5f, 1.5f));
        init(1, 100f, 100f);
        Femi.Gain(200f);
        Masc.Gain(300f);
    }
}