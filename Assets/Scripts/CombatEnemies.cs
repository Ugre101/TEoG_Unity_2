using System.Collections.Generic;
using UnityEngine;

public class CombatEnemies : MonoBehaviour
{
    public List<BasicChar> _enemies = new List<BasicChar>();
    public enemyStatusBars hpSlider, wpSlider;
    public CombatButtons combatButtons;

    public void AddEnemy(BasicChar enemy)
    {
        combatButtons._enemies.Add(enemy);
        _enemies.Add(enemy);
        hpSlider.Init(enemy);
        wpSlider.Init(enemy);
    }

    private void OnDisable()
    {
        _enemies.Clear();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}