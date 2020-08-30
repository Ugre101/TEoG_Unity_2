using System.Collections.Generic;
using UnityEngine;

public class CombatTeam : MonoBehaviour
{
    private List<BasicChar> Team = new List<BasicChar>();
    [SerializeField] private GameObject TeamContainer = null;
    [SerializeField] private CombatStatus CombatStatusPrefab = null;
    [SerializeField] private CombatMain combatMain = null;

    private List<CombatStatus> combatStatuses = new List<CombatStatus>();
    private readonly Queue<CombatStatus> combatStatusesPool = new Queue<CombatStatus>();

    private CombatStatus GetCombatStatus
    {
        get
        {
            if (combatStatusesPool.Count > 0)
            {
                CombatStatus combatStatus = combatStatusesPool.Dequeue();
                combatStatus.gameObject.SetActive(true);
                return combatStatus;
            }
            else
            {
                return Instantiate(CombatStatusPrefab, TeamContainer.transform);
            }
        }
    }

    // if nobody is alive return true else false
    public bool TeamDead => !combatStatuses.Exists(s => s.Dead == false);

    public void StartFight(List<BasicChar> EnemyTeam)
    {
        Team = EnemyTeam;
        if (Team.Count < 1) // if team is less than 1 an error must have occured
            GameManager.ReturnToLastState();
        else
        {
            foreach (BasicChar combatant in Team)
            {
                GetCombatStatus.Setup(combatant, this, combatMain);
            }
            combatStatuses = new List<CombatStatus>(TeamContainer.GetComponentsInChildren<CombatStatus>());
        }
    }

    public void WeLost()
    {
        if (TeamDead)
        {
            combatMain.SomeOneDead();
        }
    }

    public void DeSelectAll()
    {
        foreach (CombatStatus cs in combatStatuses)
        {
            cs.DeSelect();
        }
    }

    private void OnDisable()
    {
        combatStatuses.ForEach(cs => combatStatusesPool.Enqueue(cs));
        TeamContainer.transform.SleepChildren();
        combatStatuses.Clear();
    }
}