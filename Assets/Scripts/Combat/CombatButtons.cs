using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatButtons : MonoBehaviour
{
    // Public
    public GameUI gameUI;

    public TextMeshProUGUI _textbox;

    public playerMain player;
    public GameObject combatPanel, sexPanel, losePanel;
    public List<EnemyPrefab> _enemies = new List<EnemyPrefab>();

    public BasicChar CurrentEnemy
    {
        get
        {
            if (enemyTeamChars.Count < 1) { gameUI.Resume(); }
            return enemyTeamChars[indexCurrentEnemy];
        }
    }

    private int indexCurrentEnemy = 0;
    public List<BasicChar> playerTeamChars, enemyTeamChars;
    public CombatTeam playerTeam, enemyTeam;

    public BasicChar target => newTarget != null ? newTarget : CurrentEnemy;
    public BasicChar newTarget;

    [Header("Win")]
    public AfterBattleActions afterBattle;

    [Header("Lose")]
    public loseBattleEnemy loseBattle;

    // Private
    private List<string> _battleLog = new List<string>();

    private string _PlayerTeamAttacks, _EnemyTeamAttacks;
    private int _turn;

    private void Start()
    {
        // every time someone dies check if a team losses
        CombatTeam.Lost += SomeOneDead;
    }

    private void OnEnable()
    {
        _battleLog.Clear();
        _turn = 1;
    }

    private void OnDisable()
    {
        _enemies.Clear();
    }

    public void SetUpCombat()
    {
        enemyTeam.StartFight(enemyTeamChars);
        playerTeam.StartFight(playerTeamChars);
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

    public void PlayerAttack(string attack)
    {
        _PlayerTeamAttacks += attack + "\n";
        TurnManager();
    }

    public void EnemyAI(BasicChar Enemy)
    {
        float str = Enemy.strength.Value, charm = Enemy.charm.Value;
        float dmg = attackMulti(charm < str ? str : charm);
        List<string> strAttack = new List<string> { "Hits you", "Kicks you",
        "Grapples you down to the ground"};
        List<string> charmAttack = new List<string> { $"Teases you" };
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
        foreach (BasicChar e in enemyTeamChars)
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
        foreach (EnemyPrefab e in enemyTeamChars)
        {
            afterBattle.enemies.Add(e);
            player.Exp += e.reward.ExpReward;
            player.Gold += e.reward.GoldReward;
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

    public void SomeOneDead()
    {
        Debug.Log(playerTeam.TeamDead + " : " + enemyTeam.TeamDead);
        if (playerTeam.TeamDead)
        {
            LoseBattle();
        }
        else if (enemyTeam.TeamDead)
        {
            WinBattle();
        }
    }
}