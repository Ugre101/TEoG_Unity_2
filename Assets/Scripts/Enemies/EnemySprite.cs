using UnityEngine;

public class EnemySprite : BasicChar
{
    private void Start()
    {
        int Str = Random.Range(5, 10);
        int Charm = Random.Range(5, 10);
        init(1, 100f, 100f, Str, Charm,100f);
        GainFemi(200f);
        GainMasc(300f);
    }

    private void Update()
    {
    }
}