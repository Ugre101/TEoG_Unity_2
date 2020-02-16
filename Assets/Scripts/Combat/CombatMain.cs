using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatMain : MonoBehaviour
{
    /// <summary>Can only be used by children of this as it isn't defined before first enable</summary>
    public static CombatMain GetCombatMain { get; private set; }

    private CanvasMain CanvasMain => CanvasMain.GetCanvasMain;
    private PlayerMain player;

    private PlayerMain Player => player = player != null ? player : PlayerMain.GetPlayer;

    [SerializeField] private TextMeshProUGUI _textbox = null;

    [SerializeField] private GameObject skillButtonsContainer = null;

    private List<CombatButton> skillButtons = new List<CombatButton>();

    [SerializeField] private SkillBook skillBook = null;

    public BasicChar CurrentEnemy
    {
        get
        {
            if (enemyTeamChars.Count < 1) { CanvasMain.Resume(); return null; }
            return enemyTeamChars[indexCurrentEnemy];
        }
    }

    private int indexCurrentEnemy = 0;
    private readonly List<BasicChar> playerTeamChars = new List<BasicChar>();
    private readonly List<BasicChar> enemyTeamChars = new List<BasicChar>();

    [SerializeField] private CombatTeam playerTeam = null;

    [SerializeField] private CombatTeam enemyTeam = null;

    public BasicChar Target => newTarget != null ? newTarget : CurrentEnemy;
    private BasicChar newTarget = null;

    [Header("Win")]
    [SerializeField] private AfterBattleMain afterBattle = null;

    [Header("Lose")]
    [SerializeField] private LoseMain loseBattle = null;

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
        if (!skillButtons.Exists(s => s.Skill != null))
        {
            for (int i = 0; i < Mathf.Min(Player.Skills.Count, skillButtons.Count - 1); i++)
            {
                skillButtons[i].Setup(skillBook.Dict.Match(Player.Skills[i].Id));
            }
        }
    }

    private void ResetSkills(List<BasicChar> basicChars) =>
        basicChars.ForEach(bc => skillBook.Dict.OwnedSkills(bc.Skills).ForEach(us => us.ResetCoolDown()));

    public void SetUpCombat(List<BasicChar> enemies)
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
        _ = enemyTeam.StartCoroutine(enemyTeam.StartFight(enemies));
        _ = playerTeam.StartCoroutine(playerTeam.StartFight(playerTeamChars));
        ResetSkills(playerTeamChars);
        ResetSkills(enemyTeamChars);
    }

    public void PlayerAttack(string attack)
    {
        _PlayerTeamAttacks += attack + "\n";
        TurnManager();
    }

    public void EnemyAI(BasicChar Enemy)
    {
        float str = Enemy.Stats.Str, charm = Enemy.Stats.Cha;
        //  List<string> strAttack = new List<string> { "Hits you", "Kicks you", "Grapples you down to the ground" };
        //  List<string> charmAttack = new List<string> { $"Teases you" };
        if (charm < str)
        {
            _EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicAttack).skill.Action(Enemy, player);
        }
        else
        {
            _EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicTease).skill.Action(Enemy, player);
        }
    }

    public void AddToCombatLog(string addText)
    {
        _battleLog.Insert(0, addText + "\n");
        if (_textbox != null)
        {
            _textbox.text = null;
            _battleLog.ForEach(s => _textbox.text += s);
        }
    }

    private void TurnManager()
    {
        // PlayerTeam

        // EnemyTeam
        enemyTeamChars.ForEach(e => EnemyAI(e));
        // Formay textlog
        string textToAdd = $"Turn: {_turn}\nPlayer team\n" + _PlayerTeamAttacks + "\nEnemy team\n" + _EnemyTeamAttacks + "\n";
        AddToCombatLog(textToAdd);
        _turn++;
        _PlayerTeamAttacks = null;
        _EnemyTeamAttacks = null;
        // Reset cooldoowns
        RefreshCooldown(playerTeamChars);
        RefreshCooldown(enemyTeamChars);
        skillButtons.FindAll(sb => sb.Skill != null).ForEach(cb => cb.CoolDownHandler());
        // Reset newTarget
        SelectNewTarget(null);
    }

    private void RefreshCooldown(List<BasicChar> basicChars) => basicChars.ForEach(c => skillBook.Dict.OwnedSkills(c.Skills).FindAll(s => s.skill.HasCoolDown).FindAll(s => !s.Ready).ForEach(s => s.RefreshCoolDown()));

    public void WinBattle()
    {
        enemyTeamChars.ForEach(etc =>
        {
            if (etc is Boss b)
            {
                Debug.Log("Boss");
                // add bonus scenes and stuff
                Player.ExpSystem.Exp += b.Reward.ExpReward;
                Player.Currency.Gold += Player.Perks.HasPerk(PerksTypes.Greedy)
                    ? b.Reward.GoldReward * PerkEffects.Greedy.ExtraGold(Player.Perks)
                    : b.Reward.GoldReward;
                b.IsQuest.CheckQuest();
            }
            else if (etc is EnemyPrefab e)
            {
                Player.ExpSystem.Exp += e.Reward.ExpReward;
                Player.Currency.Gold += Player.Perks.HasPerk(PerksTypes.Greedy)
                    ? e.Reward.GoldReward * PerkEffects.Greedy.ExtraGold(Player.Perks)
                    : e.Reward.GoldReward;
                e.IsQuest.CheckQuest();
            }
            // if something else
        });

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