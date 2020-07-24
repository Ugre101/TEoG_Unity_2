using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CombatMain : MonoBehaviour
{
    /// <summary>Can only be used by children of this as it isn't defined before first enable</summary>
    public static CombatMain GetCombatMain { get; private set; }

    private PlayerMain player;

    private PlayerMain Player => player = player != null ? player : PlayerHolder.Player;

    [SerializeField] private TextMeshProUGUI _textbox = null;

    [SerializeField] private GameObject skillButtonsContainer = null;

    private List<CombatButton> skillButtons = new List<CombatButton>();

    [SerializeField] private SkillBook skillBook = null;

    [SerializeField] private CombatTeam playerTeam = null;

    [SerializeField] private CombatTeam enemyTeam = null;

    [Header("Win")]
    [SerializeField] private AfterBattleMain afterBattle = null;

    [Header("Lose")]
    [SerializeField] private LoseMain loseBattle = null;

    private string PlayerTeamAttacks { get => CombatHandler.PlayerTeamAttacks; set => CombatHandler.PlayerTeamAttacks = value; }
    private string EnemyTeamAttacks { get => CombatHandler.EnemyTeamAttacks; set => CombatHandler.SetEnemyTeamAttacks(value); }
    private int Turn { get => CombatHandler.Turn; set => CombatHandler.Turn = value; }

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
        CombatHandler.PrintActionLog += SetTextBox;
        CombatHandler.EndTurnEvent += TurnManager;
        skillButtons = new List<CombatButton>(skillButtonsContainer.GetComponentsInChildren<CombatButton>());
        if (!skillButtons.Exists(s => s.Skill != null))
        {
            for (int i = 0; i < Mathf.Min(Player.Skills.Count, skillButtons.Count - 1); i++)
            {
                skillButtons[i].Setup(skillBook.Dict.Match(Player.Skills[i].Id));
            }
        }
    }

    private void SetTextBox(string text) => _textbox.text = text;

    private void ResetSkills(List<BasicChar> basicChars) =>
        basicChars.ForEach(bc => skillBook.Dict.OwnedSkills(bc.Skills).ForEach(us => us.ResetCoolDown()));

    public void SetUpCombat(BasicChar enemy) => SetUpCombat(new List<BasicChar>() { enemy });

    public void SetUpCombat(List<BasicChar> enemies)
    {
        gameObject.SetActive(true);
        CombatHandler.SetUpCombat(enemies);
        _ = enemyTeam.StartCoroutine(enemyTeam.StartFight(enemies));
        _ = playerTeam.StartCoroutine(playerTeam.StartFight(CombatHandler.PlayerTeamChars));
        ResetSkills(CombatHandler.PlayerTeamChars);
        ResetSkills(CombatHandler.EnemyTeamChars);
    }

    public void EnemyAI(BasicChar Enemy)
    {
        float str = Enemy.Stats.Str, charm = Enemy.Stats.Cha;
        //  List<string> strAttack = new List<string> { "Hits you", "Kicks you", "Grapples you down to the ground" };
        //  List<string> charmAttack = new List<string> { $"Teases you" };
        // TODO check highest avg dmg amoung owned skills in future
        if (charm < str)
        {
            EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicAttack).skill.Action(Enemy, player);
        }
        else
        {
            EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicTease).skill.Action(Enemy, player);
        }
    }

    private void TurnManager()
    {
        // PlayerTeam

        // EnemyTeam
        CombatHandler.EnemyTeamChars.ForEach(e => EnemyAI(e));
        // Formay textlog
        string textToAdd = $"Turn: {Turn}\nPlayer team\n" + PlayerTeamAttacks + "\nEnemy team\n" + EnemyTeamAttacks + "\n";
        CombatHandler.AddToCombatLog(textToAdd);
        Turn++;
        PlayerTeamAttacks = null;
        EnemyTeamAttacks = null;
        // Reset cooldoowns
        RefreshCooldown(CombatHandler.PlayerTeamChars);
        RefreshCooldown(CombatHandler.EnemyTeamChars);
        skillButtons.FindAll(sb => sb.Skill != null).ForEach(cb => cb.CoolDownHandler());
        // Reset newTarget
        SelectNewTarget(null);
    }

    private void RefreshCooldown(List<BasicChar> basicChars) => basicChars.ForEach(c => skillBook.Dict.OwnedSkills(c.Skills).FindAll(s => s.skill.HasCoolDown).FindAll(s => !s.Ready).ForEach(s => s.RefreshCoolDown()));

    public void LoseBattle()
    {
        gameObject.SetActive(false);
        loseBattle.Setup(CombatHandler.EnemyTeamChars);
    }

    public void WinBattle()
    {
        CombatHandler.EnemyTeamChars.ForEach(etc =>
        {
            if (etc is Boss b)
            {
                Debug.Log("Boss");
                PostBattleReward(b);
                if (b.PostBattleDialog)
                {
                    // TODO add post battle dialog
                }
            }
            else if (etc is EnemyPrefab e)
            {
                if (player.Perks.HasPerk(PerksTypes.Bully))
                {
                    e.HP.SetToPrecent(1f - PerkEffects.Bully.HealthReGainReduction(player.Perks));
                    e.WP.SetToPrecent(1f - PerkEffects.Bully.HealthReGainReduction(player.Perks));
                }
                else
                {
                    e.HP.FullGain();
                    e.WP.FullGain();
                }
                PostBattleReward(e);
            }
            // if something else
        });

        afterBattle.Setup(CombatHandler.EnemyTeamChars);
        gameObject.SetActive(false);
    }

    private void PostBattleReward(EnemyPrefab b)
    {
        // Player loses obedince towards losing enemy
        player.RelationshipTracker.GetTempRelationshipWith(b).ObedienceStat.BaseValue--;
        // Losing enemy gain obedince and loses affection towards player
        b.RelationshipTracker.GetTempRelationshipWith(player).ObedienceStat.BaseValue++;
        b.RelationshipTracker.GetTempRelationshipWith(player).AffectionStat.BaseValue--;
        Player.ExpSystem.GainExp(Player.ExpSystem.Exp + b.Reward.ExpReward);
        b.Reward.HandleDrops(Player);
        Player.Currency.Gold += Player.Perks.HasPerk(PerksTypes.Greedy)
            ? b.Reward.GoldReward * PerkEffects.Greedy.ExtraGold(Player.Perks)
            : b.Reward.GoldReward;
        b.IsQuest.CheckQuest();
    }

    public void SomeOneDead()
    {
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
        CombatHandler.NewTarget = target;
    }
}

