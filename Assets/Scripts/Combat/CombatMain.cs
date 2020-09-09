using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatMain : MonoBehaviour
{
    /// <summary>Can only be used by children of this as it isn't defined before first enable</summary>
    public static CombatMain GetCombatMain { get; private set; }

    [FormerlySerializedAs("_textbox")] [SerializeField] private TextMeshProUGUI textbox = null;

    [SerializeField] private GameObject skillButtonsContainer = null;

    [SerializeField] private SkillBook skillBook = null;

    [SerializeField] private CombatTeam playerTeam = null, enemyTeam = null;

    [Header("Win")]
    [SerializeField] private AfterBattleMain afterBattle = null;

    [Header("Lose")]
    [SerializeField] private LoseMain loseBattle = null;

    private static BasicChar Player => PlayerMain.Player;
    private List<CombatButton> skillButtons = new List<CombatButton>();
    private static string PlayerTeamAttacks { get => CombatHandler.PlayerTeamAttacks; set => CombatHandler.PlayerTeamAttacks = value; }
    private static string EnemyTeamAttacks { get => CombatHandler.EnemyTeamAttacks; set => CombatHandler.SetEnemyTeamAttacks(value); }
    private static int Turn { get => CombatHandler.Turn; set => CombatHandler.Turn = value; }

    private void Awake()
    {
        if (GetCombatMain == null)
            GetCombatMain = this;
        else if (GetCombatMain != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        CombatHandler.PrintActionLog += SetTextBox;
        CombatHandler.EndTurnEvent += TurnManager;
        skillButtons = new List<CombatButton>(skillButtonsContainer.GetComponentsInChildren<CombatButton>());
        if (skillButtons.Exists(s => s.Skill != null)) return;
        
        for (int i = 0; i < Mathf.Min(Player.Skills.Count, skillButtons.Count - 1); i++)
        {
            skillButtons[i].Setup(skillBook.Dict.Match(Player.Skills[i].Id));
        }
    }

    private void SetTextBox(string text) => textbox.text = text;

    private void ResetSkills(List<BasicChar> basicChars) =>
        basicChars.ForEach(bc => skillBook.Dict.OwnedSkills(bc.Skills).ForEach(us => us.ResetCoolDown()));

    public void SetUpCombat(BasicChar enemy) => SetUpCombat(new List<BasicChar>() { enemy });

    public void SetUpCombat(List<BasicChar> enemies)
    {
        gameObject.SetActive(true);
        CombatHandler.SetUpCombat(enemies);
        enemyTeam.StartFight(enemies);
        playerTeam.StartFight(CombatHandler.PlayerTeamChars);
        ResetSkills(CombatHandler.PlayerTeamChars);
        ResetSkills(CombatHandler.EnemyTeamChars);
    }

    private void EnemyAi(BasicChar enemy)
    {
        float str = enemy.Stats.Str, charm = enemy.Stats.Cha;
        //  List<string> strAttack = new List<string> { "Hits you", "Kicks you", "Grapples you down to the ground" };
        //  List<string> charmAttack = new List<string> { $"Teases you" };
        // TODO check highest avg dmg amoung owned skills in future
        if (charm < str)
            EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicAttack).skill.Action(enemy, Player);
        else
            EnemyTeamAttacks += skillBook.Dict.Match(SkillId.BasicTease).skill.Action(enemy, Player);
    }

    private void TurnManager()
    {
        // PlayerTeam

        // EnemyTeam
        CombatHandler.EnemyTeamChars.ForEach(EnemyAi);
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

    private void WinBattle()
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
                if (Player.Perks.HasPerk(PerksTypes.Bully))
                {
                    e.Hp.SetToPercent(1f - PerkEffects.Bully.HealthReGainReduction(Player.Perks));
                    e.Wp.SetToPercent(1f - PerkEffects.Bully.HealthReGainReduction(Player.Perks));
                }
                else
                {
                    e.Hp.FullGain();
                    e.Wp.FullGain();
                }
                PostBattleReward(e);
            }
            // if something else
        });

        afterBattle.Setup(CombatHandler.EnemyTeamChars);
        gameObject.SetActive(false);
    }

    private static void PostBattleReward(EnemyPrefab b)
    {
        // Player loses obedince towards losing enemy
        Player.RelationshipTracker.GetTempRelationshipWith(b).ObedienceStat.BaseValue--;
        // Losing enemy gain obedince and loses affection towards player
        b.RelationshipTracker.GetTempRelationshipWith(Player).ObedienceStat.BaseValue++;
        b.RelationshipTracker.GetTempRelationshipWith(Player).AffectionStat.BaseValue--;
        // Exp, gold and loot
        Player.ExpSystem.GainExp(Player.ExpSystem.Exp + b.Reward.ExpReward);
        b.Reward.HandleDrops(Player);
        Player.Currency.Gold += Player.Perks.HasPerk(PerksTypes.Greedy)
            ? b.Reward.GoldReward * PerkEffects.Greedy.ExtraGold(Player.Perks)
            : b.Reward.GoldReward;
        // Is enemy a quest?
        b.IsQuest.CheckQuest();
        PlayerFlags.CountTimesBeatingEnemy(b);
    }

    public void SomeOneDead()
    {
        if (playerTeam.TeamDead)
            LoseBattle();
        else if (enemyTeam.TeamDead)
            WinBattle();
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
    private static readonly List<string> BattleLog = new List<string>();

    public static void ClearBattleLog()
    {
        BattleLog.Clear();
        PrintActionLog?.Invoke(string.Empty);
    }

    public static void AddToCombatLog(string addText)
    {
        BattleLog.Insert(0, addText + "\n");

        StringBuilder sb = new StringBuilder();
        foreach (string s in BattleLog)
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
            if (EnemyTeamChars.Count >= 1) return EnemyTeamChars[indexCurrentEnemy];
            
            GameManager.ReturnToLastState();  // Error return to main game
            return null;
        }
    }

    private static int indexCurrentEnemy = 0;

    public static BasicChar Target => NewTarget ?? CurrentEnemy;
    public static BasicChar NewTarget { private get; set; } = null;

    public static string PlayerTeamAttacks { get; set; }

    public static string EnemyTeamAttacks { get; private set; }

    public static void SetEnemyTeamAttacks(string value) => EnemyTeamAttacks = value;

    public static int Turn { get; set; }

    public static void SetUpCombat(IEnumerable<BasicChar> enemies)
    {
        // Clear battle log
        ClearBattleLog();
        Turn = 1;

        if (PlayerTeamChars.Count < 1)
        {
            PlayerTeamChars.Add(PlayerMain.Player);
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