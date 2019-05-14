using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatButtons : MonoBehaviour
{
    // Public
    public TextMeshProUGUI _textbox;

    public playerMain player;
    public GameObject combatPanel, sexPanel, losePanel;
    public List<BasicChar> _enemies = new List<BasicChar>();

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
        if (_enemies[0].TakeHealthDamage(dmg))
        {
            //  WIN
            combatPanel.SetActive(false);
            sexPanel.SetActive(true);
        }
        _EnemyTeamAttacks += text + "\n";
        TurnManager();
    }

    public void EnemyAI(BasicChar Enemy)
    {
        float str = Enemy.Str;
        float charm = Enemy.Charm;
        float dmg = attackMulti(charm < str ? str : charm);
        string text = charm < str ? 
            $"Hit you for {dmg} dmg." : $"Tease you for {dmg} dmg.";
        // Simple after player actions
        if (charm < str)
        {
            player.TakeHealthDamage(dmg);
        }
        else
        {
            player.TakeWillDamage(dmg);
        }
        _EnemyTeamAttacks += text + "\n";
    }

    public void AddToCombatLog(string addText)
    {
        _battleLog.Insert(0,addText + "\n");
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
}