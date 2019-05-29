using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatButtons : MonoBehaviour
{
    // Public
    public TextMeshProUGUI _textbox;

    public playerMain player;
    public GameObject combatPanel, sexPanel, losePanel;
    public List<EnemyPrefab> _enemies = new List<EnemyPrefab>();

    [Header("Win")]
    public afterBattleEnemy afterBattle;

    [Header("Lose")]
    public loseBattleEnemy loseBattle;

    // Private
    private List<string> _battleLog = new List<string>();

    private string _PlayerTeamAttacks, _EnemyTeamAttacks;
    private int _turn;

    private void OnEnable()
    {
        _battleLog.Clear();
        _turn = 1;
    }

    private void OnDisable()
    {
        _enemies.Clear();
    }

    public void FleeButton()
    {
        float toBeat = 7;
        float fleeRoll = Random.Range(0, 10);
        if (toBeat < fleeRoll)
        {
            // Success
        }
        else
        {
            _PlayerTeamAttacks += $"You failed to flee" + "\n";
            TurnManager();
            // Enemy turn
        }
    }

    public void BasicAttack()
    {
        float dmg = attackMulti(player.Str);
        string text = $"You dealth {dmg}dmg to her/him";
        _EnemyTeamAttacks += text + "\n";
        TurnManager();
        if (_enemies[0].HP.TakeDmg(dmg))
        {
            //  WIN in future have to array and send beaten enmies to other array
            WinBattle();
        }
    }

    public void EnemyAI(BasicChar Enemy)
    {
        float str = Enemy.Str, charm = Enemy.Charm;
        float dmg = attackMulti(charm < str ? str : charm);
        var strAttack = new List<string> { "Hits you", "Kicks you",
        "Grapples you down to the ground"};
        var charmAttack = new List<string> { $"Teases you" };
        string randomStr = strAttack[Random.Range(0, strAttack.Count)] + $", causing {dmg} dmg.";
        string randomCharm = charmAttack[Random.Range(0, charmAttack.Count)] + $", weakening your will by {dmg}.";
        string text = charm < str ? randomStr : randomCharm;
        // Simple after player actions
        if (charm < str)
        {
            if (player.HP.TakeDmg(dmg))
            {
                // lose battle
            }
        }
        else
        {
            if (player.WP.TakeDmg(dmg))
            {
                // lose battle
            }
        }
        _EnemyTeamAttacks += text + "\n";
    }

    public void AddToCombatLog(string addText)
    {
        _battleLog.Insert(0, addText + "\n");
        if (_textbox != null)
        {
            _textbox.text = null;
            foreach (string t in _battleLog)
            {
                _textbox.text += t;
            }
        }
    }

    private float attackMulti(float dmg)
    {
        float finalDMG = dmg;
        finalDMG *= Random.Range(1f, 3f);
        return Mathf.Round(finalDMG);
    }

    private void TurnManager()
    {
        // PlayerTeam
        foreach (BasicChar e in _enemies)
        {
            EnemyAI(e);
        }
        string textToAdd = $"Turn: {_turn}\n" + _PlayerTeamAttacks + _EnemyTeamAttacks + "\n";
        AddToCombatLog(textToAdd);
        _turn++;
        _PlayerTeamAttacks = null;
        _EnemyTeamAttacks = null;
    }

    private void WinBattle()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            afterBattle.AddEnemy(_enemies[i]);
            player.Exp += _enemies[i].reward.ExpReward;
            player.Gold += _enemies[i].reward.GoldReward;
        }
        combatPanel.SetActive(false);
        sexPanel.SetActive(true);
        // init enemy(s) in afterbattle
    }

    private void LoseBattle()
    {
        combatPanel.SetActive(false);
        losePanel.SetActive(true);
        // init enemy(s) in lose
    }
}