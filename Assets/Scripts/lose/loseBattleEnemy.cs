using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseBattleEnemy : MonoBehaviour
{
    public List<ThePrey> _enemies = new List<ThePrey>();
    // essence gameobject

    public void AddEnemy(ThePrey enemy)
    {
        _enemies.Add(enemy);
    }
    private void OnDisable()
    {
        _enemies.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
