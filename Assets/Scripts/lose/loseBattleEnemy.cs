using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseBattleEnemy : MonoBehaviour
{
    public List<BasicChar> _enemies = new List<BasicChar>();
    // essence gameobject

    public void AddEnemy(BasicChar enemy)
    {
        _enemies.Add(enemy);
    }
    private void OnDisable()
    {
        _enemies.Clear();
    }
    // Start is called before the first frame update

    // Update is called once per frame
}
