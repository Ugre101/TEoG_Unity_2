using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoseMain : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private List<EnemyPrefab> enemies = new List<EnemyPrefab>();

    [SerializeField]
    private TextMeshProUGUI _textBox = null;

    [SerializeField]
    private GameObject Leave = null;

    [SerializeField]
    private SexChar playerChar = null, enemyChar = null;

    private EnemyPrefab newTarget = null;
    public EnemyPrefab Target => newTarget != null ? newTarget : enemies[0];
    private bool canLeave = false;

    [SerializeField]
    private List<LoseScene> forced = new List<LoseScene>();

    [SerializeField]
    private List<LoseScene> submit = new List<LoseScene>();

    private List<LoseScene> CanDo(List<LoseScene> scenes) => scenes.Where(s => s.CanDo(player, Target)).Select(s => s).ToList();

    private LoseScene GetAScene(List<LoseScene> scenes)
    {
        return scenes[0];
    }

    public void Setup(List<EnemyPrefab> parEnemies)
    {
        gameObject.SetActive(true);
        enemies = parEnemies;
        _textBox.text = null;

        playerChar.Setup(player);
        enemyChar.Setup(Target);
        newTarget = null;
        Debug.Log(CanDo(forced).Count);
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}