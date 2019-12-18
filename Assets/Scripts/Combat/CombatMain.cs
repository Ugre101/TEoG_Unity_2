using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatMain : MonoBehaviour
{
    // Public
    [SerializeField]
    private CanvasMain gameUI = null;

    [SerializeField]
    private TextMeshProUGUI _textbox = null;

    [field: SerializeField] public PlayerMain player { get; private set; } = null;

    [SerializeField]
    private GameObject skillButtonsContainer;

    [SerializeField]
    private List<CombatButton> skillButtons;

    [SerializeField]
    private SkillBook skillBook;

    public BasicChar CurrentEnemy
    {
        get
        {
            if (enemyTeamChars.Count < 1) { gameUI.Resume(); }
            return enemyTeamChars[indexCurrentEnemy];
        }
    }

    private int indexCurrentEnemy = 0;
    private List<BasicChar> playerTeamChars;
    private List<EnemyPrefab> enemyTeamChars;

    [SerializeField]
    private CombatTeam playerTeam;

    [SerializeField]
    private CombatTeam enemyTeam;

    public BasicChar Target => newTarget != null ? newTarget : CurrentEnemy;
    private BasicChar newTarget;

    [SerializeField]
    [Header("Win")]
    private AfterBattleMain afterBattle;

    [SerializeField]
    [Header("Lose")]
    private LoseMain loseBattle;

    // Private
    private readonly List<string> _battleLog = new List<string>();

    private string _PlayerTeamAttacks, _EnemyTeamAttacks;
    private int _turn;

    private void Start()
    {
        skillButtons = new List<CombatButton>(skillButtonsContainer.GetComponentsInChildren<CombatButton>());
        // every time someone dies check if a team losses
        if (!skillButtons.Exists(s => s.Skill != null))
        {
            for (int i = 0; i < player.Skills.Count; i++)
            {
                skillButtons[i].userSkill = skillBook.Dict.Match(player.Skills[i].Id);
                skillButtons[i].Setup();
            }
        }
    }

    private void ResetSkills(List<BasicChar> basicChars)
    {
        foreach (BasicChar bc in basicChars)
        {
            foreach (UserSkill us in skillBook.Dict.OwnedSkills(bc.Skills))
            {
                us.ResetCoolDown();
            }
        }
    }

    public void SetUpCombat(List<EnemyPrefab> enemies)
    {
        gameObject.SetActive(true);
        // Clear battle log
        _battleLog.Clear();
        _textbox.text = "";
        _turn = 1;

        enemyTeamChars.Clear();
        enemyTeamChars.AddRange(enemies);
        enemyTeam.StartCoroutine(enemyTeam.StartFight(new List<BasicChar>(enemies)));
        _ = playerTeam.StartCoroutine(playerTeam.StartFight(playerTeamChars));
        ResetSkills(playerTeamChars);
        ResetSkills(new List<BasicChar>(enemyTeamChars));
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
        float str = Enemy.Stats.Str, charm = Enemy.Stats.Cha;
        float dmg = AttackMulti(charm < str ? str : charm);
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

    private float AttackMulti(float dmg)
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
        // Reset cooldoowns
        RefreshCooldown(playerTeamChars);
        RefreshCooldown(new List<BasicChar>(enemyTeamChars));
        foreach (CombatButton cb in skillButtons)
        {
            if (cb.Skill != null)
            {
                cb.CoolDownHandler();
            }
        }
        // Reset newTarget
        SelectNewTarget(null);
    }

    private void RefreshCooldown(List<BasicChar> basicChars)
    {
        foreach (BasicChar c in basicChars)
        {
            foreach (UserSkill s in skillBook.Dict.OwnedSkills(c.Skills))
            {
                if (s.skill.HasCoolDown ? !s.Ready : false)
                {
                    s.RefreshCoolDown();
                }
            }
        }
    }

    private void WinBattle()
    {
        foreach (EnemyPrefab e in enemyTeamChars)
        {
            player.ExpSystem.Exp += e.reward.ExpReward;
            player.Gold += e.reward.GoldReward;
        }
        afterBattle.Setup(enemyTeamChars);
        gameObject.SetActive(false);
    }

    private void LoseBattle()
    {
        gameObject.SetActive(false);
        loseBattle.Setup(enemyTeamChars);
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

    public void SelectNewTarget(BasicChar target)
    {
        playerTeam.DeSelectAll();
        enemyTeam.DeSelectAll();
        newTarget = target;
    }
}