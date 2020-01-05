using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatMain : MonoBehaviour
{
    /// <summary>Can only be used by children of this as it isn't defined before first enable</summary>
    public static CombatMain GetCombatMain { get; private set; }

    private CanvasMain CanvasMain => CanvasMain.GetCanvasMain;
    private PlayerMain Player => PlayerMain.GetPlayer;

    [SerializeField]
    private TextMeshProUGUI _textbox = null;

    [SerializeField]
    private GameObject skillButtonsContainer = null;

    [SerializeField]
    private List<CombatButton> skillButtons = null;

    [SerializeField]
    private SkillBook skillBook = null;

    public BasicChar CurrentEnemy
    {
        get
        {
            if (enemyTeamChars.Count < 1) { CanvasMain.Resume(); }
            return enemyTeamChars[indexCurrentEnemy];
        }
    }

    private int indexCurrentEnemy = 0;
    private readonly List<BasicChar> playerTeamChars = new List<BasicChar>();
    private readonly List<EnemyPrefab> enemyTeamChars = new List<EnemyPrefab>();

    [SerializeField]
    private CombatTeam playerTeam = null;

    [SerializeField]
    private CombatTeam enemyTeam = null;

    public BasicChar Target => newTarget != null ? newTarget : CurrentEnemy;
    private BasicChar newTarget = null;

    [SerializeField]
    [Header("Win")]
    private AfterBattleMain afterBattle = null;

    [SerializeField]
    [Header("Lose")]
    private LoseMain loseBattle = null;

    // Private
    private readonly List<string> _battleLog = new List<string>();

    private string _PlayerTeamAttacks, _EnemyTeamAttacks;
    private int _turn;

    private void Awake()
    {
        if (GetCombatMain == null)
        {
            GetCombatMain = this;
        }
        else if (GetCombatMain != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        skillButtons = new List<CombatButton>(skillButtonsContainer.GetComponentsInChildren<CombatButton>());
        // every time someone dies check if a team losses
        if (!skillButtons.Exists(s => s.Skill != null))
        {
            for (int i = 0; i < Player.Skills.Count; i++)
            {
                skillButtons[i].Setup(skillBook.Dict.Match(Player.Skills[i].Id));
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

        if (playerTeamChars.Count < 1)
        {
            playerTeamChars.Add(Player);
        }

        enemyTeamChars.Clear();
        enemyTeamChars.AddRange(enemies);
        _ = enemyTeam.StartCoroutine(enemyTeam.StartFight(new List<BasicChar>(enemies)));
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
            if (Player.HP.TakeDmg(dmg))
            {
                // lose battle
            }
        }
        else
        {
            if (Player.WP.TakeDmg(dmg))
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

    public void WinBattle()
    {
        foreach (EnemyPrefab e in enemyTeamChars)
        {
            Player.ExpSystem.Exp += e.reward.ExpReward;
            Player.Currency.Gold += e.reward.GoldReward;
        }
        afterBattle.Setup(enemyTeamChars);
        gameObject.SetActive(false);
    }

    public void LoseBattle()
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