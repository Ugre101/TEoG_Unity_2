using UnityEngine;

public class EnemySprite : BasicChar
{
    private void Start()
    {
        int Str = Random.Range(5, 10);
        int Charm = Random.Range(5, 10);
        init(1, 100f, 100f, Str, Charm);
    }

    private void Update()
    {
    }
}