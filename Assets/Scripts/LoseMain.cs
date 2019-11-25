using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseMain : MonoBehaviour
{
    public playerMain player;
    public List<EnemyPrefab> enemies;
    public TextMeshProUGUI _textBox;

    public GameObject Leave;
    public SexChar playerChar, enemyChar;
    private EnemyPrefab newTarget = null;
    public EnemyPrefab Target => newTarget != null ? newTarget : enemies[0];

    public void Setup(List<EnemyPrefab> parEnemies) 
    {
        gameObject.SetActive(true);
        enemies = parEnemies;
        _textBox.text = null;

        playerChar.Setup(player);
        enemyChar.Setup(Target);

    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}