public static class CombatHandler
{
    private static readonly List<string> battleLog = new List<string>();

    public static void ClearBattleLog()
    {
        battleLog.Clear();
        PrintActionLog?.Invoke(string.Empty);
    }

    public static void AddToCombatLog(string addText)
    {
        battleLog.Insert(0, addText + "\n");

        StringBuilder sb = new StringBuilder();
        foreach (string s in battleLog)
        {
            sb.Append(s);
        }
        PrintActionLog?.Invoke(sb.ToString());
    }

    public static Action<string> PrintActionLog;

    public static List<BasicChar> PlayerTeamChars { get; } = new List<BasicChar>();
    public static List<BasicChar> EnemyTeamChars { get; } = new List<BasicChar>();

    public static BasicChar CurrentEnemy
    {
        get
        {
            if (EnemyTeamChars.Count < 1)
            {
                GameManager.ReturnToLastState();  // Error return to main game
                return null;
            }
            return EnemyTeamChars[indexCurrentEnemy];
        }
    }

    private static int indexCurrentEnemy = 0;

    public static BasicChar Target => NewTarget ?? CurrentEnemy;
    public static BasicChar NewTarget { private get; set; } = null;

    public static string PlayerTeamAttacks { get; set; }

    private static string enemyTeamAttacks;

    public static string EnemyTeamAttacks => enemyTeamAttacks;

    public static void SetEnemyTeamAttacks(string value) => enemyTeamAttacks = value;

    public static int Turn { get; set; }

    public static void SetUpCombat(List<BasicChar> enemies)
    {
        // Clear battle log
        ClearBattleLog();
        Turn = 1;

        if (PlayerTeamChars.Count < 1)
        {
            PlayerTeamChars.Add(PlayerHolder.GetPlayerHolder.BasicChar);
        }

        EnemyTeamChars.Clear();
        EnemyTeamChars.AddRange(enemies);
    }

    public static void PlayerAttack(string attack)
    {
        PlayerTeamAttacks += attack + "\n";
        EndTurnEvent?.Invoke();
        //   TurnManager();
    }

    public delegate void EndTurn();

    public static event EndTurn EndTurnEvent;
}