using System.Collections.Generic;
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

    private List<LoseScene> canForced = new List<LoseScene>();

    [SerializeField]
    private List<LoseScene> submit = new List<LoseScene>();

    private List<LoseScene> canSubmit = new List<LoseScene>();

    public void Setup(List<EnemyPrefab> parEnemies)
    {
        gameObject.SetActive(true);
        enemies = parEnemies;
        _textBox.text = null;

        playerChar.Setup(player);
        enemyChar.Setup(Target);
        newTarget = null;
        canForced.Clear();
        canSubmit.Clear();
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }

    private void PossibleScenes()
    {
        forced.ForEach(f =>
        {
            if (f.CanDo(player, Target))
            {
                canForced.Add(f);
            }
        });
        submit.ForEach(s =>
        {
            if (s.CanDo(player, Target))
            {
                canSubmit.Add(s);
            }
        });
    }
}